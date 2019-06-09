namespace QingHaiGeo
{
    partial class SettingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.lblServer = new System.Windows.Forms.Label();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblDbServer = new System.Windows.Forms.Label();
            this.txtDbServer = new System.Windows.Forms.TextBox();
            this.lblPort = new System.Windows.Forms.Label();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.lblDbPort = new System.Windows.Forms.Label();
            this.txtDbPort = new System.Windows.Forms.TextBox();
            this.lblDbPassword = new System.Windows.Forms.Label();
            this.txtDbPassword = new System.Windows.Forms.TextBox();
            this.lblDbUser = new System.Windows.Forms.Label();
            this.txtDbUser = new System.Windows.Forms.TextBox();
            this.chkRememberDbPassword = new System.Windows.Forms.CheckBox();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblServer.Location = new System.Drawing.Point(36, 24);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(154, 24);
            this.lblServer.TabIndex = 4;
            this.lblServer.Text = "服务器地址：";
            // 
            // txtServer
            // 
            this.txtServer.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtServer.Location = new System.Drawing.Point(36, 55);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(296, 34);
            this.txtServer.TabIndex = 1;
            // 
            // lblDbServer
            // 
            this.lblDbServer.AutoSize = true;
            this.lblDbServer.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDbServer.Location = new System.Drawing.Point(36, 113);
            this.lblDbServer.Name = "lblDbServer";
            this.lblDbServer.Size = new System.Drawing.Size(178, 24);
            this.lblDbServer.TabIndex = 6;
            this.lblDbServer.Text = "数据库服务器：";
            // 
            // txtDbServer
            // 
            this.txtDbServer.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDbServer.Location = new System.Drawing.Point(36, 143);
            this.txtDbServer.Name = "txtDbServer";
            this.txtDbServer.Size = new System.Drawing.Size(296, 34);
            this.txtDbServer.TabIndex = 3;
            // 
            // lblPort
            // 
            this.lblPort.AutoSize = true;
            this.lblPort.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPort.Location = new System.Drawing.Point(433, 24);
            this.lblPort.Name = "lblPort";
            this.lblPort.Size = new System.Drawing.Size(154, 24);
            this.lblPort.TabIndex = 8;
            this.lblPort.Text = "服务器端口：";
            // 
            // txtPort
            // 
            this.txtPort.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtPort.Location = new System.Drawing.Point(433, 55);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(296, 34);
            this.txtPort.TabIndex = 2;
            // 
            // lblDbPort
            // 
            this.lblDbPort.AutoSize = true;
            this.lblDbPort.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDbPort.Location = new System.Drawing.Point(433, 113);
            this.lblDbPort.Name = "lblDbPort";
            this.lblDbPort.Size = new System.Drawing.Size(154, 24);
            this.lblDbPort.TabIndex = 10;
            this.lblDbPort.Text = "数据库端口：";
            // 
            // txtDbPort
            // 
            this.txtDbPort.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDbPort.Location = new System.Drawing.Point(433, 143);
            this.txtDbPort.Name = "txtDbPort";
            this.txtDbPort.Size = new System.Drawing.Size(296, 34);
            this.txtDbPort.TabIndex = 4;
            // 
            // lblDbPassword
            // 
            this.lblDbPassword.AutoSize = true;
            this.lblDbPassword.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDbPassword.Location = new System.Drawing.Point(433, 202);
            this.lblDbPassword.Name = "lblDbPassword";
            this.lblDbPassword.Size = new System.Drawing.Size(154, 24);
            this.lblDbPassword.TabIndex = 14;
            this.lblDbPassword.Text = "数据库密码：";
            // 
            // txtDbPassword
            // 
            this.txtDbPassword.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDbPassword.Location = new System.Drawing.Point(433, 231);
            this.txtDbPassword.Name = "txtDbPassword";
            this.txtDbPassword.PasswordChar = '*';
            this.txtDbPassword.ShortcutsEnabled = false;
            this.txtDbPassword.Size = new System.Drawing.Size(296, 34);
            this.txtDbPassword.TabIndex = 6;
            // 
            // lblDbUser
            // 
            this.lblDbUser.AutoSize = true;
            this.lblDbUser.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDbUser.Location = new System.Drawing.Point(36, 202);
            this.lblDbUser.Name = "lblDbUser";
            this.lblDbUser.Size = new System.Drawing.Size(178, 24);
            this.lblDbUser.TabIndex = 12;
            this.lblDbUser.Text = "数据库用户名：";
            // 
            // txtDbUser
            // 
            this.txtDbUser.Font = new System.Drawing.Font("KaiTi", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDbUser.Location = new System.Drawing.Point(36, 231);
            this.txtDbUser.Name = "txtDbUser";
            this.txtDbUser.Size = new System.Drawing.Size(296, 34);
            this.txtDbUser.TabIndex = 5;
            // 
            // chkRememberDbPassword
            // 
            this.chkRememberDbPassword.AutoSize = true;
            this.chkRememberDbPassword.Font = new System.Drawing.Font("KaiTi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chkRememberDbPassword.Location = new System.Drawing.Point(36, 298);
            this.chkRememberDbPassword.Name = "chkRememberDbPassword";
            this.chkRememberDbPassword.Size = new System.Drawing.Size(171, 24);
            this.chkRememberDbPassword.TabIndex = 7;
            this.chkRememberDbPassword.Text = "记住数据库密码";
            this.chkRememberDbPassword.UseVisualStyleBackColor = true;
            this.chkRememberDbPassword.CheckedChanged += new System.EventHandler(this.chkRememberDbPassword_CheckedChanged);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("SimSun", 13.8F);
            this.btnApply.Location = new System.Drawing.Point(463, 361);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(127, 48);
            this.btnApply.TabIndex = 8;
            this.btnApply.Text = "确定";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("SimSun", 13.8F);
            this.btnCancel.Location = new System.Drawing.Point(602, 361);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 48);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.chkRememberDbPassword);
            this.Controls.Add(this.lblDbPassword);
            this.Controls.Add(this.txtDbPassword);
            this.Controls.Add(this.lblDbUser);
            this.Controls.Add(this.txtDbUser);
            this.Controls.Add(this.lblDbPort);
            this.Controls.Add(this.txtDbPort);
            this.Controls.Add(this.lblPort);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.lblDbServer);
            this.Controls.Add(this.txtDbServer);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.txtServer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingForm";
            this.Text = "Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingForm_FormClosing);
            this.Load += new System.EventHandler(this.Setting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblServer;
        private System.Windows.Forms.TextBox txtServer;
        private System.Windows.Forms.Label lblDbServer;
        private System.Windows.Forms.TextBox txtDbServer;
        private System.Windows.Forms.Label lblPort;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label lblDbPort;
        private System.Windows.Forms.TextBox txtDbPort;
        private System.Windows.Forms.Label lblDbPassword;
        private System.Windows.Forms.TextBox txtDbPassword;
        private System.Windows.Forms.Label lblDbUser;
        private System.Windows.Forms.TextBox txtDbUser;
        private System.Windows.Forms.CheckBox chkRememberDbPassword;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnCancel;
    }
}