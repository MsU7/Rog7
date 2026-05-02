using System;
using System.Threading.Tasks;

namespace ConsoleApp1.Voice
{
    public class VoiceRecognitionService
    {
        private bool _useWindowsAPI = false;

        public VoiceRecognitionService()
        {
            InitializeRecognizer();
        }

        private void InitializeRecognizer()
        {
            try
            {
                // Try to initialize Windows Speech Recognition API
                // If not available, we'll fall back to console input
                Console.WriteLine("✓ Voice recognition system initialized");
                Console.WriteLine("Note: Using console input mode (Windows Speech API requires Windows SDK)");
                _useWindowsAPI = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Voice recognition warning: {ex.Message}");
                Console.WriteLine("Falling back to console input mode");
                _useWindowsAPI = false;
            }
        }

        public async Task<string> ListenAsync()
        {
            try
            {
                if (_useWindowsAPI)
                {
                    // Future implementation for Windows Speech API
                    // Would use Windows.Media.SpeechRecognition here
                }

                // Fallback: read from console
                return await ReadFromConsoleAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Voice recognition error: {ex.Message}");
                return await ReadFromConsoleAsync();
            }
        }

        private async Task<string> ReadFromConsoleAsync()
        {
            return await Task.Run(() =>
            {
                Console.Write("🎤 Command: ");
                return Console.ReadLine() ?? string.Empty;
            });
        }
    }
}
