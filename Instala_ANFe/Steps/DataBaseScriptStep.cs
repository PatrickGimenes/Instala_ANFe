using Instala_ANFe.Database;
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
    internal class DataBaseScriptStep : IInstallStep
    {

        private readonly DatabaseConfig _config;
        private readonly string _tipoBanco;
        private readonly ILogger _logger;

        public DataBaseScriptStep(DatabaseConfig config, string tipoBanco, ILogger logger)
        {
            _config = config;
            _tipoBanco = tipoBanco;
            _logger = logger;
        }

        public string Nome => "Configurando banco de dados";

        public async Task<bool> ExecuteAsync()
        {
            try
            {
                var db = DatabaseFactory.Create(_tipoBanco);

                await db.InstallAsync(_config);

                _logger.Info("Banco configurado com sucesso");

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Erro ao configurar banco", ex);
                return false;
            }
        }
    }
}

