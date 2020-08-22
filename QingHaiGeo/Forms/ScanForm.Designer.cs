namespace QingHaiGeo
{
    partial class ScanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ScanForm));
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlButton = new System.Windows.Forms.Panel();
            this.btnStore = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnScan = new System.Windows.Forms.Button();
            this.ssrStatusBar = new System.Windows.Forms.StatusStrip();
            this.prgStore = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.fbdBrowsePath = new System.Windows.Forms.FolderBrowserDialog();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.pnlPathInput = new System.Windows.Forms.Panel();
            this.txtScanPath = new System.Windows.Forms.TextBox();
            this.pnlPathLabel = new System.Windows.Forms.Panel();
            this.lblPath = new System.Windows.Forms.Label();
            this.cmbScanType = new System.Windows.Forms.ComboBox();
            this.pnlBrowseButton = new System.Windows.Forms.Panel();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.lvwList = new System.Windows.Forms.ListView();
            this.pnlBottom.SuspendLayout();
            this.pnlButton.SuspendLayout();
            this.ssrStatusBar.SuspendLayout();
            this.pnlTop.SuspendLayout();
            this.pnlPathInput.SuspendLayout();
            this.pnlPathLabel.SuspendLayout();
            this.pnlBrowseButton.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlButton);
            this.pnlBottom.Controls.Add(this.ssrStatusBar);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 636);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(1257, 91);
            this.pnlBottom.TabIndex = 1;
            // 
            // pnlButton
            // 
            this.pnlButton.Controls.Add(this.btnStore);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.btnScan);
            this.pnlButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlButton.Location = new System.Drawing.Point(787, 0);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(470, 58);
            this.pnlButton.TabIndex = 13;
            // 
            // btnStore
            // 
            this.btnStore.Font = new System.Drawing.Font("宋体", 13.8F);
            this.btnStore.Location = new System.Drawing.Point(166, 5);
            this.btnStore.Name = "btnStore";
            this.btnStore.Size = new System.Drawing.Size(127, 48);
            this.btnStore.TabIndex = 4;
            this.btnStore.Text = "入库";
            this.btnStore.UseVisualStyleBackColor = true;
            this.btnStore.Click += new System.EventHandler(this.btnStore_Click);
            this.btnStore.MouseLeave += new System.EventHandler(this.btnStore_MouseLeave);
            this.btnStore.MouseHover += new System.EventHandler(this.btnStore_MouseHover);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("宋体", 13.8F);
            this.btnCancel.Location = new System.Drawing.Point(313, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 48);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "关闭";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            this.btnCancel.MouseLeave += new System.EventHandler(this.btnCancel_MouseLeave);
            this.btnCancel.MouseHover += new System.EventHandler(this.btnCancel_MouseHover);
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("宋体", 13.8F);
            this.btnScan.Location = new System.Drawing.Point(20, 5);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(127, 48);
            this.btnScan.TabIndex = 3;
            this.btnScan.Text = "扫描";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            this.btnScan.MouseLeave += new System.EventHandler(this.btnScan_MouseLeave);
            this.btnScan.MouseHover += new System.EventHandler(this.btnScan_MouseHover);
            // 
            // ssrStatusBar
            // 
            this.ssrStatusBar.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssrStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prgStore,
            this.lblStatus});
            this.ssrStatusBar.Location = new System.Drawing.Point(0, 58);
            this.ssrStatusBar.Name = "ssrStatusBar";
            this.ssrStatusBar.Size = new System.Drawing.Size(1257, 33);
            this.ssrStatusBar.TabIndex = 11;
            // 
            // prgStore
            // 
            this.prgStore.ForeColor = System.Drawing.Color.SpringGreen;
            this.prgStore.Name = "prgStore";
            this.prgStore.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.prgStore.RightToLeftLayout = true;
            this.prgStore.Size = new System.Drawing.Size(155, 27);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(5, 4, 0, 4);
            this.lblStatus.Size = new System.Drawing.Size(44, 28);
            this.lblStatus.Text = "就绪";
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlPathInput);
            this.pnlTop.Controls.Add(this.pnlPathLabel);
            this.pnlTop.Controls.Add(this.pnlBrowseButton);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1257, 75);
            this.pnlTop.TabIndex = 4;
            // 
            // pnlPathInput
            // 
            this.pnlPathInput.Controls.Add(this.txtScanPath);
            this.pnlPathInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlPathInput.Location = new System.Drawing.Point(255, 22);
            this.pnlPathInput.Name = "pnlPathInput";
            this.pnlPathInput.Size = new System.Drawing.Size(887, 53);
            this.pnlPathInput.TabIndex = 8;
            // 
            // txtScanPath
            // 
            this.txtScanPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtScanPath.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtScanPath.Location = new System.Drawing.Point(0, 0);
            this.txtScanPath.Name = "txtScanPath";
            this.txtScanPath.Size = new System.Drawing.Size(887, 30);
            this.txtScanPath.TabIndex = 0;
            this.txtScanPath.MouseLeave += new System.EventHandler(this.txtScanPath_MouseLeave);
            this.txtScanPath.MouseHover += new System.EventHandler(this.txtScanPath_MouseHover);
            // 
            // pnlPathLabel
            // 
            this.pnlPathLabel.Controls.Add(this.lblPath);
            this.pnlPathLabel.Controls.Add(this.cmbScanType);
            this.pnlPathLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPathLabel.Location = new System.Drawing.Point(0, 0);
            this.pnlPathLabel.Name = "pnlPathLabel";
            this.pnlPathLabel.Size = new System.Drawing.Size(255, 75);
            this.pnlPathLabel.TabIndex = 7;
            // 
            // lblPath
            // 
            this.lblPath.AutoSize = true;
            this.lblPath.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPath.Location = new System.Drawing.Point(152, 27);
            this.lblPath.Name = "lblPath";
            this.lblPath.Size = new System.Drawing.Size(109, 20);
            this.lblPath.TabIndex = 5;
            this.lblPath.Text = "扫描路径：";
            // 
            // cmbScanType
            // 
            this.cmbScanType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbScanType.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbScanType.FormattingEnabled = true;
            this.cmbScanType.ItemHeight = 20;
            this.cmbScanType.Items.AddRange(new object[] {
            "遗迹",
            "地质科普",
            "文化村"});
            this.cmbScanType.Location = new System.Drawing.Point(23, 25);
            this.cmbScanType.Name = "cmbScanType";
            this.cmbScanType.Size = new System.Drawing.Size(115, 28);
            this.cmbScanType.TabIndex = 6;
            this.cmbScanType.SelectedIndexChanged += new System.EventHandler(this.cmbScanType_SelectedIndexChanged);
            this.cmbScanType.MouseLeave += new System.EventHandler(this.cmbScanType_MouseLeave);
            this.cmbScanType.MouseHover += new System.EventHandler(this.cmbScanType_MouseHover);
            // 
            // pnlBrowseButton
            // 
            this.pnlBrowseButton.Controls.Add(this.btnBrowse);
            this.pnlBrowseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlBrowseButton.Location = new System.Drawing.Point(1142, 0);
            this.pnlBrowseButton.Name = "pnlBrowseButton";
            this.pnlBrowseButton.Size = new System.Drawing.Size(115, 75);
            this.pnlBrowseButton.TabIndex = 5;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBrowse.Location = new System.Drawing.Point(21, 22);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(69, 30);
            this.btnBrowse.TabIndex = 2;
            this.btnBrowse.Text = "浏览";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            this.btnBrowse.MouseLeave += new System.EventHandler(this.btnBrowse_MouseLeave);
            this.btnBrowse.MouseHover += new System.EventHandler(this.btnBrowse_MouseHover);
            // 
            // lvwList
            // 
            this.lvwList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwList.Font = new System.Drawing.Font("楷体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvwList.FullRowSelect = true;
            this.lvwList.GridLines = true;
            this.lvwList.HideSelection = false;
            this.lvwList.Location = new System.Drawing.Point(0, 75);
            this.lvwList.Name = "lvwList";
            this.lvwList.Size = new System.Drawing.Size(1257, 561);
            this.lvwList.TabIndex = 3;
            this.lvwList.TabStop = false;
            this.lvwList.UseCompatibleStateImageBehavior = false;
            this.lvwList.View = System.Windows.Forms.View.Details;
            // 
            // ScanForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1257, 727);
            this.Controls.Add(this.lvwList);
            this.Controls.Add(this.pnlTop);
            this.Controls.Add(this.pnlBottom);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ScanForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "批量扫描入库";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ScanForm_FormClosing);
            this.VisibleChanged += new System.EventHandler(this.ScanForm_VisibleChanged);
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            this.ssrStatusBar.ResumeLayout(false);
            this.ssrStatusBar.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlPathInput.ResumeLayout(false);
            this.pnlPathInput.PerformLayout();
            this.pnlPathLabel.ResumeLayout(false);
            this.pnlPathLabel.PerformLayout();
            this.pnlBrowseButton.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlBottom;
        private System.Windows.Forms.FolderBrowserDialog fbdBrowsePath;
        private System.Windows.Forms.StatusStrip ssrStatusBar;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.ListView lvwList;
        private System.Windows.Forms.Panel pnlButton;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Panel pnlPathInput;
        private System.Windows.Forms.TextBox txtScanPath;
        private System.Windows.Forms.Panel pnlPathLabel;
        private System.Windows.Forms.Panel pnlBrowseButton;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Label lblPath;
        private System.Windows.Forms.Button btnStore;
        private System.Windows.Forms.ToolStripProgressBar prgStore;
        private System.Windows.Forms.ComboBox cmbScanType;
    }
}