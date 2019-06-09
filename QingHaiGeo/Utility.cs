using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MediaInfoDotNet;

namespace QingHaiGeo
{
    public static class Utility
    {
        /// <summary>
        /// 判断是否UTF-8编码
        /// </summary>
        /// <param name="buffer">文本字节数组</param>
        /// <returns></returns>
        public static bool IsUtf8(byte[] buffer)
        {

            if (buffer == null)
                return false;
            for (int i = 0; i < buffer.Length; ++i)
            {
                short c = buffer[i];
                if (c < 0)
                    c += 256;
                if (c <= 0x7f)
                    continue;
                else if ((c & 0xe0) == 0xc0)
                {
                    if (i >= buffer.Length - 1 || (buffer[i + 1] & 0xc0) != 0x80)
                        return false;
                    else
                        ++i;
                }
                else if ((c & 0xf0) == 0xe0)
                {
                    if (i >= buffer.Length - 2 || (buffer[i + 1] & 0xc0) != 0x80
                            || (buffer[i + 2] & 0xc0) != 0x80)
                        return false;
                    else
                        i += 2;
                }
                else
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 图像缩略图处理
        /// </summary>
        /// <param name="sourceFile">图像源数据</param>
        /// <param name="compression">压缩质量 1-100</param>
        /// <param name="maxHeight">缩略图的高</param>
        /// <param name="maxWidth">缩略图的宽</param>
        /// <returns></returns>
        public static Stream EncodePicture(string sourceFile, int compression, int maxHeight = 0, int maxWidth = 0)
        {
            try
            {
                Bitmap srcimg = new Bitmap(sourceFile);
                if (maxWidth == 0 && maxHeight == 0)
                {
                    maxWidth = srcimg.Width;
                    maxHeight = srcimg.Height;
                }
                //if ((maxWidth > 0 && maxWidth < srcimg.Width) &&
                //    ((float)srcimg.Height / srcimg.Width * maxWidth < srcimg.Height))
                //{
                //    maxHeight = (int)((float)srcimg.Height / srcimg.Width * maxWidth);
                //}
                //else
                //    maxHeight = srcimg.Height;
                if ((maxHeight > 0 && maxHeight < srcimg.Height) &&
                    ((float)srcimg.Width / srcimg.Height * maxHeight < srcimg.Width))
                {
                    maxWidth = (int)((float)srcimg.Width / srcimg.Height * maxHeight);
                }
                else
                {
                    maxWidth = srcimg.Width;
                    maxHeight = srcimg.Height;
                }

                Bitmap dstimg = new Bitmap(maxWidth, maxHeight);//图片压缩质量
                                                                //从Bitmap创建一个System.Drawing.Graphics对象，用来绘制高质量的缩小图。
                using (Graphics gr = Graphics.FromImage(dstimg))
                {
                    //把原始图像绘制成上面所设置宽高的缩小图
                    Rectangle rectDestination = new Rectangle(0, 0, maxWidth, maxHeight);
                    gr.Clear(Color.WhiteSmoke);
                    gr.CompositingQuality = CompositingQuality.HighQuality;
                    gr.SmoothingMode = SmoothingMode.HighQuality;
                    gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    gr.DrawImage(srcimg, rectDestination, 0, 0, srcimg.Width, srcimg.Height, GraphicsUnit.Pixel);
                    EncoderParameters ep = new EncoderParameters(1);
                    ep.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, compression);//设置压缩的比例1-100
                    ImageCodecInfo[] arrayICI = ImageCodecInfo.GetImageEncoders();
                    ImageCodecInfo jpegICIinfo = arrayICI.FirstOrDefault(t => t.FormatID == System.Drawing.Imaging.ImageFormat.Png.Guid);
                    MemoryStream dstms = new MemoryStream();
                    if (jpegICIinfo != null)
                    {
                        dstimg.Save(dstms, jpegICIinfo, ep);
                    }
                    else
                    {
                        dstimg.Save(dstms, ImageFormat.Png); //保存到内存里
                    }
                    srcimg.Dispose();
                    dstimg.Dispose();
                    dstms.Position = 0;
                    return dstms;
                }
            }
            catch { return null; }
        }
        // 获取图片编码信息
        private static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        /// <summary>
        /// 视频转码。注意，转码失败时会删除文件<paramref name="dest"/>
        /// </summary>
        /// <param name="src">源文件</param>
        /// <param name="dest">目标文件，将被覆写</param>
        /// <param name="mode">清晰度级别</param>
        /// <returns>
        /// 是否成功。注意，如果<paramref name="mode"/>不是最低级别，且视频质量较低时可能直接返
        /// 回true，但并没有向<paramref name="dest"/>文件写入数据。
        /// </returns>
        public static bool EncodeVideo(string src, string dest, SharpnessMode mode)
        {
            int maxWidth;
            if (mode == SharpnessMode.low)
                maxWidth = 720;
            else if (mode == SharpnessMode.mid)
                maxWidth = 1280;
            else
                maxWidth = 1920;
            try
            {
                MediaFile mediaFile = new MediaFile(src);
                int width = mediaFile.Video[0].Width;
                int height = mediaFile.Video[0].Height;
                // 视频转码时按清晰度从低到高的顺序来的，如果视频本身质量很低，采用上一次转码的结果即可，
                // 无需多次转码浪费时间（因为转出来的质量都是一样的低清啊）
                if (mode == SharpnessMode.mid && width <= 720)
                    return true;
                else if (mode == SharpnessMode.high && width <= 1280)
                    return true;
                // 按比例调整尺寸
                if (width > maxWidth)
                {
                    height = (int)(maxWidth / (float)width * height);
                    height = height % 2 == 0 ? height : height + 1;
                    width = maxWidth;
                }
                string ffmpegPath;
                if (File.Exists(Application.StartupPath + @"\ffmpeg\bin\ffmpeg.exe"))
                    ffmpegPath = "\"" + Directory.GetParent(Application.ExecutablePath) + "\\ffmpeg\\bin\\ffmpeg.exe\"";
                else
                    ffmpegPath = "ffmpeg";
                //调用ffmpeg转码
                string cmd = ffmpegPath + " -y -stats -i \"" + Path.GetFullPath(src) + "\" -vcodec h264 -acodec aac -s "
                    + width + "*" + height + " \"" + Path.GetFullPath(dest) + "\"";
                Process p = new Process();
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/c " + cmd;
                p.StartInfo.UseShellExecute = true;
                p.StartInfo.RedirectStandardInput = false;
                p.StartInfo.RedirectStandardOutput = false;
                p.StartInfo.CreateNoWindow = false;
                p.Start();
                //等待程序执行完退出进程
                p.WaitForExit();
                bool normal = p.ExitCode == 0;
                p.Close();
                if (!normal)
                    File.Delete(dest);
                return normal;
            }
            catch
            {
                try { File.Delete(dest); } catch { }
                return false;
            }
        }
        /// <summary>
        /// 测试ffmpeg是否可用
        /// </summary>
        /// <returns></returns>
        public static bool TestFFmpeg()
        {
            if (File.Exists(Application.StartupPath + @"\ffmpeg\bin\ffmpeg.exe"))
                return true;
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            //启动程序
            p.Start();
            p.StandardInput.AutoFlush = true;
            p.StandardInput.WriteLine("ffmpeg -version");
            p.StandardInput.WriteLine("exit");
            string strOuput = p.StandardOutput.ReadToEnd();
            return strOuput.Contains("ffmpeg version");
        }
    }
}