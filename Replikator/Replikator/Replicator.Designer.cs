namespace Replikator
{
    partial class Replika
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.StartReplication = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.df = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MasterDBName = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.MasterPassword = new System.Windows.Forms.TextBox();
            this.MasterUserName = new System.Windows.Forms.TextBox();
            this.MasterServerName = new System.Windows.Forms.TextBox();
            this.MasterAutorization = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.SlaveDBName = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.SlavePassword = new System.Windows.Forms.TextBox();
            this.SlaveUserName = new System.Windows.Forms.TextBox();
            this.SlaveServerName = new System.Windows.Forms.TextBox();
            this.SlaveAutorization = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Timer = new System.Windows.Forms.NumericUpDown();
            this.MaxBDError = new System.Windows.Forms.NumericUpDown();
            this.AdminEmail = new System.Windows.Forms.TextBox();
            this.SchemeName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.exit = new System.Windows.Forms.Button();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.loadConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConfigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Timer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxBDError)).BeginInit();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartReplication
            // 
            this.StartReplication.Location = new System.Drawing.Point(12, 579);
            this.StartReplication.Name = "StartReplication";
            this.StartReplication.Size = new System.Drawing.Size(99, 23);
            this.StartReplication.TabIndex = 0;
            this.StartReplication.Text = "StartReplication";
            this.StartReplication.UseVisualStyleBackColor = true;
            this.StartReplication.Click += new System.EventHandler(this.StartReplication_Click_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Autorization";
            // 
            // df
            // 
            this.df.AutoSize = true;
            this.df.Location = new System.Drawing.Point(15, 168);
            this.df.Name = "df";
            this.df.Size = new System.Drawing.Size(53, 13);
            this.df.TabIndex = 2;
            this.df.Text = "Password";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Server name";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 132);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "User name";
            // 
            // groupBox1
            // 
            this.groupBox1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBox1.Controls.Add(this.MasterDBName);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.MasterPassword);
            this.groupBox1.Controls.Add(this.MasterUserName);
            this.groupBox1.Controls.Add(this.MasterServerName);
            this.groupBox1.Controls.Add(this.MasterAutorization);
            this.groupBox1.Controls.Add(this.df);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.groupBox1.Location = new System.Drawing.Point(12, 32);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(403, 199);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Master Config";
            // 
            // MasterDBName
            // 
            this.MasterDBName.Location = new System.Drawing.Point(89, 99);
            this.MasterDBName.Name = "MasterDBName";
            this.MasterDBName.Size = new System.Drawing.Size(308, 20);
            this.MasterDBName.TabIndex = 10;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(15, 102);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(51, 13);
            this.label12.TabIndex = 9;
            this.label12.Text = "DB name";
            // 
            // MasterPassword
            // 
            this.MasterPassword.Location = new System.Drawing.Point(89, 165);
            this.MasterPassword.Name = "MasterPassword";
            this.MasterPassword.PasswordChar = '*';
            this.MasterPassword.Size = new System.Drawing.Size(308, 20);
            this.MasterPassword.TabIndex = 8;
            // 
            // MasterUserName
            // 
            this.MasterUserName.Location = new System.Drawing.Point(89, 129);
            this.MasterUserName.Name = "MasterUserName";
            this.MasterUserName.Size = new System.Drawing.Size(308, 20);
            this.MasterUserName.TabIndex = 7;
            // 
            // MasterServerName
            // 
            this.MasterServerName.Location = new System.Drawing.Point(89, 69);
            this.MasterServerName.Name = "MasterServerName";
            this.MasterServerName.Size = new System.Drawing.Size(308, 20);
            this.MasterServerName.TabIndex = 6;
            // 
            // MasterAutorization
            // 
            this.MasterAutorization.FormattingEnabled = true;
            this.MasterAutorization.Items.AddRange(new object[] {
            "Windows",
            "SQL Server"});
            this.MasterAutorization.Location = new System.Drawing.Point(89, 33);
            this.MasterAutorization.Name = "MasterAutorization";
            this.MasterAutorization.Size = new System.Drawing.Size(308, 21);
            this.MasterAutorization.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.SlaveDBName);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.SlavePassword);
            this.groupBox2.Controls.Add(this.SlaveUserName);
            this.groupBox2.Controls.Add(this.SlaveServerName);
            this.groupBox2.Controls.Add(this.SlaveAutorization);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Location = new System.Drawing.Point(12, 237);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(403, 203);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Slave Config";
            // 
            // SlaveDBName
            // 
            this.SlaveDBName.Location = new System.Drawing.Point(87, 95);
            this.SlaveDBName.Name = "SlaveDBName";
            this.SlaveDBName.Size = new System.Drawing.Size(308, 20);
            this.SlaveDBName.TabIndex = 10;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(13, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 9;
            this.label13.Text = "DB name";
            // 
            // SlavePassword
            // 
            this.SlavePassword.Location = new System.Drawing.Point(89, 172);
            this.SlavePassword.Name = "SlavePassword";
            this.SlavePassword.PasswordChar = '*';
            this.SlavePassword.Size = new System.Drawing.Size(308, 20);
            this.SlavePassword.TabIndex = 8;
            // 
            // SlaveUserName
            // 
            this.SlaveUserName.Location = new System.Drawing.Point(89, 136);
            this.SlaveUserName.Name = "SlaveUserName";
            this.SlaveUserName.Size = new System.Drawing.Size(308, 20);
            this.SlaveUserName.TabIndex = 7;
            // 
            // SlaveServerName
            // 
            this.SlaveServerName.Location = new System.Drawing.Point(89, 69);
            this.SlaveServerName.Name = "SlaveServerName";
            this.SlaveServerName.Size = new System.Drawing.Size(308, 20);
            this.SlaveServerName.TabIndex = 6;
            // 
            // SlaveAutorization
            // 
            this.SlaveAutorization.FormattingEnabled = true;
            this.SlaveAutorization.Items.AddRange(new object[] {
            "Windows",
            "SQL Server"});
            this.SlaveAutorization.Location = new System.Drawing.Point(89, 33);
            this.SlaveAutorization.Name = "SlaveAutorization";
            this.SlaveAutorization.Size = new System.Drawing.Size(308, 21);
            this.SlaveAutorization.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 175);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 33);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(62, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Autorization";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 139);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "User name";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 69);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 13);
            this.label7.TabIndex = 3;
            this.label7.Text = "Server name";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Timer);
            this.groupBox3.Controls.Add(this.MaxBDError);
            this.groupBox3.Controls.Add(this.AdminEmail);
            this.groupBox3.Controls.Add(this.SchemeName);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Location = new System.Drawing.Point(10, 446);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(403, 127);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Other Config";
            // 
            // Timer
            // 
            this.Timer.Location = new System.Drawing.Point(285, 95);
            this.Timer.Name = "Timer";
            this.Timer.Size = new System.Drawing.Size(112, 20);
            this.Timer.TabIndex = 11;
            // 
            // MaxBDError
            // 
            this.MaxBDError.Location = new System.Drawing.Point(91, 95);
            this.MaxBDError.Name = "MaxBDError";
            this.MaxBDError.Size = new System.Drawing.Size(115, 20);
            this.MaxBDError.TabIndex = 10;
            this.MaxBDError.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // AdminEmail
            // 
            this.AdminEmail.Location = new System.Drawing.Point(89, 30);
            this.AdminEmail.Name = "AdminEmail";
            this.AdminEmail.Size = new System.Drawing.Size(308, 20);
            this.AdminEmail.TabIndex = 9;
            // 
            // SchemeName
            // 
            this.SchemeName.Location = new System.Drawing.Point(89, 64);
            this.SchemeName.Name = "SchemeName";
            this.SchemeName.Size = new System.Drawing.Size(308, 20);
            this.SchemeName.TabIndex = 6;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 100);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 13);
            this.label8.TabIndex = 2;
            this.label8.Text = "Max BD Error";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 33);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(64, 13);
            this.label9.TabIndex = 1;
            this.label9.Text = "Admin Email";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(246, 100);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 13);
            this.label10.TabIndex = 4;
            this.label10.Text = "Timer";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(15, 64);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Scheme name";
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(316, 579);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(99, 23);
            this.exit.TabIndex = 11;
            this.exit.Text = "Exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.helpToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(425, 24);
            this.menuStrip.TabIndex = 12;
            this.menuStrip.Text = "menuStrip";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loadConfigToolStripMenuItem,
            this.saveConfigToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            // 
            // loadConfigToolStripMenuItem
            // 
            this.loadConfigToolStripMenuItem.Name = "loadConfigToolStripMenuItem";
            this.loadConfigToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.loadConfigToolStripMenuItem.Text = "Load Config";
            this.loadConfigToolStripMenuItem.Click += new System.EventHandler(this.loadConfigToolStripMenuItem_Click_1);
            // 
            // saveConfigToolStripMenuItem
            // 
            this.saveConfigToolStripMenuItem.Name = "saveConfigToolStripMenuItem";
            this.saveConfigToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.saveConfigToolStripMenuItem.Text = "SaveConfig";
            this.saveConfigToolStripMenuItem.Click += new System.EventHandler(this.saveConfigToolStripMenuItem_Click_1);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click_1);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            // 
            // Replika
            // 
            this.AcceptButton = this.exit;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(425, 614);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.StartReplication);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.MaximizeBox = false;
            this.Name = "Replika";
            this.Text = "Репликация БД";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Timer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxBDError)).EndInit();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartReplication;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label df;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox MasterAutorization;
        private System.Windows.Forms.TextBox MasterPassword;
        private System.Windows.Forms.TextBox MasterUserName;
        private System.Windows.Forms.TextBox MasterServerName;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox SlavePassword;
        private System.Windows.Forms.TextBox SlaveUserName;
        private System.Windows.Forms.TextBox SlaveServerName;
        private System.Windows.Forms.ComboBox SlaveAutorization;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox SchemeName;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox AdminEmail;
        private System.Windows.Forms.Button exit;
        private System.Windows.Forms.NumericUpDown MaxBDError;
        private System.Windows.Forms.NumericUpDown Timer;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loadConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConfigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox MasterDBName;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox SlaveDBName;
        private System.Windows.Forms.Label label13;
    }
}

