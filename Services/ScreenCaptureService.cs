using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ConsoleApp1.Services
{
    public class ScreenCaptureService
    {
        public string CaptureScreen()
        {
            try
            {
                string screenshotDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ScreenCaptures");

                if (!Directory.Exists(screenshotDir))
                    Directory.CreateDirectory(screenshotDir);

                string fileName = $"Screenshot_{DateTime.Now:yyyyMMdd_HHmmss}.png";
                string filePath = Path.Combine(screenshotDir, fileName);

                // Get the primary monitor's bounds
                Rectangle bounds = Screen.PrimaryScreen.Bounds;

                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        // Copy the screen to the bitmap
                        g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                    }

                    // Save as PNG
                    bitmap.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                }

                Console.WriteLine($"✓ Screenshot saved: {filePath}");

                // Open in default image viewer
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(filePath) { UseShellExecute = true });

                return filePath;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Screenshot error: {ex.Message}");
                return string.Empty;
            }
        }

        public string CaptureScreenToClipboard()
        {
            try
            {
                Rectangle bounds = Screen.PrimaryScreen.Bounds;

                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(bounds.Location, Point.Empty, bounds.Size);
                    }

                    Clipboard.SetImage(bitmap);
                }

                Console.WriteLine("✓ Screenshot copied to clipboard");
                return "clipboard";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Clipboard error: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
