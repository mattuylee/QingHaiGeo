namespace QingHaiGeo
{
    partial class LoginForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LoginForm));
            this.txtUser = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.btnLogin = new System.Windows.Forms.Button();
            this.chkRememberMe = new System.Windows.Forms.CheckBox();
            this.prgLoading = new System.Windows.Forms.ProgressBar();
            this.lblBrand = new System.Windows.Forms.Label();
            this.lblConfig = new System.Windows.Forms.Label();
            this.lklWebsite = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtUser
            // 
            this.txtUser.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtUser.Location = new System.Drawing.Point(564, 202);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(148, 34);
            this.txtUser.TabIndex = 0;
            this.txtUser.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUser_KeyPress);
            // 
            // txtPassword
            // 
            this.txtPassword.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPassword.Location = new System.Drawing.Point(564, 249);
            this.txtPassword.MaxLength = 16;
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.ShortcutsEnabled = false;
            this.txtPassword.Size = new System.Drawing.Size(148, 34);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtPassword_KeyPress);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.BackColor = System.Drawing.Color.Transparent;
            this.lblUser.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblUser.Location = new System.Drawing.Point(476, 205);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(82, 24);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "账户：";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.BackColor = System.Drawing.Color.Transparent;
            this.lblPassword.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPassword.Location = new System.Drawing.Point(476, 252);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(82, 24);
            this.lblPassword.TabIndex = 3;
            this.lblPassword.Text = "密码：";
            // 
            // btnLogin
            // 
            this.btnLogin.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(194)))), ((int)(((byte)(254)))));
            this.btnLogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogin.Font = new System.Drawing.Font("宋体", 12F);
            this.btnLogin.Location = new System.Drawing.Point(623, 310);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(89, 37);
            this.btnLogin.TabIndex = 3;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // chkRememberMe
            // 
            this.chkRememberMe.AutoSize = true;
            this.chkRememberMe.BackColor = System.Drawing.Color.Transparent;
            this.chkRememberMe.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkRememberMe.Location = new System.Drawing.Point(480, 316);
            this.chkRememberMe.Name = "chkRememberMe";
            this.chkRememberMe.Size = new System.Drawing.Size(111, 24);
            this.chkRememberMe.TabIndex = 2;
            this.chkRememberMe.Text = "记住密码";
            this.chkRememberMe.UseVisualStyleBackColor = false;
            this.chkRememberMe.CheckedChanged += new System.EventHandler(this.chkRememberMe_CheckedChanged);
            // 
            // prgLoading
            // 
            this.prgLoading.ForeColor = System.Drawing.Color.SpringGreen;
            this.prgLoading.Location = new System.Drawing.Point(623, 310);
            this.prgLoading.MarqueeAnimationSpeed = 20;
            this.prgLoading.Name = "prgLoading";
            this.prgLoading.Size = new System.Drawing.Size(89, 37);
            this.prgLoading.Step = 5;
            this.prgLoading.TabIndex = 4;
            this.prgLoading.Visible = false;
            // 
            // lblBrand
            // 
            this.lblBrand.AutoSize = true;
            this.lblBrand.BackColor = System.Drawing.Color.Transparent;
            this.lblBrand.Font = new System.Drawing.Font("楷体", 10.5F);
            this.lblBrand.Location = new System.Drawing.Point(602, 393);
            this.lblBrand.Name = "lblBrand";
            this.lblBrand.Size = new System.Drawing.Size(134, 18);
            this.lblBrand.TabIndex = 6;
            this.lblBrand.Text = "v1.0 2019 CDUT";
            // 
            // lblConfig
            // 
            this.lblConfig.AutoSize = true;
            this.lblConfig.BackColor = System.Drawing.Color.Transparent;
            this.lblConfig.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblConfig.Font = new System.Drawing.Font("楷体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblConfig.Location = new System.Drawing.Point(37, 387);
            this.lblConfig.Name = "lblConfig";
            this.lblConfig.Size = new System.Drawing.Size(130, 24);
            this.lblConfig.TabIndex = 7;
            this.lblConfig.Text = "服务器配置";
            this.lblConfig.Click += new System.EventHandler(this.lblConfig_Click);
            // 
            // lklWebsite
            // 
            this.lklWebsite.AutoSize = true;
            this.lklWebsite.BackColor = System.Drawing.Color.Transparent;
            this.lklWebsite.Font = new System.Drawing.Font("宋体", 10.5F);
            this.lklWebsite.Location = new System.Drawing.Point(466, 393);
            this.lklWebsite.Name = "lklWebsite";
            this.lklWebsite.Size = new System.Drawing.Size(125, 18);
            this.lklWebsite.TabIndex = 8;
            this.lklWebsite.TabStop = true;
            this.lklWebsite.Text = "www.qhsddy.cn";
            this.lklWebsite.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lklWebsite_LinkClicked);
            // 
            // LoginForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(782, 435);
            this.Controls.Add(this.lklWebsite);
            this.Controls.Add(this.lblConfig);
            this.Controls.Add(this.lblBrand);
            this.Controls.Add(this.prgLoading);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.chkRememberMe);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblUser);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.txtUser);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(800, 900);
            this.MinimumSize = new System.Drawing.Size(800, 47);
            this.Name = "LoginForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "管理员登录";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoginForm_FormClosing);
            this.Load += new System.EventHandler(this.Login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblPassword;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.CheckBox chkRememberMe;
        private System.Windows.Forms.ProgressBar prgLoading;
        private System.Windows.Forms.Label lblBrand;
        private System.Windows.Forms.Label lblConfig;
        private System.Windows.Forms.LinkLabel lklWebsite;
    }
}