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
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.btnTestServer = new System.Windows.Forms.Button();
            this.btnTestDb = new System.Windows.Forms.Button();
            this.btnManage = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnConfig
            // 
            this.btnConfig.Font = new System.Drawing.Font("SimSun", 13.8F);
            this.btnConfig.Location = new System.Drawing.Point(183, 168);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(127, 51);
            this.btnConfig.TabIndex = 0;
            this.btnConfig.Text = "配置";
            this.btnConfig.UseVisualStyleBackColor = true;
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // btnLogin
            // 
            this.btnLogin.Font = new System.Drawing.Font("SimSun", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLogin.Location = new System.Drawing.Point(335, 88);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(127, 51);
            this.btnLogin.TabIndex = 1;
            this.btnLogin.Text = "登录";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("SimSun", 13.8F);
            this.btnScan.Location = new System.Drawing.Point(487, 168);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(127, 51);
            this.btnScan.TabIndex = 2;
            this.btnScan.Text = "扫描";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // btnTestServer
            // 
            this.btnTestServer.Font = new System.Drawing.Font("SimSun", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestServer.Location = new System.Drawing.Point(183, 242);
            this.btnTestServer.Name = "btnTestServer";
            this.btnTestServer.Size = new System.Drawing.Size(199, 51);
            this.btnTestServer.TabIndex = 3;
            this.btnTestServer.Text = "测试服务器";
            this.btnTestServer.UseVisualStyleBackColor = true;
            this.btnTestServer.Click += new System.EventHandler(this.btnTestServer_Click);
            // 
            // btnTestDb
            // 
            this.btnTestDb.Font = new System.Drawing.Font("SimSun", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnTestDb.Location = new System.Drawing.Point(415, 242);
            this.btnTestDb.Name = "btnTestDb";
            this.btnTestDb.Size = new System.Drawing.Size(199, 51);
            this.btnTestDb.TabIndex = 4;
            this.btnTestDb.Text = "测试数据库";
            this.btnTestDb.UseVisualStyleBackColor = true;
            this.btnTestDb.Click += new System.EventHandler(this.btnTestDb_Click);
            // 
            // btnManage
            // 
            this.btnManage.Font = new System.Drawing.Font("SimSun", 13.8F);
            this.btnManage.Location = new System.Drawing.Point(335, 168);
            this.btnManage.Name = "btnManage";
            this.btnManage.Size = new System.Drawing.Size(127, 51);
            this.btnManage.TabIndex = 5;
            this.btnManage.Text = "管理";
            this.btnManage.UseVisualStyleBackColor = true;
            this.btnManage.Click += new System.EventHandler(this.btnManage_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnManage);
            this.Controls.Add(this.btnTestDb);
            this.Controls.Add(this.btnTestServer);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.btnConfig);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "青海地学";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnScan;
        internal System.Windows.Forms.Button btnLogin;
        internal System.Windows.Forms.Button btnTestServer;
        internal System.Windows.Forms.Button btnTestDb;
        private System.Windows.Forms.Button btnManage;
    }
}

