using Instala_ANFe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Steps
{
    public class CreateDirectoriesStep : IInstallStep
    {
        public string Nome => "Criando estrutura de diretórios";
        private readonly ILogger _logger;


        private readonly string _basePath;

        public CreateDirectoriesStep(string basePath, ILogger logger)
        {
            _basePath = basePath;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync()
        {
            try
            {
                string ANFePath = Path.Combine(_basePath, "ANFe");
                var directories = new[]
                {
                Path.Combine(_basePath, "ANFe"),
                Path.Combine(ANFePath, "app"),
                Path.Combine(ANFePath, "ANFeService"),
                Path.Combine(ANFePath, "ANFeService", "Logs"),
                Path.Combine(ANFePath, "certificados"),
                Path.Combine(ANFePath, "dir_xml"),
                Path.Combine(ANFePath, "dir_xml","ocr")
            };

                _logger.Info($"Criando pastas em: {ANFePath}");

                foreach (var dir in directories)
                {
                    if (!Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                        _logger.Info($"{dir} criada");
                    }
                }

                await Task.CompletedTask;
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Erro ao criar pastas: ", ex);
                return false;
            }
        }
    }
}
