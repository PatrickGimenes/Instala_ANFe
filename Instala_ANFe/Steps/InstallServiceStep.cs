using Instala_ANFe.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Steps
{
    internal class InstallServiceStep : IInstallStep
    {
        public string Nome => "Instalando serviço ANFe";

        private readonly string _servicePath;
        private readonly string _serviceName;
        private readonly ILogger _logger;

        public InstallServiceStep(string servicePath, string serviceName, ILogger logger)
        {
            _servicePath = servicePath;
            _serviceName = serviceName;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync()
        {
            try
            {
                string exePath = Path.Combine(_servicePath, "ANFeWinService.exe");

                if (!File.Exists(exePath))
                {
                    _logger.Error($"Executável do serviço não encontrado: {exePath}");
                    return false;
                }

                _logger.Info("Criando serviço...");

                
                bool created = RunCommand($"create {_serviceName} binPath= \"{exePath}\" start= auto ");

                if (!created)
                {
                    _logger.Error("Erro ao criar serviço (talvez já exista)");
                }

                _logger.Info("Iniciando serviço...");

                
                //bool started = RunCommand($"start {_serviceName}");

                //if (!started)
                //{
                //    _logger.Error("Erro ao iniciar serviço");
                //    return false;
                //}

                _logger.Info("Serviço instalado com sucesso");

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Erro ao instalar serviço", ex);
                return false;
            }
        }

        private bool RunCommand(string args)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = "sc.exe",
                    Arguments = args,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using var process = Process.Start(psi);

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                if (!string.IsNullOrWhiteSpace(output))
                    _logger.Info(output);

                if (!string.IsNullOrWhiteSpace(error))
                    _logger.Error(error);

                return process.ExitCode == 0;
            }
            catch (Exception ex)
            {
                _logger.Error("Erro ao executar comando SC", ex);
                return false;
            }
        }
    }
}
