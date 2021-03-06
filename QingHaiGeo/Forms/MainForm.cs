﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QingHaiGeo
{
    public partial class MainForm : Form
    {
        //单例模式
        private static MainForm instance;
        public static MainForm Instance
        {
            get
            {
                if (MainForm.instance == null)
                    MainForm.instance = new MainForm();
                return MainForm.instance;
            }
        }
        private bool dbConnected = false;
        private bool serverConnected = false;
        private MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            lblManage.BackColor = lblUpload.BackColor = Color.FromArgb(128, 0, 0, 0);
            this.btnLogin_Click(null, null);
            if (!Config.IsLogged)
            {
                Close();
            }
        }
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (Config.IsLogged)
            {
                Config.IsLogged = false;
                return;
            }
            LoginForm.Instance.ShowDialog();
        }

     
        private void btnTestServer_Click(object sender, EventArgs e)
        {
            btnTestServer.Enabled = false;
            btnTestServer.Text = "测试中";
            if (Config.Server != "localhost" && Config.Server != "127.0.0.1" && !WebAPI.IsNetworkAvailable())
            {
                MessageBox.Show("网络连接失败。请检查网络。", "测试连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnTestServer.Text = "测试服务器";
                btnTestServer.Enabled = true;
                return;
            }
            Action endTest = delegate ()
            {
                btnTestServer.Enabled = true;
                btnTestServer.Text = "测试服务器";
            };
            Task.Run(delegate ()
            {
                if (WebAPI.TestServer())
                {
                    MessageBox.Show("连接服务器成功！", "测试连接", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.serverConnected = true;
                }
                else
                    MessageBox.Show("连接服务器失败！", "测试连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Invoke(endTest);
            });
        }
        private void btnTestDb_Click(object sender, EventArgs e)
        {
            btnTestDb.Enabled = false;
            btnTestDb.Text = "测试中";
            if (Config.DbServer != "localhost" && Config.DbServer != "127.0.0.1" && !WebAPI.IsNetworkAvailable())
            {
                MessageBox.Show("网络连接失败。请检查网络。", "测试连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnTestDb.Text = "测试数据库";
                btnTestDb.Enabled = true;
                return;
            }
            Action endTest = delegate ()
            {
                btnTestDb.Enabled = true;
                btnTestDb.Text = "测试数据库";
            };
            Task.Run(delegate ()
            {
                if (WebAPI.TestDatabase())
                {
                    MessageBox.Show("连接数据库成功！", "测试连接", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.dbConnected = true;
                }
                else
                    MessageBox.Show("连接数据库失败！", "测试连接", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Invoke(endTest);
            });
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.FromArgb(128, 0, 0, 0);
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            Label label = sender as Label;
            label.BackColor = Color.FromArgb(200, 0, 0, 0);
        }

        private void lblManage_Click(object sender, EventArgs e)
        {
            if (!Config.IsLogged)
                LoginForm.Instance.ShowDialog();
            if (!Config.IsLogged)
                return;
            this.Hide();
            WebViewForm.Instance.Show();
            WebViewForm.Instance.Focus();
        }

        private void lblUpload_Click(object sender, EventArgs e)
        {
            if (!Config.IsLogged)
            {
                MessageBox.Show("您尚未登录，请先登录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoginForm.Instance.Show();
                LoginForm.Instance.Focus();
                return;
            }
            this.Hide();
            ScanForm.Instance.Show();
            ScanForm.Instance.Focus();
        }
    }
}
