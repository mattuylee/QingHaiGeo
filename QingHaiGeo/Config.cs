using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.IO;
using System.Windows.Forms;

namespace QingHaiGeo
{
    public static class Config
    {
        //标识一个值未定义
        private const int UNDEFINED_VALUE = -1;
        //配置文件名
        private readonly static string FILE_NAME = Application.StartupPath + "\\config.ini";

        //是否已登录
        public static bool IsLogged
        {
            get
            {
                return isLogged;
            }
            set
            {
                isLogged = value;
                if (!isLogged)
                    currentUser = null;
            }
        }

        //当前用户
        private static User currentUser;
        public static User CurrentUser
        {
            get
            {
                if (currentUser == null) return currentUser = new User();
                else return currentUser;
            }
        }

        //管理员用户名
        public static string User
        {
            get
            {
                if (user == null)
                    user = ReadIniItem(FILE_NAME, "USER", "user");
                return user;
            }
            set
            {
                if (user == value) return;
                user = value;
                WriteIniItem(FILE_NAME, "USER", "user", user);
            }
        }
        //用户密码
        public static string Password
        {
            get
            {
                if (password == null)
                    password = ReadIniItem(FILE_NAME, "USER", "password");
                return password;
            }
            set
            {
                if (password == value) return;
                password = value;
                if(RememberMe)
                    WriteIniItem(FILE_NAME, "USER", "password", password);
            }
        }
        //记住密码
        public static bool RememberMe
        {
            get
            {
                if (rememberMe == UNDEFINED_VALUE)
                {
                    Boolean.TryParse(ReadIniItem(FILE_NAME, "USER", "rememberMe"), out bool temp);
                    rememberMe = temp ? 1 : 0;
                }
                return rememberMe == 1;
            }
            set
            {
                if ((rememberMe == 1) == value) return;
                rememberMe = value ? 1 : 0;
                WriteIniItem(FILE_NAME, "USER", "rememberMe", (rememberMe==1).ToString());
                //清除保存的密码
                if (rememberMe == 0)
                {
                    string pwd = password;
                    rememberMe = 1;
                    Password = "";
                    rememberMe = 0;
                    Password = pwd;
                }

            }
        }

        //服务器地址
        public static string Server
        {
            get
            {
                if (server == null)
                {
                    server = ReadIniItem(FILE_NAME, "SERVER", "server");
                    if (server == string.Empty)
                        server = "http://127.0.0.1";
                }
                return server;
            }
            set
            {
                if (server == value) return;
                server = value;
                WriteIniItem(FILE_NAME, "SERVER", "server", server);
            }
        }
        //登录路径
        public readonly static string LoginPath = "/user/login";
        //服务器端口
        public static ushort Port
        {
            get
            {
                if (port == 0)
                {
                    if(!UInt16.TryParse(ReadIniItem(FILE_NAME, "SERVER", "port"), out port))
                        port = 80;
                }
                return port;
            }
            set
            {
                if (port == value) return;
                port = value;
                WriteIniItem(FILE_NAME, "SERVER", "port", port.ToString());
            }
        }

