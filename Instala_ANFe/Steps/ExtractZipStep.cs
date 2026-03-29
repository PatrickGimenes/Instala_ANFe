using Instala_ANFe.Utils;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Steps
{
    internal class ExtractZipStep : IInstallStep
    {
        public string Nome => "Extraindo arquivos";

        private readonly string _zipPath;
        private readonly string _destino;
        private readonly ILogger _logger;

        public ExtractZipStep(string zipPath, string destino, ILogger looger)
        {
            _zipPath = zipPath;
            _destino = destino;
            _logger = looger;
        }

        public async Task<bool> ExecuteAsync()
        {
            try
            {
                //if (Directory.Exists(_destino))
                //    Directory.Delete(_destino, true);

                _logger.Info("Inicio da extração");

                if (!File.Exists(_zipPath))
                {
                    _logger.Error($"Arquivo ZIP não encontrado: {_zipPath}");
                    return false;
                }
                ZipFile.ExtractToDirectory(_zipPath, _destino);

                await Task.CompletedTask;
                _logger.Info("Fim da extração");
                return true;
            }
            catch(Exception ex)
            {
                _logger.Error("Erro ao extrair: ", ex);
                return false;
            }
        }
    }
}
