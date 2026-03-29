using Instala_ANFe.Models;

namespace Instala_ANFe.Database
{
    public interface IDatabaseInstaller
    {
        Task InstallAsync(DatabaseConfig config);
        Task<bool> TestConnectionAsync(DatabaseConfig config);
    }
}