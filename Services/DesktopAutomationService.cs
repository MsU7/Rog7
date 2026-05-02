using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConsoleApp1.Services
{
    public class DesktopAutomationService
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out POINT lpPoint);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;
        }

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_MOVE = 0x0001;
        private const uint MOUSEEVENTF_WHEEL = 0x0800;
        private const byte VK_LCONTROL = 0x11;
        private const byte VK_LSHIFT = 0x10;
        private const byte VK_LALT = 0x12;
        private const uint KEYEVENTF_KEYUP = 0x0002;

        public async Task<bool> ExecuteCommandAsync(CommandParsed command)
        {
            try
            {
                return command.Action.ToLower() switch
                {
                    "open_app" => await OpenApplicationAsync(command.Target),
                    "close_app" => CloseApplication(command.Target),
                    "click" => ClickMouse(command.Target),
                    "type" => TypeText(command.Target),
                    "screenshot" => true, // Handled separately
                    "move_mouse" => MoveMouse(command.Target),
                    "scroll" => ScrollMouse(command.Target),
                    "search_web" => await SearchWebAsync(command.Target),
                    "open_file" => await OpenFileAsync(command.Target),
                    "run_command" => await RunCommandAsync(command.Target),
                    "email" => await SendEmailAsync(command.Target),
                    "note" => CreateNote(command.Target),
                    _ => false
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Execution error: {ex.Message}");
                return false;
            }
        }

        private async Task<bool> OpenApplicationAsync(string appName)
        {
            try
            {
                string processName = appName.ToLower();

                // Map common app names to executables
                var appMap = new Dictionary<string, string>
                {
                    { "notepad", "notepad.exe" },
                    { "calculator", "calc.exe" },
                    { "calc", "calc.exe" },
                    { "word", "winword.exe" },
                    { "excel", "excel.exe" },
                    { "powerpoint", "powerpnt.exe" },
                    { "chrome", "chrome.exe" },
                    { "firefox", "firefox.exe" },
                    { "edge", "msedge.exe" },
                    { "explorer", "explorer.exe" },
                    { "file explorer", "explorer.exe" },
                    { "vs code", "code.exe" },
                    { "visual studio", "devenv.exe" },
                    { "teams", "Teams.exe" },
                    { "discord", "Discord.exe" },
                    { "telegram", "Telegram.exe" },
                    { "spotify", "Spotify.exe" }
                };

                string executable = appMap.ContainsKey(processName) ? appMap[processName] : processName + ".exe";

                Process.Start(executable);
                Console.WriteLine($"Opening {appName}...");
                await Task.Delay(2000);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open {appName}: {ex.Message}");
                return false;
            }
        }

        private bool CloseApplication(string appName)
        {
            try
            {
                var processName = appName.Split(' ')[0].ToLower();
                var processes = Process.GetProcessesByName(processName);

                foreach (var process in processes)
                {
                    process.Kill();
                }

                if (processes.Length > 0)
                {
                    Console.WriteLine($"Closed {appName}");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to close {appName}: {ex.Message}");
                return false;
            }
        }

        private bool ClickMouse(string target)
        {
            try
            {
                // Get current mouse position or parse coordinates from target
                if (int.TryParse(target.Split(',')[0], out int x) && int.TryParse(target.Split(',')[1], out int y))
                {
                    SetCursorPos(x, y);
                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                    return true;
                }

                // Click at current position
                mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool TypeText(string text)
        {
            try
            {
                SendKeys.SendWait(text);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private bool MoveMouse(string target)
        {
            try
            {
                var parts = target.Split(',');
                if (parts.Length >= 2 && int.TryParse(parts[0], out int x) && int.TryParse(parts[1], out int y))
                {
                    SetCursorPos(x, y);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool ScrollMouse(string direction)
        {
            try
            {
                int scrollAmount = direction.ToLower().Contains("down") ? -120 : 120;
                GetCursorPos(out POINT pt);
                mouse_event(MOUSEEVENTF_WHEEL, (uint)pt.X, (uint)pt.Y, (uint)scrollAmount, 0);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> SearchWebAsync(string query)
        {
            try
            {
                string url = $"https://www.google.com/search?q={Uri.EscapeDataString(query)}";
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
                await Task.Delay(1000);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> OpenFileAsync(string filePath)
        {
            try
            {
                Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                await Task.Delay(1000);
                return true;
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> RunCommandAsync(string command)
        {
            try
            {
                var processInfo = new ProcessStartInfo("cmd.exe", "/c " + command)
                {
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(processInfo))
                {
                    await process.WaitForExitAsync();
                    var output = process.StandardOutput.ReadToEnd();
                    Console.WriteLine($"Command output: {output}");
                    return process.ExitCode == 0;
                }
            }
            catch
            {
                return false;
            }
        }

        private async Task<bool> SendEmailAsync(string emailInfo)
        {
            try
            {
                // Simple mailto implementation
                var parts = emailInfo.Split('|');
                if (parts.Length >= 3)
                {
                    string recipient = parts[0].Trim();
                    string subject = parts[1].Trim();
                    string body = parts[2].Trim();

                    string mailtoLink = $"mailto:{recipient}?subject={Uri.EscapeDataString(subject)}&body={Uri.EscapeDataString(body)}";
                    Process.Start(new ProcessStartInfo(mailtoLink) { UseShellExecute = true });
                    await Task.Delay(1000);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        private bool CreateNote(string content)
        {
            try
            {
                string fileName = $"Note_{DateTime.Now:yyyyMMdd_HHmmss}.txt";
                string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), fileName);
                System.IO.File.WriteAllText(filePath, content);
                Console.WriteLine($"Note created: {filePath}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
