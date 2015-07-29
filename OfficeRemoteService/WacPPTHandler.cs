using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Forms;
using SHDocVw;

namespace OfficeRemoteService
{
    public class WacPPTHandler
    {
        [DllImport("user32.dll", SetLastError = true)]
        public static extern void SwitchToThisWindow(IntPtr hWnd, bool fAltTab);

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, IntPtr dwExtraInfo);

        private const UInt32 MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const UInt32 MOUSEEVENTF_LEFTUP = 0x0004;

        [DllImport("user32.dll")]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 dwData, IntPtr dwExtraInfo);

        // This is the COM IServiceProvider interface, not System.IServiceProvider .Net interface!
        [ComImport(), ComVisible(true), Guid("6D5140C1-7436-11CE-8034-00AA006009FA"),
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        public interface IServiceProvider
        {
            [return: MarshalAs(UnmanagedType.I4)]
            [PreserveSig]
            int QueryService(ref Guid guidService, ref Guid riid, [MarshalAs(UnmanagedType.Interface)] out object ppvObject);
        }

        const int E_ACCESSDENIED = unchecked((int)0x80070005L);
        static Guid IID_IWebBrowserApp = new Guid("0002DF05-0000-0000-C000-000000000046");
        static Guid IID_IWebBrowser2 = new Guid("D30C1661-CDAF-11D0-8A3E-00C04FC9E26E");

        const string WacPPTURLPattern = "PowerPointView=ReadingView";
        const string NextPageButtonId = "ButtonFastFwd-Small14";
        const string PrePageButtonId = "ButtonFastBack-Small14";

        public static void HandleRequest(HttpListenerContext context)
        {
            string gesType = context.Request.QueryString["type"];
            string gesDirection = context.Request.QueryString["direction"] ?? "";
            int touches = 0;
            if (null != context.Request.QueryString["touches"])
                Int32.TryParse(context.Request.QueryString["touches"], out touches);
            double offsetX = 0;
            if (null != context.Request.QueryString["offset_x"])
                Double.TryParse(context.Request.QueryString["offset_x"], out offsetX);
            double offsetY = 0;
            if (null != context.Request.QueryString["offset_y"])
                Double.TryParse(context.Request.QueryString["offset_y"], out offsetY);

            double frameLeft = 0, frameTop = 0;
            mshtml.IHTMLDocument2 frameDoc = FindFirstPPTFrame(out frameLeft, out frameTop);
            if (frameDoc == null)
            {
                return;
            }

            if (gesType == "swipe")
            {
                int buttonLeftToFrame = 0,
                    buttonTopToFrame = 0;

                if (gesDirection == "left")
                {
                    var preButton = (mshtml.IHTMLElement)frameDoc.all.item(PrePageButtonId);
                    if (preButton == null)
                    {
                        return;
                    }

                    GetElementLocToFrame(preButton, out buttonLeftToFrame, out buttonTopToFrame);
                    Click(
                        (int)(buttonLeftToFrame + frameLeft + preButton.offsetWidth / 2),
                        (int)(buttonTopToFrame + frameTop + preButton.offsetHeight / 2));
                }
                else if (gesDirection == "right")
                {
                    var nextButton = (mshtml.IHTMLElement)frameDoc.all.item(NextPageButtonId);
                    if (nextButton == null)
                    {
                        return;
                    }

                    GetElementLocToFrame(nextButton, out buttonLeftToFrame, out buttonTopToFrame);
                    Click(
                        (int)(buttonLeftToFrame + frameLeft + nextButton.offsetWidth / 2),
                        (int)(buttonTopToFrame + frameTop + nextButton.offsetHeight / 2));
                }
            }
        }

        private static mshtml.IHTMLDocument2 FindFirstPPTFrame(
            out double frameLeft,
            out double frameTop)
        {
            ShellWindows windows = new ShellWindows();
            foreach (InternetExplorer explorer in windows)
            {
                IntPtr handle = (IntPtr)explorer.HWND;
                mshtml.IHTMLDocument2 doc = explorer.Document;

                if (doc.frames != null)
                {
                    for (int i = 0; i < doc.frames.length; i++)
                    {
                        mshtml.IHTMLWindow2 frameWindow = doc.frames.item(i);

                        mshtml.IHTMLDocument2 frameDoc = null;
                        try
                        {
                            frameDoc = frameWindow.document;
                        }
                        catch(System.UnauthorizedAccessException)
                        { }

                        // convert IHTMLWindow2 to IWebBrowser2 using IServiceProvider.
                        IServiceProvider sp = (IServiceProvider)frameWindow;

                        // Use IServiceProvider.QueryService to get IWebBrowser2 object
                        object brws = null;
                        sp.QueryService(ref IID_IWebBrowserApp, ref IID_IWebBrowser2, out brws);

                        // Get the document from IWebBrowser2
                        //SHDocVw.IWebBrowser2 browser = (SHDocVw.IWebBrowser2)brws;
                        InternetExplorer browser = (InternetExplorer)brws;
                        frameDoc = (mshtml.IHTMLDocument2)browser.Document;

                        if (frameDoc != null && frameDoc.url.Contains(WacPPTURLPattern))
                        {
                            // get the screen position of frame
                            AutomationElement autoEle = AutomationElement.FromHandle(handle);
                            var ieServerEle = autoEle.FindFirst(
                                TreeScope.Element | TreeScope.Descendants,
                                new PropertyCondition(
                                    AutomationElement.ClassNameProperty,
                                    "Internet Explorer_Server"));

                            if (ieServerEle != null)
                            {
                                SwitchToThisWindow(handle, true);
                                frameLeft = ieServerEle.Current.BoundingRectangle.Left;
                                frameTop = ieServerEle.Current.BoundingRectangle.Top;
                                return frameDoc;
                            }
                        }
                    }
                }
            }

            frameLeft = 0;
            frameTop = 0;
            return null;
        }

        private static void GetElementLocToFrame(
            mshtml.IHTMLElement ele,
            out int left,
            out int top)
        {
            left = ele.offsetLeft;
            top = ele.offsetTop;
            var pp = ele.offsetParent;
            while (pp != null)
            {
                left += pp.offsetLeft;
                top += pp.offsetTop;
                pp = pp.offsetParent;
            }
        }

        private static void Click(int x, int y)
        {
            Cursor.Position = new Point(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, new IntPtr());
            mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, new IntPtr());
        }
    }
}
