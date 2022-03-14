
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
            this.components = new System.ComponentModel.Container();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabGame = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tabAllAvatars = new System.Windows.Forms.TabPage();
            this.prevOwnAvatarBtn = new System.Windows.Forms.Button();
            this.nextOwnAvatarBtn = new System.Windows.Forms.Button();
            this.prevAvatarBtn = new System.Windows.Forms.Button();
            this.nextAvatarBtn = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.label5 = new System.Windows.Forms.Label();
            this.priorityFeeTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.contractKeyTxt = new System.Windows.Forms.TextBox();
            this.deployContractBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.networkChainTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.privateKey1Txt = new System.Windows.Forms.TextBox();
            this.saveSettingsBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.apiKeyTxt = new System.Windows.Forms.TextBox();
            this.notifyControl = new System.Windows.Forms.NotifyIcon(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.privateKey2Txt = new System.Windows.Forms.TextBox();
            this.makeOfferBtn = new System.Windows.Forms.Button();
            this.acceptOfferBtn = new System.Windows.Forms.Button();
            this.offerAmountTxt = new System.Windows.Forms.TextBox();
            this.priceLbl = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label8 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1.SuspendLayout();
            this.tabGame.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabAllAvatars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.tabSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabGame);
            this.tabControl1.Controls.Add(this.tabAllAvatars);
            this.tabControl1.Controls.Add(this.tabSettings);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1265, 734);
            this.tabControl1.TabIndex = 0;
            // 
            // tabGame
            // 
            this.tabGame.Controls.Add(this.button2);
            this.tabGame.Controls.Add(this.button1);
            this.tabGame.Controls.Add(this.pictureBox1);
            this.tabGame.Location = new System.Drawing.Point(4, 22);
            this.tabGame.Name = "tabGame";
            this.tabGame.Size = new System.Drawing.Size(1257, 708);
            this.tabGame.TabIndex = 0;
            this.tabGame.Text = "Testing";
            this.tabGame.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(917, 300);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(108, 36);
            this.button2.TabIndex = 2;
            this.button2.Text = "Call contract";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(917, 239);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(108, 36);
            this.button1.TabIndex = 1;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 8);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(860, 572);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // tabAllAvatars
            // 
            this.tabAllAvatars.Controls.Add(this.panel1);
            this.tabAllAvatars.Controls.Add(this.label8);
            this.tabAllAvatars.Controls.Add(this.pictureBox4);
            this.tabAllAvatars.Controls.Add(this.prevOwnAvatarBtn);
            this.tabAllAvatars.Controls.Add(this.nextOwnAvatarBtn);
            this.tabAllAvatars.Controls.Add(this.prevAvatarBtn);
            this.tabAllAvatars.Controls.Add(this.nextAvatarBtn);
            this.tabAllAvatars.Controls.Add(this.pictureBox3);
            this.tabAllAvatars.Controls.Add(this.pictureBox2);
            this.tabAllAvatars.Location = new System.Drawing.Point(4, 22);
            this.tabAllAvatars.Name = "tabAllAvatars";
            this.tabAllAvatars.Padding = new System.Windows.Forms.Padding(3);
            this.tabAllAvatars.Size = new System.Drawing.Size(1257, 708);
            this.tabAllAvatars.TabIndex = 2;
            this.tabAllAvatars.Text = "All Avatars";
            this.tabAllAvatars.UseVisualStyleBackColor = true;
            // 
            // prevOwnAvatarBtn
            // 
            this.prevOwnAvatarBtn.Location = new System.Drawing.Point(1038, 6);
            this.prevOwnAvatarBtn.Name = "prevOwnAvatarBtn";
            this.prevOwnAvatarBtn.Size = new System.Drawing.Size(56, 23);
            this.prevOwnAvatarBtn.TabIndex = 5;
            this.prevOwnAvatarBtn.Text = "Prev";
            this.prevOwnAvatarBtn.UseVisualStyleBackColor = true;
            this.prevOwnAvatarBtn.Click += new System.EventHandler(this.prevOwnAvatarBtn_Click);
            // 
            // nextOwnAvatarBtn
            // 
            this.nextOwnAvatarBtn.Location = new System.Drawing.Point(1100, 6);
            this.nextOwnAvatarBtn.Name = "nextOwnAvatarBtn";
            this.nextOwnAvatarBtn.Size = new System.Drawing.Size(56, 23);
            this.nextOwnAvatarBtn.TabIndex = 4;
            this.nextOwnAvatarBtn.Text = "Next";
            this.nextOwnAvatarBtn.UseVisualStyleBackColor = true;
            this.nextOwnAvatarBtn.Click += new System.EventHandler(this.nextOwnAvatarBtn_Click);
            // 
            // prevAvatarBtn
            // 
            this.prevAvatarBtn.Location = new System.Drawing.Point(3, 6);
            this.prevAvatarBtn.Name = "prevAvatarBtn";
            this.prevAvatarBtn.Size = new System.Drawing.Size(56, 23);
            this.prevAvatarBtn.TabIndex = 3;
            this.prevAvatarBtn.Text = "Prev";
            this.prevAvatarBtn.UseVisualStyleBackColor = true;
            this.prevAvatarBtn.Click += new System.EventHandler(this.prevAvatarBtn_Click);
            // 
            // nextAvatarBtn
            // 
            this.nextAvatarBtn.Location = new System.Drawing.Point(607, 6);
            this.nextAvatarBtn.Name = "nextAvatarBtn";
            this.nextAvatarBtn.Size = new System.Drawing.Size(56, 23);
            this.nextAvatarBtn.TabIndex = 2;
            this.nextAvatarBtn.Text = "Next";
            this.nextAvatarBtn.UseVisualStyleBackColor = true;
            this.nextAvatarBtn.Click += new System.EventHandler(this.nextAvatarBtn_Click);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox3.Location = new System.Drawing.Point(949, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(305, 702);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox3_Paint);
            this.pictureBox3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox3_MouseClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(660, 702);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            this.pictureBox2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox2_MouseClick);
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.label6);
            this.tabSettings.Controls.Add(this.privateKey2Txt);
            this.tabSettings.Controls.Add(this.label5);
            this.tabSettings.Controls.Add(this.priorityFeeTxt);
            this.tabSettings.Controls.Add(this.label4);
            this.tabSettings.Controls.Add(this.contractKeyTxt);
            this.tabSettings.Controls.Add(this.deployContractBtn);
            this.tabSettings.Controls.Add(this.label3);
            this.tabSettings.Controls.Add(this.networkChainTxt);
            this.tabSettings.Controls.Add(this.label2);
            this.tabSettings.Controls.Add(this.privateKey1Txt);
            this.tabSettings.Controls.Add(this.saveSettingsBtn);
            this.tabSettings.Controls.Add(this.label1);
            this.tabSettings.Controls.Add(this.apiKeyTxt);
            this.tabSettings.Location = new System.Drawing.Point(4, 22);
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.Size = new System.Drawing.Size(1257, 708);
            this.tabSettings.TabIndex = 1;
            this.tabSettings.Text = "Settings";
            this.tabSettings.UseVisualStyleBackColor = true;
            this.tabSettings.Enter += new System.EventHandler(this.tabSettings_Enter);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 200);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "MaxPriorityFee (Gwei)";
            // 
            // priorityFeeTxt
            // 
            this.priorityFeeTxt.Location = new System.Drawing.Point(133, 197);
            this.priorityFeeTxt.Name = "priorityFeeTxt";
            this.priorityFeeTxt.Size = new System.Drawing.Size(378, 20);
            this.priorityFeeTxt.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Contract Key";
            // 
            // contractKeyTxt
            // 
            this.contractKeyTxt.Location = new System.Drawing.Point(133, 171);
            this.contractKeyTxt.Name = "contractKeyTxt";
            this.contractKeyTxt.Size = new System.Drawing.Size(378, 20);
            this.contractKeyTxt.TabIndex = 8;
            // 
            // deployContractBtn
            // 
            this.deployContractBtn.Location = new System.Drawing.Point(517, 171);
            this.deployContractBtn.Name = "deployContractBtn";
            this.deployContractBtn.Size = new System.Drawing.Size(88, 20);
            this.deployContractBtn.TabIndex = 7;
            this.deployContractBtn.Text = "Deploy contract";
            this.deployContractBtn.UseVisualStyleBackColor = true;
            this.deployContractBtn.Click += new System.EventHandler(this.deployContractBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 145);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Network Chain ID";
            // 
            // networkChainTxt
            // 
            this.networkChainTxt.Location = new System.Drawing.Point(133, 142);
            this.networkChainTxt.Name = "networkChainTxt";
            this.networkChainTxt.Size = new System.Drawing.Size(378, 20);
            this.networkChainTxt.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Private Wallet Key 1";
            // 
            // privateKey1Txt
            // 
            this.privateKey1Txt.Location = new System.Drawing.Point(133, 55);
            this.privateKey1Txt.Name = "privateKey1Txt";
            this.privateKey1Txt.PasswordChar = '=';
            this.privateKey1Txt.Size = new System.Drawing.Size(378, 20);
            this.privateKey1Txt.TabIndex = 3;
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
            // notifyControl
            // 
            this.notifyControl.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyControl.BalloonTipTitle = "HumanCrypto";
            this.notifyControl.Text = "Notification";
            this.notifyControl.Visible = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 84);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Private Wallet Key 2";
            // 
            // privateKey2Txt
            // 
            this.privateKey2Txt.Location = new System.Drawing.Point(133, 81);
            this.privateKey2Txt.Name = "privateKey2Txt";
            this.privateKey2Txt.PasswordChar = '=';
            this.privateKey2Txt.Size = new System.Drawing.Size(378, 20);
            this.privateKey2Txt.TabIndex = 13;
            // 
            // makeOfferBtn
            // 
            this.makeOfferBtn.Enabled = false;
            this.makeOfferBtn.Location = new System.Drawing.Point(171, 26);
            this.makeOfferBtn.Name = "makeOfferBtn";
            this.makeOfferBtn.Size = new System.Drawing.Size(93, 23);
            this.makeOfferBtn.TabIndex = 6;
            this.makeOfferBtn.Text = "Make offer";
            this.makeOfferBtn.UseVisualStyleBackColor = true;
            this.makeOfferBtn.Click += new System.EventHandler(this.makeOfferBtn_Click);
            // 
            // acceptOfferBtn
            // 
            this.acceptOfferBtn.Enabled = false;
            this.acceptOfferBtn.Location = new System.Drawing.Point(3, 52);
            this.acceptOfferBtn.Name = "acceptOfferBtn";
            this.acceptOfferBtn.Size = new System.Drawing.Size(92, 23);
            this.acceptOfferBtn.TabIndex = 7;
            this.acceptOfferBtn.Text = "Accept Offer";
            this.acceptOfferBtn.UseVisualStyleBackColor = true;
            this.acceptOfferBtn.Click += new System.EventHandler(this.acceptOfferBtn_Click);
            // 
            // offerAmountTxt
            // 
            this.offerAmountTxt.Location = new System.Drawing.Point(173, 55);
            this.offerAmountTxt.Name = "offerAmountTxt";
            this.offerAmountTxt.Size = new System.Drawing.Size(91, 20);
            this.offerAmountTxt.TabIndex = 8;
            this.offerAmountTxt.Text = "0";
            // 
            // priceLbl
            // 
            this.priceLbl.AutoSize = true;
            this.priceLbl.Location = new System.Drawing.Point(3, 26);
            this.priceLbl.Name = "priceLbl";
            this.priceLbl.Size = new System.Drawing.Size(31, 13);
            this.priceLbl.TabIndex = 9;
            this.priceLbl.Text = "Price";
            // 
            // pictureBox4
            // 
            this.pictureBox4.Location = new System.Drawing.Point(667, 319);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(276, 385);
            this.pictureBox4.TabIndex = 10;
            this.pictureBox4.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(669, 303);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(39, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Parinti:";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Coral;
            this.panel1.Controls.Add(this.acceptOfferBtn);
            this.panel1.Controls.Add(this.priceLbl);
            this.panel1.Controls.Add(this.makeOfferBtn);
            this.panel1.Controls.Add(this.offerAmountTxt);
            this.panel1.Location = new System.Drawing.Point(672, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(267, 109);
            this.panel1.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1265, 734);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabGame.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabAllAvatars.ResumeLayout(false);
            this.tabAllAvatars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.tabSettings.ResumeLayout(false);
            this.tabSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
        private System.Windows.Forms.TextBox privateKey1Txt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox networkChainTxt;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox contractKeyTxt;
        private System.Windows.Forms.Button deployContractBtn;
        private System.Windows.Forms.NotifyIcon notifyControl;
        private System.Windows.Forms.TabPage tabAllAvatars;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox priorityFeeTxt;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button prevAvatarBtn;
        private System.Windows.Forms.Button nextAvatarBtn;
        private System.Windows.Forms.Button prevOwnAvatarBtn;
        private System.Windows.Forms.Button nextOwnAvatarBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox privateKey2Txt;
        private System.Windows.Forms.Label priceLbl;
        private System.Windows.Forms.TextBox offerAmountTxt;
        private System.Windows.Forms.Button acceptOfferBtn;
        private System.Windows.Forms.Button makeOfferBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Panel panel1;
    }
}

