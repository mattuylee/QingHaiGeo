using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QingHaiGeo
{
    public partial class VideoForm : Form
    {
        //要添加视频的对象的类型
        private TargetType targetType;
        //视频上传线程
        private Thread uploadThread = null;

        public VideoForm(string code, TargetType targetType)
        {
            InitializeComponent();
            this.targetType = targetType;
            this.txtCode.Text = code;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (ofdVideo.ShowDialog() == DialogResult.OK)
            {
                var item = lvwVideos.FindItemWithText(ofdVideo.FileName);
                if (item != null)
                {
                    MessageBox.Show("文件已在列表！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                lvwVideos.Items.Add(ofdVideo.FileName);
            }
        }
        private void VideoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Exit(true);
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Exit(this.uploadThread == null);
        }
        private void btnAddVideo_Click(object sender, EventArgs e)
        {
            if (!Config.IsLogged)
            {
                MessageBox.Show("您尚未登录！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoginForm.Instance.Show();
                LoginForm.Instance.Focus();
                return;
            }
            if (lvwVideos.Items.Count == 0)
            {
                MessageBox.Show("列表为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            btnAddVideo.Enabled = btnBrowse.Enabled = false;
            prgProgress.Style = ProgressBarStyle.Marquee;
            List<string> videos = new List<string>(lvwVideos.Items.Count);
            foreach (ListViewItem i in lvwVideos.Items)
                videos.Add(i.Text);
            this.uploadThread = new Thread(delegate()
            {
                UploadVideos(txtCode.Text, videos);
            });
        }

        private void UploadVideos(string code, List<string> videos)
        {
            SetStatusTipTextCrossThread("测试ffmpeg是否可用...");
            if (!Utility.TestFFmpeg())
            {
                EndUpload("ffmpeg未配置，无法进行视频压缩。");
                return;
            }
            SetStatusTipTextCrossThread("测试数据库连接...");
            if (!WebAPI.TestDatabase())
            {
                EndUpload("数据库连接失败。请检查数据库配置。");
                return;
            }
            bool exist = false;
            if (this.targetType == TargetType.relic)
                exist = WebAPI.IsRelicExist(code);
            else if (this.targetType == TargetType.knowledge)
                exist = WebAPI.IsKnowledgeExist(code);
            if (!exist)
            {
                string name;
                switch(this.targetType)
                {
                    case TargetType.relic:
                        name = "遗迹";
                        break;
                    case TargetType.knowledge:
                        name = "地质科普";
                        break;
                    default:
                        name = this.targetType.ToString();
                        break;
                }
                MessageBox.Show(name + " ID不存在");
                return;
            }
            List<VideoInfo> videoinfos = new List<VideoInfo>(videos.Count);
            for (int i = 0; i < videos.Count; ++i)
            {
                SetStatusTipTextCrossThread((i + 1).ToString() + " / " + videos.Count + ", 正在上传");
                if (WebAPI.StoreVideo(new FileInfo(videos[i]), out VideoInfo videoInfo))
                {
                    this.BeginInvoke(new Action(delegate ()
                    {
                        var item = lvwVideos.FindItemWithText(videos[i]);
                        if (item != null)
                            lvwVideos.Items.Remove(item);
                    }));
                    videoinfos.Add(videoInfo);
                }
                else
                {
                    SetStatusTipTextCrossThread("资源上传失败，正在清除垃圾数据...");
                    this.BeginInvoke(new Action(delegate ()
                    {
                        ListViewItem item = lvwVideos.FindItemWithText(videos[i]);
                        if (item != null)
                            item.ForeColor = Color.Red;
                    }));
                    // 删除已上传的部分资源
                    if (videoInfo != null)
                        WebAPI.DeleteRelic(new Relic() { videos = new VideoInfo[] { videoInfo } });
                }
            }
            SetStatusTipTextCrossThread("资源上传完毕，正在更新数据...");
            bool success = true;
            if (this.targetType == TargetType.knowledge)
                success = WebAPI.AddVideos<Knowledge>(code, videoinfos);
            else
                success = WebAPI.AddVideos<Relic>(code, videoinfos);
            if (!success)
            {
                SetStatusTipTextCrossThread("更新数据失败，正在清除数据...");
                WebAPI.DeleteRelic(new Relic() { videos = videoinfos.ToArray() });
                EndUpload("更新数据失败！任何视频都没有成功上传，请重试！");
            }
            else
                EndUpload("上传成功！");
        }
        //从工作线程调用主线程，设置状态栏提示文本
        private void SetStatusTipTextCrossThread(string text)
        {
            this.BeginInvoke(new Action(delegate ()
            {
                lblStatus.Text = text;
            }));
        }
        // 调用主线程解除上传状态
        private void EndUpload(string tip = null)
        {
            this.Invoke(new Action(delegate()
            {
                if (tip != null)
                    MessageBox.Show(tip, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnAddVideo.Enabled = btnBrowse.Enabled = true;
                prgProgress.Style = ProgressBarStyle.Blocks;
                prgProgress.Value = 0;
                this.uploadThread = null;
            }));
        }

        // 执行退出
        private void Exit(bool exit)
        {
            if (this.uploadThread != null)
            {
                DialogResult res =
                    MessageBox.Show("视频上传中，终止上传可能产生异常错误，是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    this.uploadThread.Abort();
                    btnAddVideo.Enabled = btnBrowse.Enabled = true;
                    prgProgress.Style = ProgressBarStyle.Blocks;
                    prgProgress.Value = 0;
                    lblStatus.Text = "";
                }
                if (exit)
                    this.Close();
            }
        }
    }
}
