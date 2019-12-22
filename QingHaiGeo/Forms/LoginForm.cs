using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace QingHaiGeo
{
    public partial class LoginForm : Form
    {
        //单例模式
        private static LoginForm instance;
        public static LoginForm Instance
        {
            get
            {
                if (LoginForm.instance == null)
                    LoginForm.instance = new LoginForm();
                return LoginForm.instance;
            }
        }
        //初始化标记
        private bool inited = false;
        private Thread loginThread;
        private LoginForm()
        {
            InitializeComponent();
        }
        private void Login_Load(object sender, EventArgs e)
        {
            this.txtUser.Text = Config.User;
            this.txtPassword.Text = Config.Password;
            this.chkRememberMe.Checked = Config.RememberMe;
            this.inited = true;
        }

        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnLogin_Click(null, null);
        }

        private void chkRememberMe_CheckedChanged(object sender, EventArgs e)
        {
            if (this.inited && chkRememberMe.Checked && !Config.ComfirmRemember())
            {
                chkRememberMe.Checked = false;
                return;
            }
            Config.RememberMe = chkRememberMe.Checked;
            //取消记住密码
            if (!chkRememberMe.Checked)
                Config.Password = Config.Password;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string requestStr = "user=" + Uri.EscapeDataString(txtUser.Text) + "&password=" +
                Uri.EscapeDataString(txtPassword.Text);
            this.loginThread = new Thread(new ParameterizedThreadStart(this.Login));
            this.loginThread.Start(requestStr);
            txtUser.Enabled = txtPassword.Enabled = chkRememberMe.Enabled = false;
            this.prgLoading.Style = ProgressBarStyle.Marquee;
            this.prgLoading.Visible = true;
        }

        //登录成功回调函数
        private void LoginSuccessCallback()
        {
            this.prgLoading.Visible = false;
            this.prgLoading.Style = ProgressBarStyle.Blocks;
            txtUser.Enabled = txtPassword.Enabled = chkRememberMe.Enabled = true;
            Config.User = txtUser.Text;
            if (Config.RememberMe)
                Config.Password = txtPassword.Text;
            Config.IsLogged = true;
            this.loginThread = null;
            this.Close();
        }
        //登录失败回调函数
        private void LoginFailedCallback(string err)
        {
            this.prgLoading.Visible = false;
            this.prgLoading.Style = ProgressBarStyle.Blocks;
            txtUser.Enabled = txtPassword.Enabled = chkRememberMe.Enabled = true;
            this.loginThread = null;
            MessageBox.Show(err, "登录失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //发起登录，异步线程操作
        private void Login(object requestBodyString)
        {
            string[] callbackParam = new string[1];
            if (Config.Server != "localhost" && Config.Server != "127.0.0.1" && !WebAPI.IsNetworkAvailable())
            {
                callbackParam[0] = "网络连接失败。请检查网络。";
                this.Invoke(new Action<string>(this.LoginFailedCallback), callbackParam);
                return;
            }
            HttpWebRequest request =
                (HttpWebRequest)WebRequest.Create(Config.Server + ":" + Config.Port + Config.LoginPath);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            try
            {
                Stream requstStream = request.GetRequestStream();
                byte[] buffer = Encoding.UTF8.GetBytes(requestBodyString as string);
                requstStream.Write(buffer, 0, buffer.Length);
            }
            catch (WebException e)
            {
                callbackParam[0] = e.Message + "!";
                this.Invoke(new Action<string>(this.LoginFailedCallback), callbackParam);
                return;
            }
            JObject jResult = null;
            try
            {
                Stream responseStream = request.GetResponse().GetResponseStream();
                string responseText = new StreamReader(responseStream).ReadToEnd();
                jResult = (JObject)JsonConvert.DeserializeObject(responseText);
            }
            catch (Exception e)
            {
                callbackParam[0] = "登录出错：\n" + e.Message;
                this.Invoke(new Action<string>(this.LoginFailedCallback), callbackParam);
                return;
            }

            bool success = jResult.TryGetValue("error", out JToken error);
            if (!success || error.ToString() != string.Empty)
            {
                if (success)
                    callbackParam[0] = error.ToString();
                else
                    callbackParam[0] = "登录服务器失败，请检查服务器地址和端口配置是否正确！";
                this.Invoke(new Action<string>(this.LoginFailedCallback), callbackParam);
                return;
            }
            if (!jResult.TryGetValue("user", out JToken jUser) || jUser.Type == JTokenType.Null)
            {
                callbackParam[0] = "服务器错误！获取用户信息失败！请检查服务器是否正常运行！";
                this.Invoke(new Action<string>(this.LoginFailedCallback), callbackParam);
                return;
            }
            Config.CurrentUser.id = jUser.SelectToken("id")?.ToString();
            Config.CurrentUser.userName = jUser.SelectToken("userName")?.ToString();
            Config.CurrentUser.nickName = jUser.SelectToken("nickName")?.ToString();
            Config.CurrentUser.realName = jUser.SelectToken("realName")?.ToString();
            Config.CurrentUser.avatar = jUser.SelectToken("avatar")?.ToString();
            Config.CurrentUser.isSuperAdmin = jUser.SelectToken("isSuperAdmin")?.ToString().ToLower() == "true";
            JToken jIsAdmin = jUser.SelectToken("isAdmin");
            if (jIsAdmin != null)
                Config.CurrentUser.isAdmin = jIsAdmin.Value<bool>();

            if (Config.CurrentUser.id == null)
            {
                callbackParam[0] = "数据异常！未获取到管理员凭证！请检查服务器是否正常运行！";
                this.Invoke(new Action<string>(this.LoginFailedCallback), callbackParam);
                return;
            }
            if (!Config.CurrentUser.isAdmin && !Config.CurrentUser.isSuperAdmin)
            {
                callbackParam[0] = "仅允许管理员账户登录！";
                this.Invoke(new Action<string>(this.LoginFailedCallback), callbackParam);
                return;
            }
            if (!WebAPI.TestDatabase())
            {
                callbackParam[0] = "连接数据库失败！请检查数据库配置！";
                this.Invoke(new Action<string>(this.LoginFailedCallback), callbackParam);
                return;
            }
            this.Invoke(new Action(this.LoginSuccessCallback));
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.loginThread != null)
            {
                this.loginThread.Abort();
            }
            if (!Config.IsLogged)
            {
                Application.Exit();
            }
        }

        private void lblConfig_Click(object sender, EventArgs e)
        {
            SettingForm.Instance.Show();
            SettingForm.Instance.Focus();
        }

        private void lklWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(lklWebsite.Text);
        }
    }
}
