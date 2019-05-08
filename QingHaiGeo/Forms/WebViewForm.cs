﻿using System;
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
        private ChromiumWebBrowser browser = new ChromiumWebBrowser(Application.StartupPath + "\\web\\index.html");
        private WebViewForm()
        {
            InitializeComponent();
            this.browser.RegisterJsObject("RefSharp", this, false);
            this.browser.Dock = DockStyle.Fill;
            this.browser.Parent = this;
            this.browser.Show();
        }
        private void WebAPIForm_Activated(object sender, EventArgs e)
        {
            this.browser.Load(Application.StartupPath + "\\web\\index.html");
        }
        private void WebAPIForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
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
    }

}
