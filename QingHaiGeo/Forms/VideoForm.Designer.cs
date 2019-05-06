namespace QingHaiGeo
{
    partial class VideoForm
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
            this.txtCode = new System.Windows.Forms.TextBox();
            this.lblCode = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnAddVideo = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ofdVideo = new System.Windows.Forms.OpenFileDialog();
            this.sstStrip = new System.Windows.Forms.StatusStrip();
            this.prgProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lvwVideos = new System.Windows.Forms.ListView();
            this.sstStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCode
            // 
            this.txtCode.Font = new System.Drawing.Font("KaiTi", 12F);
            this.txtCode.Location = new System.Drawing.Point(126, 17);
            this.txtCode.Name = "txtCode";
            this.txtCode.ReadOnly = true;
            this.txtCode.Size = new System.Drawing.Size(331, 30);
            this.txtCode.TabIndex = 10;
            // 
            // lblCode
            // 
            this.lblCode.AutoSize = true;
            this.lblCode.Font = new System.Drawing.Font("KaiTi", 12F);
            this.lblCode.Location = new System.Drawing.Point(11, 22);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(109, 20);
            this.lblCode.TabIndex = 9;
            this.lblCode.Text = "科普编号：";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Font = new System.Drawing.Font("KaiTi", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBrowse.Location = new System.Drawing.Point(690, 13);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(125, 40);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "添加视频";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnAddVideo
            // 
            this.btnAddVideo.Font = new System.Drawing.Font("SimSun", 13.8F);
            this.btnAddVideo.Location = new System.Drawing.Point(541, 364);
            this.btnAddVideo.Name = "btnAddVideo";
            this.btnAddVideo.Size = new System.Drawing.Size(127, 51);
            this.btnAddVideo.TabIndex = 13;
            this.btnAddVideo.Text = "上传";
            this.btnAddVideo.UseVisualStyleBackColor = true;
            this.btnAddVideo.Click += new System.EventHandler(this.btnAddVideo_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Font = new System.Drawing.Font("SimSun", 13.8F);
            this.btnCancel.Location = new System.Drawing.Point(688, 364);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(127, 51);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ofdVideo
            // 
            this.ofdVideo.Filter = "视频文件 (*.mp4)|*.mp4";
            // 
            // sstStrip
            // 
            this.sstStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.sstStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.prgProgress,
            this.lblStatus});
            this.sstStrip.Location = new System.Drawing.Point(0, 424);
            this.sstStrip.Name = "sstStrip";
            this.sstStrip.Size = new System.Drawing.Size(837, 24);
            this.sstStrip.TabIndex = 15;
            this.sstStrip.Text = "statusStrip1";
            // 
            // prgProgress
            // 
            this.prgProgress.Margin = new System.Windows.Forms.Padding(10, 3, 1, 3);
            this.prgProgress.Name = "prgProgress";
            this.prgProgress.Size = new System.Drawing.Size(120, 18);
            // 
            // lblStatus
            // 
            this.lblStatus.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 19);
            // 
            // lvwVideos
            // 
            this.lvwVideos.Location = new System.Drawing.Point(15, 69);
            this.lvwVideos.Name = "lvwVideos";
            this.lvwVideos.Size = new System.Drawing.Size(800, 280);
            this.lvwVideos.TabIndex = 16;
            this.lvwVideos.UseCompatibleStateImageBehavior = false;
            // 
            // VideoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 448);
            this.Controls.Add(this.lvwVideos);
            this.Controls.Add(this.sstStrip);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAddVideo);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtCode);
            this.Controls.Add(this.lblCode);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(855, 495);
            this.MinimumSize = new System.Drawing.Size(855, 495);
            this.Name = "VideoForm";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "添加视频";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VideoForm_FormClosing);
            this.sstStrip.ResumeLayout(false);
            this.sstStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Label lblCode;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnAddVideo;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.OpenFileDialog ofdVideo;
        private System.Windows.Forms.StatusStrip sstStrip;
        private System.Windows.Forms.ToolStripProgressBar prgProgress;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ListView lvwVideos;
    }
}