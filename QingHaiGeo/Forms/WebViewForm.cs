using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QingHaiGeo
{
    [ComVisible(true)]
    public partial class WebViewForm : Form
    {
        private Thread httpThread;
        private static WebViewForm instance;
        public static WebViewForm Instance
        {
            get
            {
                if (WebViewForm.instance == null)
                {
                    WebViewForm.instance = new WebViewForm();
                }
                return WebViewForm.instance;
            }
        }
        private ChromiumWebBrowser browser;

        private WebViewForm()
        {
            InitializeComponent();
        }
        private void WebViewForm_Load(object sender, EventArgs e)
        {
            this.Visible = false;
            this.Opacity = 100;
            HttpListener listerner = new HttpListener();
            listerner.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            Config.localPort = (ushort)GetRandomPort();
            string prefix = "http://localhost:" + Config.localPort + "/";
            listerner.Prefixes.Add(prefix);
            listerner.Start();
            this.httpThread = new Thread(new ThreadStart(delegate ()
            {
                while (true)
                {
                    try
                    {
                        HttpListenerContext ctx = listerner.GetContext();
                        if (ctx.Request.HttpMethod.ToLower() != "get")
                        {
                            ctx.Response.StatusCode = 501;
                            ctx.Response.Close();
                            continue;
                        }
                        string filename = Path.GetDirectoryName(Application.ExecutablePath) + "/web" + ctx.Request.RawUrl;
                        if (!File.Exists(filename))
                        {
                            StreamWriter writer = new StreamWriter(ctx.Response.OutputStream);
                            ctx.Response.StatusCode = 404;
                            writer.WriteLine("找不到文件" + filename);
                            writer.Close();
                            ctx.Response.Close();
                            continue;
                        }
                        ctx.Response.StatusCode = 200;
                        FileStream fs = new FileStream(filename, FileMode.Open);
                        fs.CopyTo(ctx.Response.OutputStream);
                        fs.Close();
                        ctx.Response.Close();
                    }
                    catch (Exception ev) { }
                }
            }));
            this.httpThread.IsBackground = true;
            this.httpThread.Start();
            this.browser = new ChromiumWebBrowser("");
            BindingOptions option = new BindingOptions() { CamelCaseJavascriptNames = false };
            this.browser.JavascriptObjectRepository.Register("NativeObj", new DataBinding(), false, option);
            this.browser.Dock = DockStyle.Fill;
            this.browser.Parent = this;
            LoginForm.Instance.ShowDialog();
            if (!Config.IsLogged)
            {
                this.Close();
                return;
            }
            this.browser.Load(prefix + "index.html");
            this.Visible = true;
            this.browser.Show();
        }
        private static int GetRandomPort()
        {
            var random = new Random();
            var randomPort = random.Next(10000, 65535);

            while (IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners().Any(p => p.Port == randomPort))
            {
                randomPort = random.Next(10000, 65535);
            }
            return randomPort;
        }

        private void WebViewForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(this.httpThread != null)
            {
                this.httpThread.Abort();
            }
            Application.Exit();
        }
    }

    class DataBinding
    {
        // 确保登录后才允许打开添加视频窗口
        private bool ShouldOpenVideoWindow()
        {
            if (Config.IsLogged)
            {
                return true;
            }
            var res =
                MessageBox.Show("请先登录！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res != DialogResult.OK)
                return false;
            LoginForm.Instance.ShowDialog();
            return Config.IsLogged;
        }
        // 检查是否正在上传数据
        private bool CheckUploading()
        {
            if (ScanForm.Instance.Visible)
            {
                MessageBox.Show("当前正在上传数据，请终止后继续。", "批量上传", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public string Login()
        {
            if (Config.IsLogged)
                return Config.CurrentUser.id;
            WebViewForm.Instance.Invoke(new Action(() =>
            {
                LoginForm.Instance.ShowDialog();
            }));
            if (Config.IsLogged)
                return Config.CurrentUser.id;
            else
                return null;
        }
        public void AddRelicVideo(string code)
        {
            WebViewForm.Instance.Invoke(new Action(() =>
            {
                if (ShouldOpenVideoWindow())
                    new VideoForm(code, TargetType.relic).ShowDialog();
            }));
        }
        public void AddKnowledgeVideo(string code)
        {
            WebViewForm.Instance.Invoke(new Action(() =>
            {
                if (ShouldOpenVideoWindow())
                    new VideoForm(code, TargetType.knowledge).ShowDialog();
            }));
        }
        public void AddVillageVideo(string code)
        {
            WebViewForm.Instance.Invoke(new Action(() =>
            {
                if (Config.IsLogged)
                    new VideoForm(code, TargetType.village).ShowDialog();
            }));
        }
        public string GetBaseUrl()
        {
            return Config.Server + ':' + Config.Port;
        }
        //@Deprecated
        public string GetBaseRoute()
        {
            return Application.StartupPath + "\\web";
        }
        public string DeleteRelic(string code)
        {
            Relic relic = WebAPI.GetRelic(code);
            if (relic == null)
                return "遗迹不存在";
            WebAPI.DeleteRelic(relic);
            return null;
        }
        public string UpdateRelic(string jRelic)
        {
            if (String.IsNullOrEmpty(jRelic))
                return "遗迹不存在";
            JObject r = (JObject)JsonConvert.DeserializeObject(jRelic);
            Relic relic;
            if (!r.TryGetValue("code", out var code) || ((relic = WebAPI.GetRelic(code.ToString())) == null))
                return "遗迹不存在";

            if (r.ContainsKey("description"))
            {
                string description = r["description"].ToString();
                relic.description = String.IsNullOrEmpty(description) ? relic.description : description;
            }
            if (r.ContainsKey("name") && !String.IsNullOrEmpty(r["name"].ToString()))
            {
                relic.name = r["name"].ToString();
            }
            if (r.ContainsKey("relicTypeCode") && r["relicTypeCode"].ToString() != relic.relicTypeCode)
            {
                RelicType[] types = WebAPI.GetRelicTypes();
                string t = r["relicTypeCode"].ToString();
                foreach (RelicType type in types)
                {
                    if (type.code == t)
                    {
                        relic.relicTypeCode = type.code;
                        break;
                    }
                }
            }
            if (r.ContainsKey("location"))
            {
                JToken jLocation = r["location"];
                Location location = jLocation.ToObject<Location>();
                if (location != null)
                    relic.location = location;
            }
            WebAPI.StoreRelic(relic, out bool success, true);
            if (success)
                return null;
            else
                return "未知错误";
        }
        public string UpdateVillage(string jsonVillage)
        {
            if (String.IsNullOrEmpty(jsonVillage))
                return "文化村不存在";
            JObject r = (JObject)JsonConvert.DeserializeObject(jsonVillage);
            CultureVillage village;
            if (!r.TryGetValue("code", out var code) || ((village = WebAPI.GetVillage(code.ToString())) == null))
                return "文化村不存在";

            if (r.ContainsKey("description"))
            {
                string description = r["description"].ToString();
                village.description = String.IsNullOrEmpty(description) ? village.description : description;
            }
            if (r.ContainsKey("name") && !String.IsNullOrEmpty(r["name"].ToString()))
            {
                village.name = r["name"].ToString();
            }
            if (r.ContainsKey("location"))
            {
                JToken jLocation = r["location"];
                Location location = jLocation.ToObject<Location>();
                if (location != null)
                    village.location = location;
            }
            WebAPI.StoreVillage(village, out bool success, true);
            if (success)
                return null;
            else
                return "未知错误";
        }
        public string UpdateKnowledgeTrait(string code, string trait)
        {
            if (!WebAPI.UpdateKnowledgeTrait(code, trait))
                return "更新失败";
            return null;
        }
        public void PlayVideo(string url)
        {
            System.Diagnostics.Process.Start(url);
        }
        public void UploadRelics()
        {
            if (!CheckUploading())
            {
                return;
            }
            WebViewForm.Instance.Invoke(new Action(() =>
            {
                ScanForm.Instance.Show();
                ScanForm.Instance.SetUploadTypeToRelic();
            }));
        }
        public void UploadKnowledges()
        {
            if (!CheckUploading())
            {
                return;
            }
            WebViewForm.Instance.Invoke(new Action(() =>
            {
                ScanForm.Instance.Show();
                ScanForm.Instance.SetUploadTypeToKnowledge();
            }));
        }
        public void UploadVillages()
        {
            if (!CheckUploading())
            {
                return;
            }
            WebViewForm.Instance.Invoke(new Action(() =>
            {
                ScanForm.Instance.Show();
                ScanForm.Instance.SetUploadTypeToVillage();
            }));
        }
    }
}
