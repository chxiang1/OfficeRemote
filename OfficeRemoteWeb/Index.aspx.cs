using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using PPt = Microsoft.Office.Interop.PowerPoint;
using System.Runtime.InteropServices;

namespace OfficeRemoteWeb
{
    public partial class Index : System.Web.UI.Page
    {
        PPt.Application pptApplication;
        PPt.Presentation presentation;
        PPt.Slides slides;
        PPt.Slide slide;
        int slidescount;
        int slideIndex;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                // Get Running PowerPoint Application object 
                pptApplication = System.Runtime.InteropServices.Marshal.GetActiveObject("PowerPoint.Application") as PPt.Application;
            }
            catch
            {
                
            }
            if (pptApplication != null)
            {
                // Get Presentation Object 
                presentation = pptApplication.ActivePresentation;
                // Get Slide collection object 
                slides = presentation.Slides;
                // Get Slide count 
                slidescount = slides.Count;
                // Get current selected slide  
                try
                {
                    // Get selected slide object in normal view 
                    slide = slides[pptApplication.ActiveWindow.Selection.SlideRange.SlideNumber];
                }
                catch
                {
                    // Get selected slide object in reading view 
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            }
        }

        protected void lbNext_Click(object sender, EventArgs e)
        {
            slideIndex = slide.SlideIndex + 1;
            if (slideIndex > slidescount)
            {
                
            }
            else
            {
                try
                {
                    slide = slides[slideIndex];
                    slides[slideIndex].Select();
                }
                catch
                {
                    pptApplication.SlideShowWindows[1].View.Next();
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            } 
        }

        protected void lbPrev_Click(object sender, EventArgs e)
        {
            slideIndex = slide.SlideIndex - 1;
            if (slideIndex >= 1)
            {
                try
                {
                    slide = slides[slideIndex];
                    slides[slideIndex].Select();
                }
                catch
                {
                    pptApplication.SlideShowWindows[1].View.Previous();
                    slide = pptApplication.SlideShowWindows[1].View.Slide;
                }
            }
        }

        protected void lbFirst_Click(object sender, EventArgs e)
        {
            try
            {
                // Call Select method to select first slide in normal view 
                slides[1].Select();
                slide = slides[1];
            }
            catch
            {
                // Transform to first page in reading view 
                pptApplication.SlideShowWindows[1].View.First();
                slide = pptApplication.SlideShowWindows[1].View.Slide;
            } 
        }

        protected void lbLast_Click(object sender, EventArgs e)
        {
            try
            {
                slides[slidescount].Select();
                slide = slides[slidescount];
            }
            catch
            {
                pptApplication.SlideShowWindows[1].View.Last();
                slide = pptApplication.SlideShowWindows[1].View.Slide;
            } 
        }
    }
}