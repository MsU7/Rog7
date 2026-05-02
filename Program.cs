using System;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("🎤 AI Desktop Controller - Voice Command System");
            Console.WriteLine("================================================\n");

            Console.WriteLine("✓ Application started successfully\n");

            await RunSimpleMenu();
        }

        static async Task RunSimpleMenu()
        {
            bool running = true;

            Console.WriteLine("📋 Available Commands:");
            Console.WriteLine("  • open notepad");
            Console.WriteLine("  • open calculator");
            Console.WriteLine("  • exit\n");

            while (running)
            {
                Console.Write("🎤 Enter command: ");
                string input = Console.ReadLine() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("⚠️  Please enter a command.\n");
                    continue;
                }

                input = input.ToLower().Trim();

                // Check for exit
                if (input.Contains("exit") || input.Contains("quit"))
                {
                    Console.WriteLine("👋 Goodbye!\n");
                    running = false;
                    break;
                }

                // Open apps
                if (input.Contains("open notepad"))
                {
                    try
                    {
                        System.Diagnostics.Process.Start("notepad.exe");
                        Console.WriteLine("✓ Notepad opened\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ Error: {ex.Message}\n");
                    }
                    continue;
                }

                if (input.Contains("open calculator"))
                {
                    try
                    {
                        System.Diagnostics.Process.Start("calc.exe");
                        Console.WriteLine("✓ Calculator opened\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"✗ Error: {ex.Message}\n");
                    }
                    continue;
                }

                Console.WriteLine("⚠️  Command not recognized. Try 'open notepad' or 'open calculator'\n");
            }
        }
    }
}
