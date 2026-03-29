using Instala_ANFe.Factories;
using Instala_ANFe.Models;
using Instala_ANFe.Steps;
using Instala_ANFe.Utils;

namespace Instala_ANFe
{
    public partial class Form1 : Form
    {
        private readonly ILogger _logger = new FileLogger();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CarregarBancos();

            
        }


        private void btn_path_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {

                

                //dialog.Description = "Selecione a pasta onde será instalada a aplicação";
                dialog.ShowNewFolderButton = true;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string caminho = dialog.SelectedPath;

                    txtPath.Text = caminho;
                }
            }
        }
        private void CarregarBancos()
        {
            cmbBanco.DataSource = new List<BancoOption>
            {
                new BancoOption { Nome = "SQL Server", Valor = "sqlserver" },
                new BancoOption { Nome = "Oracle", Valor = "oracle" },
                new BancoOption { Nome = "PostgreSQL", Valor = "postgres" },
                new BancoOption { Nome = "Informix", Valor = "informix" }
            };

            cmbBanco.DisplayMember = "Nome";
            cmbBanco.ValueMember = "Valor";
        }

        private async void btn_testConnection_Click(object sender, EventArgs e)
        {
            string tipoBanco = cmbBanco.SelectedValue?.ToString();

            var config = new DatabaseConfig
            {
                Servidor = txtServer.Text,
                Porta = txtPorta.Text,
                Usuario = txtUser.Text,
                Senha = txtPass.Text,
                Banco = txtBanco.Text
            };

            if (!ValidarConfiguracao(config, tipoBanco))
                return;

            IInstallStep testeConnection = new TestDatabaseStep(config, tipoBanco, _logger);
            bool sucesso = await testeConnection.ExecuteAsync();

            MessageBox.Show(
                sucesso ? "Conexão realizada com sucesso!" : "Falha ao conectar",
                "Teste de Conexão",
                MessageBoxButtons.OK,
                sucesso ? MessageBoxIcon.Information : MessageBoxIcon.Error
            );
        }

        private async void btn_install_Click(object sender, EventArgs e)
        {
            string basePath = txtPath.Text;
            bool is64Bits = Environment.Is64BitOperatingSystem;

            string linkANFe = is64Bits ? "https://www.automatizanfe.com.br/download/ANFe_v64.zip"
                :
                "https://www.automatizanfe.com.br/download/ANFe_v32.zip";
            string baseTemp = Path.Combine(Path.GetTempPath(), "Instala_ANFe");

            string tempDownload = Path.Combine(baseTemp, "download");
            string tempExtract = Path.Combine(baseTemp, "extract");

            Directory.CreateDirectory(tempDownload);
            Directory.CreateDirectory(tempExtract);

            string zipANFe = Path.Combine(tempDownload, "anfe.zip");

            string ANFePath = Path.Combine(basePath, "ANFe");
            string appPath = Path.Combine(ANFePath, "app");
            string servicePath = Path.Combine(ANFePath, "ANFeService");


            string tipoBanco = cmbBanco.SelectedValue?.ToString();

            var config = new DatabaseConfig
            {
                Servidor = txtServer.Text,
                Porta = txtPorta.Text,
                Usuario = txtUser.Text,
                Senha = txtPass.Text,
                Banco = txtBanco.Text
            };

            if (!ValidarConfiguracao(config, tipoBanco))
                return;



            if (string.IsNullOrWhiteSpace(basePath))
            {
                MessageBox.Show("Selecione um diretório de instalação");
                return;
            }
            var downloadStep = new DownloadFileStep(linkANFe, zipANFe, _logger);

            downloadStep.OnProgress = (percent) =>
            {
                // garante thread segura (UI thread)
                pbANFe.Invoke(new Action(() =>
                {
                    pbANFe.Value = percent;
                    lblProgress.Text = percent + "%";
                }));
            };
            var steps = new List<IInstallStep>
            {
                new CreateDirectoriesStep(basePath, _logger),
                downloadStep,
                new ExtractZipStep(zipANFe, tempExtract, _logger),
                new ExtractNestedZipsStep(
                    tempExtract,
                    appPath,
                    servicePath,
                    _logger
                ),
                new TestDatabaseStep(config, tipoBanco, _logger),
                new DataBaseScriptStep(config, tipoBanco, _logger),
                new InstallServiceStep(
                    servicePath,
                    "ANFeService",
                    _logger
                ),               
            }; 
            btn_install.Enabled = false;
            foreach (var step in steps)
            {
                lblStatus.Text = step.Nome;

                bool ok = await step.ExecuteAsync();

                if (!ok)
                {
                    MessageBox.Show($"Erro no passo: {step.Nome}");
                    break;
                }
                
            }
            btn_install.Enabled = true;
        }


        private bool ValidarConfiguracao(DatabaseConfig config, string tipoBanco)
        {
            var erros = new List<string>();

            
            if (string.IsNullOrWhiteSpace(tipoBanco))
                erros.Add("Selecione um banco de dados");

            
            if (string.IsNullOrWhiteSpace(config.Servidor))
                erros.Add("Informe o servidor");

            if (string.IsNullOrWhiteSpace(config.Porta))
                erros.Add("Informe a porta");

            if (string.IsNullOrWhiteSpace(config.Usuario))
                erros.Add("Informe o usuário");

            if (string.IsNullOrWhiteSpace(config.Banco))
                erros.Add("Informe o nome do banco");

            
            if (!string.IsNullOrWhiteSpace(config.Porta) && !int.TryParse(config.Porta, out _))
                erros.Add("Porta inválida (deve ser numérica)");

            
            if (erros.Any())
            {
                MessageBox.Show(
                    string.Join(Environment.NewLine, erros),
                    "Validação",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return false;
            }

            return true;
        }


    }
}