        //数据库服务器地址
        public static string DbServer
        {
            get
            {
                if (dbServer == null)
                {
                    dbServer = ReadIniItem(FILE_NAME, "DATABASE", "server");
                    if (dbServer == string.Empty)
                        dbServer = "127.0.0.1";
                }
                return dbServer;
            }
            set
            {
                if (dbServer == value) return;
                dbServer = value;
                WriteIniItem(FILE_NAME, "DATABASE", "server", dbServer);
            }
        }
        //数据库端口
        public static ushort DbPort
        {
            get
            {
                if (dbPort == 0)
                {
                    if (!UInt16.TryParse(ReadIniItem(FILE_NAME, "DATABASE", "port"), out dbPort))
                        dbPort = 27017;
                }
                return dbPort;
            }
            set
            {
                if (dbPort == value) return;
                dbPort = value;
                WriteIniItem(FILE_NAME, "DATABASE", "port", dbPort.ToString());
            }
        }
        //数据库用户名
        public static string DbUser
        {
            get
            {
                if (dbUser == null)
                    dbUser = ReadIniItem(FILE_NAME, "DATABASE", "user");
                return dbUser;
            }
            set
            {
                if (dbUser == value) return;
                dbUser = value;
                WriteIniItem(FILE_NAME, "DATABASE", "user", dbUser);
            }
        }
        //数据库密码
        public static string DbPassword
        {
            get
            {
                if (dbPassword == null)
                    dbPassword = ReadIniItem(FILE_NAME, "DATABASE", "password");
                return dbPassword;
            }
            set
            {
                if (dbPassword == value) return;
                dbPassword = value;
                if (RememberDbPassword)
                    WriteIniItem(FILE_NAME, "DATABASE", "password", dbPassword);
            }
        }
        //记住数据库密码
        public static bool RememberDbPassword
        {
            get
            {
                if (rememberDbPassword == UNDEFINED_VALUE)
                {
                    if (!Boolean.TryParse(ReadIniItem(FILE_NAME, "DATABASE", "rememberDbPassword"), out bool temp))
                        rememberDbPassword = 1;
                    else
                        rememberDbPassword = temp ? 1 : 0;
                }
                return rememberDbPassword == 1;
            }
            set
            {
                if ((rememberDbPassword == 1) == value) return;
                rememberDbPassword = value ? 1 : 0;
                if (rememberDbPassword == 0)
                {
                    string pwd = password;
                    rememberDbPassword = 1;
                    Password = "";
                    rememberDbPassword = 0;
                    Password = pwd;
                }
                WriteIniItem(FILE_NAME, "DATABASE", "rememberDbPassword", (rememberDbPassword == 1).ToString());
            }
        }
        //本地HTTP服务端口，用于加载管理页面
        public static ushort localPort = 0;

        private static bool isLogged;
        private static string user = null;
        private static string password = null;
        private static int rememberMe = UNDEFINED_VALUE;
        private static string server = null;
        private static ushort port = 0;
        private static string dbUser = null;
        private static string dbPassword = null;
        private static int rememberDbPassword = UNDEFINED_VALUE;
        private static string dbServer = null;
        private static ushort dbPort = 0;

        #region
        [DllImport("kernel32")]//返回0表示失败，非0为成功
        private static extern long WritePrivateProfileString(string section, string key,
            string val, string filePath);
        [DllImport("kernel32")]//返回取得字符串缓冲区的长度
        private static extern int GetPrivateProfileString(string section, string key,
            string def, StringBuilder retVal, int size, string filePath);
        #endregion

        /// <summary>
        /// 读取配置项
        /// </summary>
        /// <param name="filename">配置文件名</param>
        /// <param name="section">节名称</param>
        /// <param name="key">配置项名称</param>
        public static string ReadIniItem(string filename, string section, string key)
        {
            if (!File.Exists(filename))
                return string.Empty;
            StringBuilder sb = new StringBuilder(256);
            if (GetPrivateProfileString(section, key, null, sb, 256, filename) == 0)
            {
                StreamReader reader = new FileInfo(filename).OpenText();
                string text = reader.ReadToEnd();
                reader.Close();
                int pos = text.IndexOf("[" + section + "]");
                if (pos == -1) return string.Empty;
                pos = text.IndexOf(key + "=", pos);
                if (pos == -1) return string.Empty;
                pos += key.Length + 1;
                int endPos = text.IndexOfAny(new char[] { '\n','\r'}, pos);
                if (endPos == -1) endPos = text.Length;
                return text.Substring(pos, endPos - pos).TrimEnd(new char[] { '\n', '\r', '\t', '\v'});
            }
            return sb.ToString();
        }
        /// <summary>
        /// 写配置项
        /// </summary>
        /// <param name="filename">配置文件名</param>
        /// <param name="section">节名称</param>
        /// <param name="key">配置项名称</param>
        /// <param name="value">配置项值</param>
        /// <returns></returns>
        public static bool WriteIniItem(string filename, string section, string key, string value)
        {
            return WritePrivateProfileString(section, key, value, filename) == 0;
        }

        /// <summary>
        /// 弹窗确认是否记住密码
        /// </summary>
        public static bool ComfirmRemember()
        {
            DialogResult result =
                MessageBox.Show("记住密码可能造成安全隐患，您确定要记住密码？", "记住密码", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            return result == DialogResult.OK;
        }
    }
}
