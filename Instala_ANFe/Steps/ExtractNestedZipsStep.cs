using Instala_ANFe.Utils;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Steps
{
    internal class ExtractNestedZipsStep : IInstallStep
    {
        public string Nome => "Extraindo arquivos internos";

        private readonly string _extractPath;
        private readonly string _appPath;
        private readonly string _servicePath;
        private readonly ILogger _logger;

        public ExtractNestedZipsStep(string extractPath, string appPath, string servicePath, ILogger logger)
        {
            _extractPath = extractPath;
            _appPath = appPath;
            _servicePath = servicePath;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync()
        {
            try
            { 
                _logger.Info("Inicio da extração");
                var zips = Directory.GetFiles(_extractPath, "*.zip", SearchOption.AllDirectories);

                var appZip = zips.FirstOrDefault(z => Path.GetFileName(z).StartsWith("ANFeApp"));
                var serviceZip = zips.FirstOrDefault(z => Path.GetFileName(z).StartsWith("ANFe_WS"));

                if (appZip == null || serviceZip == null)
                {
                    _logger.Error("Não encontrou os zips internos (ANFeApp / ANFe_WS)");
                    return false;
                }

                _logger.Info($"Extraindo App: {appZip}");
                ExtractZip(appZip, _appPath);

                _logger.Info($"Extraindo Service: {serviceZip}");
                ExtractZip(serviceZip, _servicePath);

                _logger.Info("Fim da extração");
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.Error("Erro ao extrair zips internos", ex);
                return false;
            }
        }

        private void ExtractZip(string zipPath, string destino)
        {
            //if (Directory.Exists(destino))
            //    Directory.Delete(destino, true);

            Directory.CreateDirectory(destino);

            ZipFile.ExtractToDirectory(zipPath, destino);
        }
    }
}
