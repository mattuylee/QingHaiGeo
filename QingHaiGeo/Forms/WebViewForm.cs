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
                    WebViewForm.instance = new WebViewForm();
                return WebViewForm.instance;
            }
        }
        private ChromiumWebBrowser browser;
        
        private WebViewForm()
        {
            InitializeComponent();
            LoginForm.Instance.ShowDialog();
            if (!Config.IsLogged)
            {
                Application.Exit();
            }
            HttpListener listerner = new HttpListener();
            listerner.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            Config.localPort = (ushort)GetRandomPort();
            string prefix = "http://localhost:" + Config.localPort+ "/";
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
                    catch (Exception e) { }
                }
            }));
            this.httpThread.IsBackground = true;
            this.httpThread.Start();
            this.browser = new ChromiumWebBrowser(prefix + "index.html");
        }
        private void WebViewForm_Load(object sender, EventArgs e)
        {
            this.browser.JavascriptObjectRepository.Register("NativeObj", new DataBinding(), false);
            this.browser.Dock = DockStyle.Fill;
            this.browser.Parent = this;
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
    }

    class DataBinding
    {
        public string Login()
        {
            if (Config.IsLogged)
                return Config.CurrentUser.id;
            LoginForm.Instance.ShowDialog();
            if (Config.IsLogged)
                return Config.CurrentUser.id;
            else
                return null;
        }
        public void AddRelicVideo(string code)
        {
            if (Config.IsLogged)
            {
                new VideoForm(code, TargetType.relic).ShowDialog();
                return;
            }
            var res =
                MessageBox.Show("请先登录！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res != DialogResult.OK)
                return;
            LoginForm.Instance.ShowDialog();
            if (Config.IsLogged)
                new VideoForm(code, TargetType.relic).ShowDialog();
        }
        public void AddKnowledgeVideo(string code)
        {
            if (Config.IsLogged)
            {
                new VideoForm(code, TargetType.knowledge).ShowDialog();
                return;
            }
            var res =
                MessageBox.Show("请先登录！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (res != DialogResult.OK)
                return;
            LoginForm.Instance.ShowDialog();
            if (Config.IsLogged)
                new VideoForm(code, TargetType.knowledge).ShowDialog();
        }
        public int GetRelicCount()
        {
            return WebAPI.GetRelicCount();
        }
        public int GetKnowledgeCount()
        {
            return WebAPI.GetKnowledgeCount();
        }
        public int GetUserCount()
        {
            return WebAPI.GetUserCount();
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
            if (ScanForm.Instance.Visible)
            {
                MessageBox.Show("当前正在上传数据，请终止后继续。", "批量上传", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ScanForm.Instance.SetUploadTypeToRelic();
            ScanForm.Instance.Show();
        }
        public void UploadKnowledges()
        {
            if (ScanForm.Instance.Visible)
            {
                MessageBox.Show("当前正在上传数据，请终止后继续。", "批量上传", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ScanForm.Instance.SetUploadTypeToKnowledge();
            ScanForm.Instance.Show();
        }
    }
}
