namespace Instala_ANFe
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            openFileDialog1 = new OpenFileDialog();
            btn_path = new Button();
            label1 = new Label();
            txtPath = new TextBox();
            label2 = new Label();
            cmbBanco = new ComboBox();
            btn_testConnection = new Button();
            txtBanco = new TextBox();
            txtServer = new TextBox();
            txtUser = new TextBox();
            txtPass = new TextBox();
            label3 = new Label();
            btn_install = new Button();
            txtPorta = new TextBox();
            lblStatus = new Label();
            pbANFe = new ProgressBar();
            lblProgress = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // btn_path
            // 
            btn_path.Location = new Point(226, 48);
            btn_path.Name = "btn_path";
            btn_path.Size = new Size(31, 23);
            btn_path.TabIndex = 0;
            btn_path.Text = "...";
            btn_path.UseVisualStyleBackColor = true;
            btn_path.Click += btn_path_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(28, 19);
            label1.Name = "label1";
            label1.Size = new Size(200, 15);
            label1.TabIndex = 1;
            label1.Text = "Em qual local será instalado o ANFe?";
            // 
            // txtPath
            // 
            txtPath.Location = new Point(28, 48);
            txtPath.Name = "txtPath";
            txtPath.PlaceholderText = "Ex: C:\\";
            txtPath.Size = new Size(192, 23);
            txtPath.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(28, 74);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 5;
            label2.Text = "Qual o banco?";
            // 
            // cmbBanco
            // 
            cmbBanco.FormattingEnabled = true;
            cmbBanco.Location = new Point(28, 92);
            cmbBanco.Name = "cmbBanco";
            cmbBanco.Size = new Size(121, 23);
            cmbBanco.TabIndex = 6;
            // 
            // btn_testConnection
            // 
            btn_testConnection.Location = new Point(145, 203);
            btn_testConnection.Name = "btn_testConnection";
            btn_testConnection.Size = new Size(75, 23);
            btn_testConnection.TabIndex = 7;
            btn_testConnection.Text = "Testar";
            btn_testConnection.UseVisualStyleBackColor = true;
            btn_testConnection.Click += btn_testConnection_Click;
            // 
            // txtBanco
            // 
            txtBanco.Location = new Point(28, 216);
            txtBanco.Name = "txtBanco";
            txtBanco.PlaceholderText = "banco";
            txtBanco.Size = new Size(100, 23);
            txtBanco.TabIndex = 8;
            // 
            // txtServer
            // 
            txtServer.Location = new Point(28, 158);
            txtServer.Name = "txtServer";
            txtServer.PlaceholderText = "server";
            txtServer.Size = new Size(100, 23);
            txtServer.TabIndex = 9;
            // 
            // txtUser
            // 
            txtUser.Location = new Point(28, 245);
            txtUser.Name = "txtUser";
            txtUser.PlaceholderText = "usuário";
            txtUser.Size = new Size(100, 23);
            txtUser.TabIndex = 10;
            // 
            // txtPass
            // 
            txtPass.Location = new Point(28, 274);
            txtPass.Name = "txtPass";
            txtPass.PasswordChar = '*';
            txtPass.PlaceholderText = "senha";
            txtPass.Size = new Size(100, 23);
            txtPass.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(28, 130);
            label3.Name = "label3";
            label3.Size = new Size(126, 15);
            label3.TabIndex = 12;
            label3.Text = "Informações do banco";
            // 
            // btn_install
            // 
            btn_install.Location = new Point(23, 26);
            btn_install.Name = "btn_install";
            btn_install.Size = new Size(75, 23);
            btn_install.TabIndex = 13;
            btn_install.Text = "Instalar ANFe";
            btn_install.UseVisualStyleBackColor = true;
            btn_install.Click += btn_install_Click;
            // 
            // txtPorta
            // 
            txtPorta.Location = new Point(28, 187);
            txtPorta.Name = "txtPorta";
            txtPorta.PlaceholderText = "porta";
            txtPorta.Size = new Size(100, 23);
            txtPorta.TabIndex = 14;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(18, 63);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(105, 15);
            lblStatus.TabIndex = 15;
            lblStatus.Text = "Aguardando inicio";
            // 
            // pbANFe
            // 
            pbANFe.Location = new Point(23, 81);
            pbANFe.Name = "pbANFe";
            pbANFe.Size = new Size(100, 23);
            pbANFe.Style = ProgressBarStyle.Continuous;
            pbANFe.TabIndex = 16;
            // 
            // lblProgress
            // 
            lblProgress.AutoSize = true;
            lblProgress.Location = new Point(129, 84);
            lblProgress.Name = "lblProgress";
            lblProgress.Size = new Size(23, 15);
            lblProgress.TabIndex = 17;
            lblProgress.Text = "0%";
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(btn_path);
            groupBox1.Controls.Add(txtPath);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(cmbBanco);
            groupBox1.Controls.Add(txtPorta);
            groupBox1.Controls.Add(btn_testConnection);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(txtBanco);
            groupBox1.Controls.Add(txtPass);
            groupBox1.Controls.Add(txtServer);
            groupBox1.Controls.Add(txtUser);
            groupBox1.Location = new Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(336, 298);
            groupBox1.TabIndex = 18;
            groupBox1.TabStop = false;
            groupBox1.Text = "Configurção";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btn_install);
            groupBox2.Controls.Add(lblStatus);
            groupBox2.Controls.Add(pbANFe);
            groupBox2.Controls.Add(lblProgress);
            groupBox2.Location = new Point(378, 60);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(200, 126);
            groupBox2.TabIndex = 19;
            groupBox2.TabStop = false;
            groupBox2.Text = "Instalação";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 321);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "Form1";
            Text = "Instala ANFe";
            Load += Form1_Load;
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private Button btn_path;
        private Label label1;
        private TextBox txtPath;
        private Label label2;
        private ComboBox cmbBanco;
        private Button btn_testConnection;
        private TextBox txtBanco;
        private TextBox txtServer;
        private TextBox txtUser;
        private TextBox txtPass;
        private Label label3;
        private Button btn_install;
        private TextBox txtPorta;
        private Label lblStatus;
        private ProgressBar pbANFe;
        private Label lblProgress;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
    }
}
