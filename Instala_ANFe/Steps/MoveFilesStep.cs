using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Steps
{
    public class MoveFilesStep : IInstallStep
    {
        public string Nome => "Movendo arquivos";

        private readonly string _origem;
        private readonly string _destino;

        public MoveFilesStep(string origem, string destino)
        {
            _origem = origem;
            _destino = destino;
        }

        public async Task<bool> ExecuteAsync()
        {
            try
            {
                if (!Directory.Exists(_destino))
                    Directory.CreateDirectory(_destino);

                foreach (var file in Directory.GetFiles(_origem, "*", SearchOption.AllDirectories))
                {
                    var relativePath = file.Substring(_origem.Length + 1);
                    var destinoFinal = Path.Combine(_destino, relativePath);

                    var pastaDestino = Path.GetDirectoryName(destinoFinal);

                    if (!Directory.Exists(pastaDestino))
                        Directory.CreateDirectory(pastaDestino);

                    File.Copy(file, destinoFinal, true);
                }

                await Task.CompletedTask;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
