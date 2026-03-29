using Instala_ANFe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Steps
{
    internal class DownloadFileStep : IInstallStep
    {
        public string Nome => "Baixando dependências";

        private readonly string _url;
        private readonly string _destino;
        private readonly ILogger _logger;

        public Action<int> OnProgress { get; set; }

        public DownloadFileStep(string url, string destino, ILogger logger)
        {   
            _url = url;
            _destino = destino;
            _logger = logger;
        }

        public async Task<bool> ExecuteAsync()
        {
            try
            {
                _logger.Info("Início do download");
                _logger.Info($"Salvando em: {_destino}");

                using var client = new HttpClient
                {
                    Timeout = TimeSpan.FromMinutes(10)
                };

                using var response = await client.GetAsync(
                    _url,
                    HttpCompletionOption.ResponseHeadersRead
                );

                response.EnsureSuccessStatusCode();

                var totalBytes = response.Content.Headers.ContentLength ?? -1L;

                await using var stream = await response.Content.ReadAsStreamAsync();
                await using var fs = new FileStream(
                    _destino,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    8192,
                    true
                );

                var buffer = new byte[8192];
                long totalRead = 0;
                int bytesRead;
                int lastPercent = 0;

                while ((bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                {
                    await fs.WriteAsync(buffer, 0, bytesRead);
                    totalRead += bytesRead;

                    // Só calcula se o servidor informou o tamanho
                    if (totalBytes > 0)
                    {
                        int percent = (int)((totalRead * 100) / totalBytes);

                        // Evita logar 1000x o mesmo %
                        if (percent != lastPercent)
                        {
                            lastPercent = percent;
                            _logger.Info($"Download: {percent}%");
                            OnProgress?.Invoke(percent);
                        }
                    }
                }
                
                _logger.Info("Download finalizado com sucesso");

                if (!File.Exists(_destino))
                {
                    _logger.Error("Arquivo não foi criado!");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error("Erro no download", ex);
                return false;
            }
        }
    }
}

