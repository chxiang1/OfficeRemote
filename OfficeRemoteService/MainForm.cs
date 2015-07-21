using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OfficeRemoteService
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private static Thread thread;
        private static Worker worker;
        private static int port = 0;
        private static HttpListener listener;

        public class Worker
        {
            // This method will be called when the thread is started.
            public void DoWork(object rtb)
            {
                byte[] sendData = null;
                string dir = Environment.CurrentDirectory;
                listener = new HttpListener();

                try
                {
                    listener.Prefixes.Add(String.Format("http://+:{0}/", port));
                    listener.Start();

                    ((RichTextBox)rtb).Invoke(new Action(() =>
                    {
                        ((RichTextBox)rtb).AppendText(String.Format("{0}: \r\n{1}\r\n\r\n", DateTime.Now, "Service started at port: " + port));
                        ((RichTextBox)rtb).SelectionStart = ((RichTextBox)rtb).Text.Length;
                        ((RichTextBox)rtb).ScrollToCaret();
                    }));

                    while (!_shouldStop)
                    {
                        var context = listener.GetContext();
                        Console.WriteLine(string.Format("{0} {1}", context.Request.HttpMethod, context.Request.Url.AbsolutePath));
                        string relPath = context.Request.Url.LocalPath;
                        context.Response.ContentType = GetContentType(context.Request.Url.LocalPath);

                        if ("/Handlers/OfficeRemoteProxy.ashx" == relPath)
                        {
                            Handler.HandleRequest(context);
                                
                        }

                        if (File.Exists(dir + relPath))
                            sendData = File.ReadAllBytes(dir + relPath);
                        else
                        {
                            sendData = System.Text.Encoding.Default.GetBytes("404 File not found");
                            if (relPath == "/" || String.IsNullOrEmpty(relPath))
                            {
                                if (File.Exists(dir + "\\index.html"))
                                {
                                    sendData = File.ReadAllBytes(dir + "\\index.html");
                                    context.Response.ContentType = "text/html";
                                }
                            }
                        }

                        context.Response.Close(sendData, true);
                    }
                }
                catch (Exception ex)
                {
                    if (!(ex is ThreadAbortException))
                    {
                        ((RichTextBox)rtb).Invoke(new Action(() =>
                        {
                            ((RichTextBox)rtb).AppendText(String.Format("{0}: \r\n{1}\r\n\r\n", DateTime.Now, "Service stopped:" + ex.ToString()));
                            ((RichTextBox)rtb).SelectionStart = ((RichTextBox)rtb).Text.Length;
                            ((RichTextBox)rtb).ScrollToCaret();
                        }));
                    }
                }
                finally
                {
                    listener.Close();
                }

                Console.WriteLine("Service thread terminating gracefully.");
            }
            public void RequestStop()
            {
                _shouldStop = true;
            }

            // Volatile is used as hint to the compiler that this data
            // member will be accessed by multiple threads.
            private volatile bool _shouldStop;
        }

        static string GetContentType(string path)
        {
            string ext = "";
            int dotIndex = path.LastIndexOf('.');
            if (dotIndex >= 0)
                ext = path.Substring(dotIndex + 1);

            string contentType;
            switch (ext)
            {
                case "htm":
                    contentType = "text/html";
                    break;
                case "html":
                    contentType = "text/html";
                    break;
                case "js":
                    contentType = "text/javascript";
                    break;
                case "css":
                    contentType = "text/css";
                    break;
                case "png":
                    contentType = "image/png";
                    break;
                case "jpg":
                    contentType = "image/jpeg";
                    break;
                case "jpeg":
                    contentType = "image/jpeg";
                    break;
                default:
                    contentType = "text/plain";
                    break;
            }
            return contentType;
        }

        private string runCommand(string str)
        {
            string strOutput = "";

            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c " + str;
            p.StartInfo.Verb = "runas";
            p.StartInfo.UseShellExecute = true;

            try
            {
                p.Start();

                p.WaitForExit();
            }
            catch
            {
                MessageBox.Show("Failed to register port, probably cancelled by user.");
                strOutput = null;
            }

            return strOutput;
        }

        private void appendLog(string outStr)
        {
            rtbLog.AppendText(String.Format("{0}: \r\n{1}\r\n\r\n", DateTime.Now, outStr));
            rtbLog.SelectionStart = rtbLog.Text.Length;
            rtbLog.ScrollToCaret();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tbPort.Text))
            {
                MessageBox.Show("Please specify a port number.");
                return;
            }
            int.TryParse(tbPort.Text, out port);
            if (port == 0 || port >= 65535)
            {
                MessageBox.Show("Please specify a VALID port number.");
                return;
            }

            string strOutput = runCommand(String.Format("netsh http add urlacl url=http://+:{0}/ user=everyone listen=yes", port));
            if (strOutput == null)
                return;
            
            worker = new Worker();
            thread = new Thread(new ParameterizedThreadStart(worker.DoWork));
            thread.IsBackground = true;
            thread.Start(rtbLog);

            btnRegister.Enabled = false;
            btnStop.Enabled = true;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (worker != null)
            {
                try
                {
                    worker.RequestStop();

                    thread.Join(1000);
                    if (thread.ThreadState != System.Threading.ThreadState.Stopped)
                    {
                        listener.Close();
                        thread.Abort();
                    }
                    appendLog("Service stopped.");

                    btnRegister.Enabled = true;
                    btnStop.Enabled = false;
                }
                catch (Exception ex)
                {
                    appendLog("Failed to stop service:" + ex.ToString());
                }
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Hide();
            }
        }

        private void OfficeRemoteNotifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
