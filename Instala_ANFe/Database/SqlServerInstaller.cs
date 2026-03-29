using Instala_ANFe.Models;
using Instala_ANFe.Utils;
using System.Data.SqlClient;

namespace Instala_ANFe.Database
{
    internal class SqlServerInstaller : IDatabaseInstaller
    {
        private readonly ILogger _logger;

        public SqlServerInstaller(ILogger logger)
        {
            _logger = logger;
        }

        public async Task InstallAsync(DatabaseConfig config)
        {
            string connString =
                $"Server={config.Servidor},{config.Porta};" +
                $"Database={config.Banco};" +
                $"User Id={config.Usuario};" +
                $"Password={config.Senha};" +
                $"TrustServerCertificate=True;";

            await using var conn = new SqlConnection(connString);
            await conn.OpenAsync();

            await using var transaction = conn.BeginTransaction();

            try
            {
                _logger.Info("Iniciando configuração do banco SQL Server (com transação)...");

                string basePath = AppDomain.CurrentDomain.BaseDirectory;

                await ExecuteScriptAsync(conn, transaction, Path.Combine(basePath, "Scripts", "01_create_tables.sql"));
                await ExecuteScriptAsync(conn, transaction, Path.Combine(basePath, "Scripts", "02_seed_data.sql"));

                await transaction.CommitAsync();

                _logger.Info("Banco SQL Server configurado com sucesso (commit realizado)");
            }
            catch (Exception ex)
            {
                _logger.Error("Erro detectado, executando rollback...", ex);

                await transaction.RollbackAsync();

                _logger.Info("Rollback realizado com sucesso");

                throw;
            }
        }

        private async Task ExecuteScriptAsync(
            SqlConnection conn,
            SqlTransaction transaction,
            string path)
        {
            _logger.Info($"Executando script: {path}");

            if (!File.Exists(path))
                throw new Exception($"Script não encontrado: {path}");

            string script = await File.ReadAllTextAsync(path);

            await using var cmd = new SqlCommand(script, conn, transaction);

            await cmd.ExecuteNonQueryAsync();

            _logger.Info($"Script executado com sucesso: {path}");
        }

        public async Task<bool> TestConnectionAsync(DatabaseConfig config)
        {
            try
            {
                _logger.Info("Início do teste de conexão SQL Server");

                string connString =
                    $"Server={config.Servidor},{config.Porta};" +
                    $"Database={config.Banco};" +
                    $"User Id={config.Usuario};" +
                    $"Password={config.Senha};" +
                    $"TrustServerCertificate=True;";

                using var connection = new SqlConnection(connString);
                await connection.OpenAsync();

                _logger.Info("Sucesso no teste");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Falha no teste de conexão", ex);
                return false;
            }
        }
    }
}