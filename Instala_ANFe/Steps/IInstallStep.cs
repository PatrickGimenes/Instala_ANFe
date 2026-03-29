using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Steps
{
    internal interface IInstallStep
    {
        string Nome { get; }
        Task<bool> ExecuteAsync();
    }
}
