using AutomationBase.Utility;
using System;
using System.Drawing;
//using System.Drawing.Imaging;
using System.IO;
using System.Windows;
//using TestStack.White.UIItems;

namespace AutomationBase.Utility
{
	public class Screenshot
	{
 //       public static void TakeSS_Selenium(OpenQA.Selenium.IWebDriver driver, string FileName, out string filePath)
 //       {
 //           OpenQA.Selenium.Screenshot ss = ((OpenQA.Selenium.ITakesScreenshot)driver).GetScreenshot();
 //           filePath = string.Format("{0}\\{1}\\Screenshots\\{2}.jpeg", BaseTest.TestResultPathStem, BaseTest.TestID, FileName);
 //           if (!Directory.Exists(Path.GetDirectoryName(filePath)))
 //               Directory.CreateDirectory(Path.GetDirectoryName(filePath));
 //           ss.SaveAsFile(filePath, OpenQA.Selenium.ScreenshotImageFormat.Jpeg);
 //       }

 //       public static Bitmap TakeSS_OfControl(UIItem Control)
 //       {
 //           System.Windows.Rect rect = new Rect(Control.Bounds.BottomLeft, Control.Bounds.TopRight);

 //           int TLx = Convert.ToInt16(rect.TopLeft.X);
 //           int TLy = Convert.ToInt16(rect.TopLeft.Y);

 //           Bitmap SS = new Bitmap(Convert.ToInt16(rect.Width), Convert.ToInt16(rect.Height));

 //           Graphics graphics = Graphics.FromImage(SS as System.Drawing.Image);

 //           graphics.CopyFromScreen(TLx, TLy, 0, 0, SS.Size);

 //           return SS;
 //       }
 //       public static void TakeSS_OfControl(UIItem Control, string filename)
 //       {
 //           System.Windows.Rect rect = new Rect(Control.Bounds.BottomLeft, Control.Bounds.TopRight);

 //           int TLx = Convert.ToInt16(rect.TopLeft.X);
 //           int TLy = Convert.ToInt16(rect.TopLeft.Y);

 //           Bitmap printscreen = new Bitmap(Convert.ToInt16(rect.Width), Convert.ToInt16(rect.Height));

 //           Graphics graphics = Graphics.FromImage(printscreen as System.Drawing.Image);

 //           graphics.CopyFromScreen(TLx, TLy, 0, 0, printscreen.Size);
 //           string filePath = string.Format("C:\\Automation_Data\\{0}.jpg", filename);
 //           printscreen.Save(filePath, ImageFormat.Jpeg);
 //           printscreen.Dispose();
 //       }
 //       public static Bitmap TakeSS_FullDesktop()
 //       {

 //           var Screen = System.Windows.Forms.Screen.PrimaryScreen;
 //           var size = Screen.Bounds.Size;
 //           System.Windows.Rect rect = new Rect(0, 0, size.Width, size.Height);
 //           //System.Windows.Rect rect = new Rect(Screen.Bounds.Bottom, Screen.Bounds.Right);

 //           int TLx = Convert.ToInt16(rect.TopLeft.X);
 //           int TLy = Convert.ToInt16(rect.TopLeft.Y);

 //           Bitmap SS = new Bitmap(Convert.ToInt16(rect.Width), Convert.ToInt16(rect.Height));

 //           Graphics graphics = Graphics.FromImage(SS as System.Drawing.Image);

 //           graphics.CopyFromScreen(TLx, TLy, 0, 0, SS.Size);

 //           return SS;
 //       }
 //       public static void TakeScreenShot(Bitmap SS, string FileName, out string fullPath)
 //       {
 //           fullPath = string.Format("{0}\\{1}\\Screenshots\\{2}.jpg", BaseTest.TestResultPathStem, BaseTest.TestID, FileName);
 //           Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
 //           SS.Save(fullPath, ImageFormat.Jpeg);
 //           SS.Dispose();
 //       }

	}
}
