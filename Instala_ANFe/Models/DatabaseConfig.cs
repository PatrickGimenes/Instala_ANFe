using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Models
{
    public class DatabaseConfig
    {
        public required string Servidor { get; set; }
        public required string Porta { get; set; }
        public required string Usuario { get; set; }
        public required string Senha { get; set; }
        public required string Banco { get; set; }

    }
}
