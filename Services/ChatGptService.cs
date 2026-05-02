using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections.Generic;

namespace ConsoleApp1.Services
{
    public class CommandParsed
    {
        public string Action { get; set; }
        public string Target { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new();
    }

    public class ChatGptService
    {
        private readonly string _apiKey;
        private readonly HttpClient _httpClient;
        private const string ApiUrl = "https://api.openai.com/v1/chat/completions";

        public ChatGptService()
        {
            try
            {
                // Replace with your actual OpenAI API key or read from environment variable
                _apiKey = Environment.GetEnvironmentVariable("OPENAI_API_KEY") ?? "sk-your-api-key-here";
                _httpClient = new HttpClient();

                // Set a reasonable timeout
                _httpClient.Timeout = TimeSpan.FromSeconds(30);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: ChatGptService initialization: {ex.Message}");
                _apiKey = "sk-your-api-key-here";
                _httpClient = new HttpClient();
            }
        }

        public async Task<CommandParsed> ProcessCommandAsync(string userInput)
        {
            try
            {
                // If no valid API key, use local command parsing
                if (_apiKey == "sk-your-api-key-here" || string.IsNullOrEmpty(_apiKey))
                {
                    Console.WriteLine("⚠️  No OpenAI API key found. Using local command parser...");
                    return ParseCommandLocally(userInput);
                }

                var request = new
                {
                    model = "gpt-3.5-turbo",
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                            content = "You are a desktop automation assistant. Parse user voice commands into structured actions. " +
                                    "Return JSON with: {\"action\": \"action_name\", \"target\": \"target_name\", \"parameters\": {...}}. " +
                                    "Actions: open_app, close_app, click, type, screenshot, move_mouse, scroll, open_file, run_command, search_web, email, schedule, note."
                        },
                        new
                        {
                            role = "user",
                            content = $"Parse this desktop command: '{userInput}'"
                        }
                    },
                    temperature = 0.5,
                    max_tokens = 200
                };

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                content.Headers.Add("Authorization", $"Bearer {_apiKey}");

                var response = await _httpClient.PostAsync(ApiUrl, content);
                var responseText = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"⚠️  ChatGPT API error: {response.StatusCode}");
                    return ParseCommandLocally(userInput);
                }

                var jsonResponse = JsonDocument.Parse(responseText);
                var message = jsonResponse.RootElement
                    .GetProperty("choices")[0]
                    .GetProperty("message")
                    .GetProperty("content")
                    .GetString();

                return ParseJsonCommand(message, userInput);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ChatGPT error: {ex.Message}. Using local parser...");
                return ParseCommandLocally(userInput);
            }
        }

        private CommandParsed ParseJsonCommand(string jsonResponse, string originalInput)
        {
            try
            {
                // Try to extract JSON from response
                var startIdx = jsonResponse.IndexOf('{');
                var endIdx = jsonResponse.LastIndexOf('}');

                if (startIdx >= 0 && endIdx > startIdx)
                {
                    var jsonStr = jsonResponse.Substring(startIdx, endIdx - startIdx + 1);
                    var doc = JsonDocument.Parse(jsonStr);

                    var action = doc.RootElement.TryGetProperty("action", out var actionProp)
                        ? actionProp.GetString() ?? "unknown"
                        : "unknown";

                    var target = doc.RootElement.TryGetProperty("target", out var targetProp)
                        ? targetProp.GetString() ?? ""
                        : "";

                    return new CommandParsed { Action = action, Target = target };
                }
            }
            catch { }

            return ParseCommandLocally(originalInput);
        }

        private CommandParsed ParseCommandLocally(string input)
        {
            var lower = input.ToLower();

            // Open application
            if (lower.Contains("open"))
            {
                if (lower.Contains("notepad")) return new CommandParsed { Action = "open_app", Target = "notepad" };
                if (lower.Contains("calculator")) return new CommandParsed { Action = "open_app", Target = "calc" };
                if (lower.Contains("word")) return new CommandParsed { Action = "open_app", Target = "winword" };
                if (lower.Contains("excel")) return new CommandParsed { Action = "open_app", Target = "excel" };
                if (lower.Contains("chrome")) return new CommandParsed { Action = "open_app", Target = "chrome" };
                if (lower.Contains("firefox")) return new CommandParsed { Action = "open_app", Target = "firefox" };
                if (lower.Contains("edge")) return new CommandParsed { Action = "open_app", Target = "msedge" };
                if (lower.Contains("explorer") || lower.Contains("file")) return new CommandParsed { Action = "open_app", Target = "explorer" };
            }

            // Close application
            if (lower.Contains("close")) return new CommandParsed { Action = "close_app", Target = input };

            // Type text
            if (lower.Contains("type")) return new CommandParsed { Action = "type", Target = input.Replace("type", "").Trim() };

            // Screenshot
            if (lower.Contains("screenshot") || lower.Contains("capture")) return new CommandParsed { Action = "screenshot", Target = "" };

            // Click
            if (lower.Contains("click")) return new CommandParsed { Action = "click", Target = input.Replace("click", "").Trim() };

            // Scroll
            if (lower.Contains("scroll"))
            {
                var direction = lower.Contains("down") ? "down" : lower.Contains("up") ? "up" : "down";
                return new CommandParsed { Action = "scroll", Target = direction };
            }

            // Search
            if (lower.Contains("search")) return new CommandParsed { Action = "search_web", Target = input.Replace("search", "").Trim() };

            // Default
            return new CommandParsed { Action = "unknown", Target = input };
        }
    }
}
