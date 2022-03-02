
namespace HumanCrypto
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGame = new System.Windows.Forms.TabPage();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.networkChainTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.privateKeyTxt = new System.Windows.Forms.TextBox();
            this.saveSettingsBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.apiKeyTxt = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGame);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1059, 616);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGame
            // 
            this.tabGame.Location = new System.Drawing.Point(4, 22);
            this.tabGame.Name = "tabGame";
            this.tabGame.Size = new System.Drawing.Size(1051, 590);
            this.tabGame.TabIndex = 0;
            this.tabGame.Text = "Game";
            this.tabGame.UseVisualStyleBackColor = true;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.label3);
            this.tabSettings.Controls.Add(this.networkChainTxt);
            this.tabSettings.Controls.Add(this.label2);
            this.tabSettings.Controls.Add(this.privateKeyTxt);
            this.tabSettings.Controls.Add(this.saveSettingsBtn);
            this.tabSettings.Controls.Add(this.label1);
            this.tabSettings.Controls.Add(this.apiKeyTxt);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(1051, 590);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            this.tabSettings.Enter += new System.EventHandler(this.tabSettings_Enter);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 84);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Network Chain ID";
            // 
            // networkChainTxt
            // 
            this.networkChainTxt.Location = new System.Drawing.Point(133, 81);
            this.networkChainTxt.Name = "networkChainTxt";
            this.networkChainTxt.Size = new System.Drawing.Size(378, 20);
            this.networkChainTxt.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Private Wallet Key";
            // 
            // privateKeyTxt
            // 
            this.privateKeyTxt.Location = new System.Drawing.Point(133, 55);
            this.privateKeyTxt.Name = "privateKeyTxt";
            this.privateKeyTxt.Size = new System.Drawing.Size(378, 20);
            this.privateKeyTxt.TabIndex = 3;
            // 
            // saveSettingsBtn
            // 
            this.saveSettingsBtn.Location = new System.Drawing.Point(678, 453);
            this.saveSettingsBtn.Name = "saveSettingsBtn";
            this.saveSettingsBtn.Size = new System.Drawing.Size(100, 37);
            this.saveSettingsBtn.TabIndex = 2;
            this.saveSettingsBtn.Text = "Save Settings";
            this.saveSettingsBtn.UseVisualStyleBackColor = true;
            this.saveSettingsBtn.Click += new System.EventHandler(this.saveSettingsBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Infura API Key";
            // 
            // apiKeyTxt
            // 
            this.apiKeyTxt.Location = new System.Drawing.Point(133, 18);
            this.apiKeyTxt.Name = "apiKeyTxt";
            this.apiKeyTxt.PasswordChar = '=';
            this.apiKeyTxt.Size = new System.Drawing.Size(378, 20);
            this.apiKeyTxt.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1059, 616);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabGame;
        private System.Windows.Forms.TabPage tabSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox apiKeyTxt;
        private System.Windows.Forms.Button saveSettingsBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox privateKeyTxt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox networkChainTxt;
    }
}

