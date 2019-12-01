namespace QingHaiGeo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnTestServer = new System.Windows.Forms.Button();
            this.btnTestDb = new System.Windows.Forms.Button();
            this.lblManage = new System.Windows.Forms.Label();
            this.lblUpload = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Location = new System.Drawing.Point(585, 387);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(127, 51);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Visible = false;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnTestServer
            // 
            this.btnTestServer.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestServer.Location = new System.Drawing.Point(99, 387);
            this.btnTestServer.Name = "btnTestServer";
            this.btnTestServer.Size = new System.Drawing.Size(199, 51);
            this.btnTestServer.TabIndex = 3;
            this.btnTestServer.Text = "测试服务器";
            this.btnTestServer.UseVisualStyleBackColor = true;
            this.btnTestServer.Visible = false;
            this.btnTestServer.Click += new System.EventHandler(this.btnTestServer_Click);
            // 
            // btnTestDb
            // 
            this.btnTestDb.Font = new System.Drawing.Font("宋体", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestDb.Location = new System.Drawing.Point(331, 387);
            this.btnTestDb.Name = "btnTestDb";
            this.btnTestDb.Size = new System.Drawing.Size(199, 51);
            this.btnTestDb.TabIndex = 4;
            this.btnTestDb.Text = "测试数据库";
            this.btnTestDb.UseVisualStyleBackColor = true;
            this.btnTestDb.Visible = false;
            this.btnTestDb.Click += new System.EventHandler(this.btnTestDb_Click);
            // 
            // lblManage
            // 
            this.lblManage.AutoSize = true;
            this.lblManage.BackColor = System.Drawing.Color.DarkCyan;
            this.lblManage.Font = new System.Drawing.Font("宋体", 16F);
            this.lblManage.ForeColor = System.Drawing.Color.White;
            this.lblManage.Location = new System.Drawing.Point(50, 143);
            this.lblManage.Name = "lblManage";
            this.lblManage.Padding = new System.Windows.Forms.Padding(8);
            this.lblManage.Size = new System.Drawing.Size(164, 43);
            this.lblManage.TabIndex = 7;
            this.lblManage.Text = " 数据管理 ";
            this.lblManage.Click += new System.EventHandler(this.lblManage_Click);
            this.lblManage.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.lblManage.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // lblUpload
            // 
            this.lblUpload.AutoSize = true;
            this.lblUpload.BackColor = System.Drawing.Color.DarkCyan;
            this.lblUpload.Font = new System.Drawing.Font("宋体", 16F);
            this.lblUpload.ForeColor = System.Drawing.Color.White;
            this.lblUpload.Location = new System.Drawing.Point(50, 217);
            this.lblUpload.Name = "lblUpload";
            this.lblUpload.Padding = new System.Windows.Forms.Padding(8);
            this.lblUpload.Size = new System.Drawing.Size(164, 43);
            this.lblUpload.TabIndex = 8;
            this.lblUpload.Text = " 批量上传 ";
            this.lblUpload.Click += new System.EventHandler(this.lblUpload_Click);
            this.lblUpload.MouseEnter += new System.EventHandler(this.label_MouseEnter);
            this.lblUpload.MouseLeave += new System.EventHandler(this.label_MouseLeave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblUpload);
            this.Controls.Add(this.lblManage);
            this.Controls.Add(this.btnTestDb);
            this.Controls.Add(this.btnTestServer);
            this.Controls.Add(this.btnLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "青海地学";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        internal System.Windows.Forms.Button btnLogin;
        internal System.Windows.Forms.Button btnTestServer;
        internal System.Windows.Forms.Button btnTestDb;
        private System.Windows.Forms.Label lblManage;
        private System.Windows.Forms.Label lblUpload;
    }
}

