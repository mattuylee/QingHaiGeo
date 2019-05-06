using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace QingHaiGeo
{
    public partial class SettingForm : Form
    {
        //初始化标识符
        private bool inited = false;
        //单例模式实例
        private static SettingForm instance;
        public static SettingForm Instance
        {
            get
            {
                if (SettingForm.instance == null)
                    SettingForm.instance = new SettingForm();
                return SettingForm.instance;
            }
        }

        private SettingForm()
        {
            InitializeComponent();
        }

        private void Setting_Load(object sender, EventArgs e)
        {
            txtServer.Text = Config.Server;
            txtPort.Text = Config.Port.ToString();
            txtDbServer.Text = Config.DbServer;
            txtDbPort.Text = Config.DbPort.ToString();
            txtDbUser.Text = Config.DbUser;
            txtDbPassword.Text = Config.DbPassword;
            chkRememberDbPassword.Checked = Config.RememberDbPassword;
            this.inited = true;
        }

        private void chkRememberDbPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.inited && chkRememberDbPassword.Checked && !Config.ComfirmRemember())
            {
                chkRememberDbPassword.Checked = false;
                return;
            }
            Config.RememberDbPassword = chkRememberDbPassword.Checked;
            //取消记住密码
            if (!chkRememberDbPassword.Checked)
                Config.Password = Config.Password;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Config.Server = txtServer.Text;
            if (!int.TryParse(txtPort.Text, out int port) || port < 0 || port > 0xffff)
            {
                MessageBox.Show("无效的服务器端口！");
                return;
            }
            if (!int.TryParse(txtDbPort.Text, out int dbPort) || dbPort < 0 || dbPort > 0xffff)
            {
                MessageBox.Show("无效的数据库端口！");
                return;
            }
            bool needRestart = false;
            if (WebAPI.IsDatabaseConnected() &&
                    (Config.DbServer != txtDbServer.Text ||
                    Config.DbPort.ToString() != txtDbPort.Text ||
                    Config.DbUser != txtDbUser.Text ||
                    Config.DbPassword != txtDbPassword.Text))
            {
                needRestart = true;
            }
            Config.Port = (short)port;
            Config.DbPort = (short)dbPort;
            Config.DbServer = txtDbServer.Text;
            Config.DbUser = txtDbUser.Text;
            Config.DbPassword = txtDbPassword.Text;
            Config.RememberDbPassword = chkRememberDbPassword.Checked;
            this.Hide();
            if (needRestart)
                MessageBox.Show("修改成功！部分修改需要重启应用生效。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void SettingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
