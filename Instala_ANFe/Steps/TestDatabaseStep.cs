using Instala_ANFe.Factories;
using Instala_ANFe.Models;
using Instala_ANFe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Steps
{
    internal class TestDatabaseStep : IInstallStep
    {
        private readonly DatabaseConfig _config;
        private readonly string _tipoBanco;
        private readonly ILogger _logger;
        public TestDatabaseStep(DatabaseConfig config, string tipoBanco, ILogger logger) { 
            _config = config;
            _tipoBanco = tipoBanco;
            _logger = logger;
        }
        string IInstallStep.Nome => "Testando conexão com o banco";

        public async Task<bool> ExecuteAsync()
        {
            try
            {
                var db = DatabaseFactory.Create(_tipoBanco);

                bool sucesso = await db.TestConnectionAsync(_config);
                return sucesso;

            }
            catch (Exception ex)
            {   
                _logger.Error("Erro ao testar conexão com o banco", ex);
                return false;
            }
        }
    }
}
