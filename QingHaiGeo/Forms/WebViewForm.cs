using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
        private ChromiumWebBrowser browser = new ChromiumWebBrowser("");
        private bool inited = false;
        private WebViewForm()
        {
            InitializeComponent();
            this.browser.RegisterJsObject("NativeObj", this, false);
            this.browser.Dock = DockStyle.Fill;
            this.browser.Parent = this;
            this.browser.Load(Config.Server + ":" + Config.Port + "/admin-resource/index.html");
            this.browser.Show();
            this.inited = true;
        }
        private void WebViewForm_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible && this.inited)
                this.browser.Load(Config.Server + ":" + Config.Port + "/admin-resource/index.html");
        }
        private void WebAPIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            MainForm.Instance.Show();
        }

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
            if (r.ContainsKey("relicTypeCode") && r["relicTypeCode"].ToString() != relic.relicTypeCode )
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
            if (r.TryGetValue("location", out JToken jLocation))
            {
                Location location = jLocation.Value<Location>();
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
    }
}
