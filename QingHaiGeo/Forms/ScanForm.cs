using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Drawing;

namespace QingHaiGeo
{
    public partial class ScanForm : Form
    {
        private static ScanForm instance;
        public static ScanForm Instance
        {
            get
            {
                if (ScanForm.instance == null)
                    ScanForm.instance = new ScanForm();
                return ScanForm.instance;
            }
        }

        //扫描线程和入库线程
        private Thread scanThread = null, storeThread = null;
        //被扫描到的遗迹
        private Dictionary<string, IRelicMediaResource> relics = new Dictionary<string, IRelicMediaResource>();
        //被扫描到的地质科普
        private Dictionary<string, IRelicMediaResource> knowledges = new Dictionary<string, IRelicMediaResource>();

        private ScanForm()
        {
            InitializeComponent();
            this.colPath.Text = "目录";
            this.colPath.Width = 200;
            this.colCode.Text = "编号";
            this.colCode.Width = 140;
            this.colName.Text = "名称";
            this.colName.Width = 120;
            this.colTypeCode.Text = "类型代码";
            this.colTypeCode.Width = 100;
            this.colType.Text = "类型名称";
            this.colType.Width = 200;
            this.colLocation.Text = "遗迹坐标";
            this.colLocation.Width = 200;
            this.colIntro.Text = "遗迹介绍";
            this.colIntro.Width = 100;
            this.colTrait.Text = "特征";
            this.colTrait.Width = 100;
            this.colError.Text = "错误内容";
            this.colError.Width = 400;
        }
        #region 扫描作业，在工作线程运行
        /// <summary>
        /// 从一个目录扫描遗迹
        /// </summary>
        /// <param name="parent">遗迹目录们所在的目录</param>
        private void ScanRelics(DirectoryInfo parent)
        {
            var relicDirs = parent.EnumerateDirectories("*", SearchOption.TopDirectoryOnly);
            RelicType[] relicTypes = WebAPI.GetRelicTypes();
            if (relicTypes == null)
            {
                MessageBox.Show("获取数据失败！请检查服务器连接！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int i = -1, successCount = 0;
            foreach (DirectoryInfo relicDir in relicDirs)
            {
                ++i;
                SetStatusTipTextCrossThread(relicDir.FullName);
                //元数据文件
                FileInfo dataFile = new FileInfo(relicDir.FullName + "\\data.txt");
                if (!dataFile.Exists)
                {
                    this.BeginInvoke(new Action<string, Relic, string, int>(InsertOneRelic),
                        relicDir.FullName, null, "缺失data.txt文件", i - successCount);
                    continue;
                }
                Relic relic = new Relic();
                //读取遗迹编号
                relic.code = Config.ReadIniItem(dataFile.FullName, "遗迹信息", "编号").Trim();
                if (relic.code == string.Empty)
                {
                    InsertOneRelic(relicDir.FullName, null, "缺失遗迹编号");
                    continue;
                }
                //读取遗迹名称
                relic.name = Config.ReadIniItem(dataFile.FullName, "遗迹信息", "名称");
                if (relic.name == string.Empty)
                {
                    this.Invoke(new Action<string, Relic, string, int>(InsertOneRelic),
                        relicDir.FullName, null, "缺失遗迹名称", i - successCount);
                    continue;
                }
                //读取遗迹类型
                relic.relicTypeCode = Config.ReadIniItem(dataFile.FullName, "遗迹信息", "类型代码").Trim();
                if (relic.relicTypeCode == string.Empty)
                {
                    string typeName = Config.ReadIniItem(dataFile.FullName, "遗迹信息", "类型名称").Trim();
                    foreach (var relicType in relicTypes)
                    {
                        if (relicType.category == typeName)
                        {
                            relic.relicTypeCode = relicType.code;
                            relic.relicType = relicType;
                            break;
                        }
                    }
                    if (relic.relicTypeCode == string.Empty)
                    {
                        this.BeginInvoke(new Action<string, Relic, string, int>(InsertOneRelic),
                            relicDir.FullName, relic, "遗迹类型缺失", i - successCount);
                        continue;
                    }
                } //根据类型名称获取遗迹类型
                else
                {
                    foreach (var relicType in relicTypes)
                    {
                        if (relicType.code == relic.relicTypeCode)
                        {
                            relic.relicType = relicType;
                            break;
                        }
                    }
                    if (relic.relicType == null)
                    {
                        this.BeginInvoke(new Action<string, Relic, string, int>(InsertOneRelic),
                            relicDir.FullName, relic, "遗迹类型无效", i - successCount);
                        continue;
                    }
                } //根据类型代码取得遗迹类型
                relic.location = new Location();
                string longitude = Config.ReadIniItem(dataFile.FullName, "遗迹信息", "经度").Trim();
                string latitude = Config.ReadIniItem(dataFile.FullName, "遗迹信息", "纬度").Trim();
                string altitude = Config.ReadIniItem(dataFile.FullName, "遗迹信息", "高程").Trim();
                byte flag = 1;
                flag &= Convert.ToByte(TryParseDegree(longitude, out relic.location.longitude));
                flag &= Convert.ToByte(TryParseDegree(latitude, out relic.location.latitude));
                flag &= Convert.ToByte(Double.TryParse(altitude, out relic.location.altitude));
                if (flag == 0)
                {
                    this.BeginInvoke(new Action<string, Relic, string, int>(InsertOneRelic),
                        relicDir.FullName, relic, "位置信息有误", i - successCount);
                    continue;
                }
                try
                {
                    Stream stream = new FileInfo(relicDir.FullName + "\\introduction.txt").OpenRead();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    stream.Close();
                    if (Utility.IsUtf8(buffer))
                    {
                        //去除UTF-8的BOM
                        if (buffer.Length >= 3 && buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
                            relic.description = Encoding.UTF8.GetString(buffer, 3, buffer.Length - 3);
                        else
                            relic.description = Encoding.UTF8.GetString(buffer);
                    }
                    else
                        relic.description = Encoding.Default.GetString(buffer);
                }
                catch
                {
                    this.BeginInvoke(new Action<string, Relic, string, int>(InsertOneRelic),
                        relicDir.FullName, relic, "遗迹介绍缺失", i - successCount);
                    continue;
                }
                this.BeginInvoke(new Action<string, Relic, string, int>(InsertOneRelic),
                    relicDir.FullName, relic, null, 0);
                ++successCount;
            }
        }
        /// <summary>
        /// 从一个目录扫描遗迹
        /// </summary>
        /// <param name="parent">地质科普目录所在的目录</param>
        private void ScanKnowledges(DirectoryInfo parent)
        {
            var knowledgeDirs = parent.EnumerateDirectories("*", SearchOption.TopDirectoryOnly);
            RelicType[] relicTypes = WebAPI.GetRelicTypes();
            if (relicTypes == null)
            {
                MessageBox.Show("获取数据失败！请检查服务器连接！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int i = -1, successCount = 0;
            foreach (DirectoryInfo knowledgeDir in knowledgeDirs)
            {
                SetStatusTipTextCrossThread(knowledgeDir.FullName);
                Knowledge knowledge = new Knowledge();
                knowledge.code = knowledgeDir.Name;
                //根据类型代码取得遗迹类型
                foreach (var relicType in relicTypes)
                {
                    if (relicType.code == knowledge.code)
                    {
                        knowledge.name = relicType.category;
                        break;
                    }
                }
                if (knowledge.name == null)
                {
                    this.BeginInvoke(new Action<string, Knowledge, string, int>(InsertOneKnowledge),
                        knowledgeDir.FullName, knowledge, "编号无效，请确保遗迹类型存在", i - successCount);
                    continue;
                }
                //读取地质特征文件
                try
                {
                    Stream stream = new FileInfo(knowledgeDir.FullName + "\\trait.txt").OpenRead();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    stream.Close();
                    if (Utility.IsUtf8(buffer))
                    {
                        //去除UTF-8的BOM
                        if (buffer.Length >= 3 && buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
                            knowledge.trait = Encoding.UTF8.GetString(buffer, 3, buffer.Length - 3);
                        else
                            knowledge.trait = Encoding.UTF8.GetString(buffer);
                    }
                    else
                        knowledge.trait = Encoding.Default.GetString(buffer);
                }
                catch
                {
                    this.BeginInvoke(new Action<string, Knowledge, string, int>(InsertOneKnowledge),
                        knowledgeDir.FullName, knowledge, "读取地质特征失败", i - successCount);
                    continue;
                }
                //读取简介文件
                try
                {
                    Stream stream = new FileInfo(knowledgeDir.FullName + "\\introduction.txt").OpenRead();
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, (int)stream.Length);
                    stream.Close();
                    if (Utility.IsUtf8(buffer))
                    {
                        //去除UTF-8的BOM
                        if (buffer.Length >= 3 && buffer[0] == 0xEF && buffer[1] == 0xBB && buffer[2] == 0xBF)
                            knowledge.description = Encoding.UTF8.GetString(buffer, 3, buffer.Length - 3);
                        else
                            knowledge.description = Encoding.UTF8.GetString(buffer);
                    }
                    else
                        knowledge.trait = Encoding.Default.GetString(buffer);
                }
                catch
                {
                    this.BeginInvoke(new Action<string, Knowledge, string, int>(InsertOneKnowledge),
                        knowledgeDir.FullName, knowledge, "读取简介失败", i - successCount);
                    continue;
                }
                this.BeginInvoke(new Action<string, Knowledge, string, int>(InsertOneKnowledge),
                        knowledgeDir.FullName, knowledge, null, 0);
            }
        }
        /// <summary>
        /// 向列表中插入一条遗迹数据
        /// </summary>
        /// <param name="path">遗迹目录</param>
        /// <param name="relic">遗迹对象</param>
        /// <param name="err">错误文本，如果不为null表示扫描出错，将不被入库</param>
        /// <param name="index">插入的位置索引，仅有错误时生效</param>
        private void InsertOneRelic(string path, Relic relic, string err, int index = 0)
        {
            ListViewItem item;
            if (err == null && index >= 0 && index <= lvwList.Items.Count)
                item = lvwList.Items.Add(path);
            else
                item = lvwList.Items.Insert(index, path);
            item.SubItems.Add(relic == null ? "" : relic.code);
            item.SubItems.Add(relic == null ? "" : relic.name);
            item.SubItems.Add(relic == null ? "" : relic.relicTypeCode);
            item.SubItems.Add(relic == null ? "" : relic.relicType.category);
            string location = "";
            if (relic != null)
            {
                location += relic.location.latitude < 0 ? "S" : "N";
                location += Math.Abs(relic.location.latitude).ToString();
                location += ",";
                location += relic.location.longitude < 0 ? "W" : "E";
                location += Math.Abs(relic.location.longitude).ToString();
                location += ",";
                location += relic.location.altitude + "m";
            }
            item.SubItems.Add(location);
            item.SubItems.Add(relic == null ? "" : relic.description);
            item.SubItems.Add(err ?? "");
            if (err == null)
                this.relics.Add(path, relic);
            else
                item.ForeColor = Color.Blue;
        }
        /// <summary>
        /// 向列表中插入一条地质科普数据
        /// </summary>
        /// <param name="path">地质科普目录</param>
        /// <param name="knowledge">地质科普对象</param>
        /// <param name="err">错误文本，如果不为null表示扫描出错，将不被入库</param>
        /// <param name="index">插入的位置索引，仅有错误时生效</param>
        private void InsertOneKnowledge(string path, Knowledge knowledge, string err, int index = 0)
        {
            ListViewItem item;
            if (err != null && index >= 0 && index <= lvwList.Items.Count)
                item = lvwList.Items.Insert(index, path);
            else
                item = lvwList.Items.Add(path);
            item.SubItems.Add(knowledge == null ? "" : knowledge.code);
            item.SubItems.Add(knowledge == null ? "" : knowledge.name);
            item.SubItems.Add(knowledge == null ? "" : knowledge.description);
            item.SubItems.Add(knowledge == null ? "" : knowledge.trait);
            item.SubItems.Add(err ?? "");
            if (err == null)
                this.knowledges.Add(path, knowledge);
        }
        #endregion
        #region 入库作业，在工作线程运行
        private void StoreObjects(TargetType targetType)
        {
            if (targetType != TargetType.relic && targetType != TargetType.knowledge)
                throw new ArgumentException("只能入库遗迹和地质科普");

            SetStatusTipTextCrossThread("正在测试数据库连接...");
            if (!WebAPI.TestDatabase())
            {
                EndStore("连接数据库失败！");
                return;
            }
            int i = 0;
            Dictionary<string, IRelicMediaResource> objects = targetType == TargetType.relic ? relics : knowledges;
            foreach (var pair in objects)
            {
                ++i;
                IRelicMediaResource obj = pair.Value;
                DirectoryInfo objPath = new DirectoryInfo(pair.Key);
                if (!objPath.Exists)
                {
                    SetListItemErrorText(pair.Key, "目录不存在");
                    continue;
                }
                bool hasExist = false;
                if (targetType == TargetType.relic)
                    hasExist = WebAPI.IsRelicExist(obj.code);
                else if (targetType == TargetType.knowledge)
                    hasExist = WebAPI.IsKnowledgeExist(obj.code);
                if (hasExist)
                {
                    if (targetType == TargetType.relic)
                        SetListItemErrorText(pair.Key, "遗迹已存在");
                    else if (targetType == TargetType.knowledge)
                        SetListItemErrorText(pair.Key, "地质科普已存在");
                    continue;
                }
                //上传背景音乐
                FileInfo musicFile = new FileInfo(objPath.FullName + "\\bgm.mp3");
                if (musicFile.Exists)
                {
                    SetStatusTipTextCrossThread(i + " / " + objects.Count + ", 正在入库..." + obj.code + "...bgm.mp3");
                    var istream = musicFile.OpenRead();
                    obj.music = WebAPI.StoreFile(istream);
                    istream.Close();
                    if (obj.music == null)
                    {
                        SetListItemErrorText(pair.Key, "bgm.mp3入库失败");
                        continue;
                    }
                }
                //上传图片
                DirectoryInfo picPath = new DirectoryInfo(objPath.FullName + "\\photo");
                if (picPath.Exists)
                {
                    bool flag = true;
                    List<string> picIds = new List<string>();
                    foreach (FileInfo picFile in picPath.EnumerateFiles())
                    {
                        SetStatusTipTextCrossThread(i + " / " + relics.Count + ", 正在入库..." + picFile.Name);
                        flag = WebAPI.StorePicture(picFile, out string picId);
                        if (picId != null)
                            picIds.Add(picId);
                        if (!flag) break;
                    }
                    obj.pictures = picIds.ToArray();
                    if (!flag)
                    {
                        SetListItemErrorText(pair.Key, "图片入库失败");
                        WebAPI.DeleteObject(obj);
                        continue;
                    }
                }
                //上传视频
                DirectoryInfo videoPath = new DirectoryInfo(objPath.FullName + "\\video");
                if (videoPath.Exists)
                {
                    bool flag = true;
                    List<VideoInfo> videos = new List<VideoInfo>();
                    foreach (FileInfo videoFile in videoPath.EnumerateFiles("*.mp4"))
                    {
                        SetStatusTipTextCrossThread(i + " / " + relics.Count + ", 正在入库..." + videoFile.Name);
                        flag = WebAPI.StoreVideo(videoFile, out VideoInfo videoInfo);
                        if (videoInfo != null)
                            videos.Add(videoInfo);
                        if (!flag)
                            break;
                    }
                    obj.videos = videos.ToArray();
                    if (!flag)
                    {
                        SetListItemErrorText(pair.Key, "视频入库失败");
                        WebAPI.DeleteObject(obj);
                        continue;
                    }
                }
                bool success = false;
                IRelicMediaResource result = null;
                if (targetType == TargetType.relic)
                    result = WebAPI.StoreRelic(obj as Relic, out success);
                else if (targetType == TargetType.knowledge)
                    result = WebAPI.StoreKnowledge(obj as Knowledge, out success);
                if (!success && result == null)
                {
                    SetListItemErrorText(pair.Key, "入库失败");
                    WebAPI.DeleteObject(obj);
                }
                else if (!success)
                {
                    string targetName = "";
                    if (targetType == TargetType.relic)
                        targetName = "遗迹";
                    else if (targetType == TargetType.knowledge)
                        targetName = "地质科普";
                    SetListItemErrorText(pair.Key, targetName + "已存在");
                    WebAPI.DeleteObject(obj);
                }
                else
                    SetListItemSuccessColor(pair.Key);
            }
            objects.Clear();
            EndStore("入库完毕。");
        }
        // 从工作线程调用，结束入库
        private void EndStore(string tip)
        {
            this.Invoke(new Action(delegate ()
            {
                if (tip != null)
                    MessageBox.Show(tip, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = "";
                cmbScanType.Enabled = txtScanPath.Enabled = btnBrowse.Enabled =
                btnScan.Enabled = btnStore.Enabled = true;
                prgStore.Visible = false;
                prgStore.Style = ProgressBarStyle.Blocks;
                this.storeThread = null;
            }));
        }
        /// <summary>
        /// 入库失败后，从工作线程调用主线程，设置一条项目的错误文本
        /// </summary>
        /// <param name="path">遗迹目录/地质科普目录</param>
        /// <param name="err">错误文本</param>
        private void SetListItemErrorText(string path, string err)
        {
            this.Invoke(new Action(delegate ()
            {
                var item = lvwList.FindItemWithText(path);
                if (item == null || item.SubItems.Count == 0)
                    return;
                item.SubItems[item.SubItems.Count - 1].Text = err;
                item.ForeColor = Color.Red;
            }));
        }
        /// <summary>
        /// 入库成功后，将列表项背景颜色设为绿色
        /// </summary>
        /// <param name="path">遗迹目录/地质科普目录</param>
        private void SetListItemSuccessColor(string path)
        {
            this.BeginInvoke(new Action(delegate ()
            {
                var item = lvwList.FindItemWithText(path);
                if (item != null)
                    item.ForeColor = Color.Green;
            }));
        }
        #endregion
        //从工作线程调用主线程，设置状态栏提示文本
        private void SetStatusTipTextCrossThread(string text)
        {
            this.Invoke(new Action(delegate ()
            {
                lblStatus.Text = text;
            }));
        }
        //格式化度数
        private bool TryParseDegree(string degree, out double result)
        {
            result = 0;
            if (degree == null || degree == string.Empty)
                return false;
            if (Double.TryParse(degree, out result))
                return true;
            degree = degree.Replace('°', '|').Replace('′', '|').Replace('″', '|');
            string[] degrees = degree.Split('|');
            if (degrees.Length != 3)
                return false;
            try
            {
                result = Int32.Parse(degrees[0]);
                result += Int32.Parse(degrees[1]) / 60.0;
                result += Double.Parse(degrees[1]) / 3600.0;
                return true;
            }
            catch { return false; }
        }
        //退出窗口时调用
        private void DoExit(bool noRetention)
        {
            if (this.storeThread != null)
            {
                DialogResult res =
                    MessageBox.Show("正在入库，终止入库可能会发生异常错误，是否继续？", "警告", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (res == DialogResult.Yes)
                {
                    this.storeThread.Abort();
                    cmbScanType.Enabled = txtScanPath.Enabled = btnBrowse.Enabled =
                    btnScan.Enabled = btnStore.Enabled = true;
                    lblStatus.Text = "";
                    prgStore.Visible = false;
                    prgStore.Style = ProgressBarStyle.Blocks;
                    this.storeThread = null;
                }
                if (!noRetention) return;
            }
            if (this.storeThread != null)
            {
                if (DialogResult.OK !=
                MessageBox.Show("当前正在进行扫描，确定要结束吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk))
                    return;
                this.scanThread.Abort();
                cmbScanType.Enabled = txtScanPath.Enabled = btnBrowse.Enabled =
                    btnScan.Enabled = btnStore.Enabled = true;
                lblStatus.Text = "";
                this.storeThread = null;
                if (!noRetention) return;
            }
            lvwList.Items.Clear();
            this.Hide();
            MainForm.Instance.Show();
            MainForm.Instance.Focus();
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            if (this.scanThread != null || this.storeThread != null)
            {
                MessageBox.Show("当前已有工作正在进行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (txtScanPath.Text == string.Empty)
            {
                MessageBox.Show("请选择扫描路径！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txtScanPath.Text.IndexOfAny(Path.GetInvalidPathChars()) != -1)
            {
                MessageBox.Show("扫描路径包含非法字符！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DirectoryInfo parent = new DirectoryInfo(txtScanPath.Text);
            if (!parent.Exists || (parent.Attributes & FileAttributes.Directory) == 0)
            {
                MessageBox.Show("所选目录不存在！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            lvwList.Items.Clear();
            this.relics.Clear();
            this.knowledges.Clear();
            cmbScanType.Enabled = txtScanPath.Enabled = btnBrowse.Enabled =
            btnScan.Enabled = btnStore.Enabled = false;
            bool relicFlag = cmbScanType.SelectedIndex == 0;
            this.scanThread = new Thread(new ThreadStart(delegate ()
            {
                if (relicFlag)
                    ScanRelics(parent);
                else
                    ScanKnowledges(parent);
                this.scanThread = null;
                this.Invoke(new Action(delegate ()
                {
                    cmbScanType.Enabled = txtScanPath.Enabled = btnBrowse.Enabled =
                    btnScan.Enabled = btnStore.Enabled = true;
                    lblStatus.Text = "";
                }));
            }));
            this.scanThread.Start();
        }
        private void btnStore_Click(object sender, EventArgs e)
        {
            if (this.scanThread != null || this.storeThread != null)
            {
                MessageBox.Show("当前已有工作正在进行！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if ((cmbScanType.SelectedIndex == 0 && this.relics.Count == 0) ||
                (cmbScanType.SelectedIndex == 1 && this.knowledges.Count == 0))
            {
                MessageBox.Show("没有可入库的对象！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            DialogResult res =
                MessageBox.Show("入库将会耗费大量时间，请保持网络畅通，耐心等待。是否立即开始？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
            if (res != DialogResult.Yes)
                return;

            cmbScanType.Enabled = txtScanPath.Enabled = btnBrowse.Enabled =
            btnScan.Enabled = btnStore.Enabled = false;
            prgStore.Visible = true;
            prgStore.Style = ProgressBarStyle.Marquee;
            lblStatus.Text = "正在测试ffmpeg...";
            if (!Utility.TestFFmpeg())
            {
                MessageBox.Show("ffmpeg未配置，无法进行视频压缩。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblStatus.Text = "";
                EndStore(null);
                return;
            }
            TargetType targetType;
            if (cmbScanType.SelectedIndex == 0)
                targetType = TargetType.relic;
            else
                targetType = TargetType.knowledge;
            this.storeThread = new Thread(delegate ()
            {
                StoreObjects(targetType);
            });
            this.storeThread.Start();
        }
        private void ScanForm_VisibleChanged(object sender, EventArgs e)
        {
            if (!this.Visible)
                return;
            this.relics.Clear();
            this.knowledges.Clear();
            cmbScanType.SelectedIndex = 0;
            btnStore.Enabled = false;
            txtScanPath.Text = "";
            lblStatus.Text = "";
            prgStore.Visible = false;
        }
        private void cmbScanType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvwList.Clear();
            this.relics.Clear();
            this.knowledges.Clear();
            btnStore.Enabled = false;
            if (cmbScanType.SelectedIndex == 0)
            {
                this.lvwList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
                {
                this.colPath,
                this.colCode,
                this.colName,
                this.colTypeCode,
                this.colType,
                this.colLocation,
                this.colIntro,
                this.colError
                });
            }
            else
            {
                this.lvwList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[]
                {
                this.colPath,
                this.colCode,
                this.colName,
                this.colIntro,
                this.colTrait,
                this.colError
                });
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (this.fbdBrowsePath.ShowDialog() == DialogResult.OK)
                this.txtScanPath.Text = fbdBrowsePath.SelectedPath;
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DoExit(false);
        }
        private void ScanForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            DoExit(true);
        }
        private void txtScanPath_MouseHover(object sender, EventArgs e)
        {
            lblStatus.Text = "选择" + cmbScanType.Text + "目录所在的目录";
        }
        private void txtScanPath_MouseLeave(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }
        private void btnBrowse_MouseHover(object sender, EventArgs e)
        {
            lblStatus.Text = "选择" + cmbScanType.Text + "目录所在的目录";
        }
        private void btnBrowse_MouseLeave(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }
        private void cmbScanType_MouseHover(object sender, EventArgs e)
        {
            lblStatus.Text = "选择要扫描的对象";
        }
        private void cmbScanType_MouseLeave(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }
        private void btnScan_MouseHover(object sender, EventArgs e)
        {
            lblStatus.Text = "扫描所选目录中的" + cmbScanType.Text;
        }
        private void btnScan_MouseLeave(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }
        private void btnStore_MouseHover(object sender, EventArgs e)
        {
            lblStatus.Text = "将列表中的" + cmbScanType.Text + "入库，但有错误的条目将不被入库";
        }
        private void btnStore_MouseLeave(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }
        private void btnCancel_MouseHover(object sender, EventArgs e)
        {
            if (storeThread != null)
                lblStatus.Text = "停止本次入库操作";
            else if (scanThread != null)
                lblStatus.Text = "取消本次扫描操作";
            else
                lblStatus.Text = "关闭扫描窗口";
        }
        private void btnCancel_MouseLeave(object sender, EventArgs e)
        {
            lblStatus.Text = "";
        }

        private System.Windows.Forms.ColumnHeader colPath = new ColumnHeader();
        private System.Windows.Forms.ColumnHeader colCode = new ColumnHeader();
        private System.Windows.Forms.ColumnHeader colName = new ColumnHeader();
        private System.Windows.Forms.ColumnHeader colTypeCode = new ColumnHeader();
        private System.Windows.Forms.ColumnHeader colType = new ColumnHeader();
        private System.Windows.Forms.ColumnHeader colLocation = new ColumnHeader();
        private System.Windows.Forms.ColumnHeader colIntro = new ColumnHeader();
        private System.Windows.Forms.ColumnHeader colTrait = new ColumnHeader();
        private System.Windows.Forms.ColumnHeader colError = new ColumnHeader();
    }
}
