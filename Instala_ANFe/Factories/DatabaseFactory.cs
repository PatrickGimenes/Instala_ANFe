using Instala_ANFe.Database;
using Instala_ANFe.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instala_ANFe.Factories
{
    public class DatabaseFactory
    {
        public static IDatabaseInstaller Create(string tipo)
        {
            ILogger logger = new FileLogger();
            return tipo switch
            {
                "sqlserver" => new SqlServerInstaller(logger),
                //"mysql" => new MySqlInstaller(),
                "postgres" => new PostgresInstaller(logger),
                //"informix" => new InformixInstaller(),
                _ => throw new Exception("Banco não suportado")
            };
        }
    }
}
