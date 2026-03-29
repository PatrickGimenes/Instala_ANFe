using Instala_ANFe.Models;
using Instala_ANFe.Utils;
using Npgsql;

namespace Instala_ANFe.Database
{
    public class PostgresInstaller : IDatabaseInstaller
    {
        private readonly ILogger _logger;

        public PostgresInstaller(ILogger logger)
        {
            _logger = logger;
        }

        public async Task InstallAsync(DatabaseConfig config)
        {
            string connString =
                $"Host={config.Servidor};" +
                $"Port={config.Porta};" +
                $"Username={config.Usuario};" +
                $"Password={config.Senha};" +
                $"Database={config.Banco}";

            await using var conn = new NpgsqlConnection(connString);
            await conn.OpenAsync();

            await using var transaction = await conn.BeginTransactionAsync();

            try
            {
                _logger.Info("Iniciando configuração do banco (com transação)...");

                await ExecuteScriptAsync(conn, transaction, "Scripts/01_create_tables.sql");
                await ExecuteScriptAsync(conn, transaction, "Scripts/02_seed_data.sql");

                await transaction.CommitAsync();

                _logger.Info("Banco configurado com sucesso (commit realizado)");
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
            NpgsqlConnection conn,
            NpgsqlTransaction transaction,
            string path)
                {
                    _logger.Info($"Executando script: {path}");

                    if (!File.Exists(path))
                        throw new Exception($"Script não encontrado: {path}");

                    string script = await File.ReadAllTextAsync(path);

                    await using var cmd = new NpgsqlCommand(script, conn, transaction);

                    await cmd.ExecuteNonQueryAsync();

                    _logger.Info($"Script executado com sucesso: {path}");        
        }

        public async Task<bool> TestConnectionAsync(DatabaseConfig config)
        {
            try
            {
                _logger.Info("Início do teste de conexão");

                string connString =
                    $"Host={config.Servidor};" +
                    $"Port={config.Porta};" +
                    $"Username={config.Usuario};" +
                    $"Password={config.Senha};" +
                    $"Database={config.Banco}";

                using var conn = new NpgsqlConnection(connString);
                await conn.OpenAsync();

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