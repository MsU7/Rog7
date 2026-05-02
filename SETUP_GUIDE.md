# Setup & Configuration Guide

## Quick Start

### 1. Build and Run

```powershell
# Navigate to project directory
cd C:\Users\c_ish\source\repos\ConsoleApp1\

# Restore packages
dotnet restore

# Build
dotnet build

# Run
dotnet run
```

### 2. First Run (Without ChatGPT API)

The application works completely offline using the local command parser:

```
🎤 AI Desktop Controller - Voice Command System
================================================

Initializing voice recognition and AI services...

✓ Voice recognition initialized (Windows Speech API)
⚠️  No OpenAI API key found. Using local command parser...

🎤 Say 'Show Desktop', 'Open Notepad', 'Move Mouse', or any other command...
Say 'Screenshot' to see your desktop
Say 'Exit' to quit

📻 Listening for voice command...
🎤 Command: open notepad
✓ Recognized: "open notepad"
⚙️  Executing: open_app
✓ Command executed successfully!
```

## ChatGPT Integration (Optional)

### Step 1: Get OpenAI API Key

1. Go to https://platform.openai.com/api-keys
2. Sign in with your OpenAI account (create one if needed)
3. Click "Create new secret key"
4. Copy the key (you'll only see it once!)

### Step 2: Set Environment Variable

**Windows PowerShell (Recommended):**

```powershell
# Set the environment variable for current user
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-your-actual-api-key-here", "User")

# Verify it's set
$env:OPENAI_API_KEY
```

**Windows CMD:**

```cmd
setx OPENAI_API_KEY sk-your-actual-api-key-here
```

**Note**: After setting the variable, restart your IDE or terminal for changes to take effect.

### Step 3: Restart IDE

- Close Visual Studio completely
- Reopen it
- Run the application again

Now ChatGPT will be used for intelligent command parsing!

## Supported Local Commands (No API Needed)

### Opening Applications
```
open notepad
open calculator / calc
open word
open excel
open powerpoint
open chrome
open firefox
open edge
open explorer / file explorer
open vs code
open teams
open discord
```

### Application Control
```
close notepad
close [app name]
```

### Text Input
```
type hello world
type test message
```

### Mouse & Screen
```
click
scroll down
scroll up
move mouse to 100,200
screenshot
capture
show desktop
```

### Web & Search
```
search weather in New York
search "how to learn C#"
```

### Other
```
exit
quit
```

## Testing Commands

### Test 1: Basic Application Opening
```
Command: open notepad
Expected: Notepad opens
```

### Test 2: Screenshot
```
Command: screenshot
Expected: Screenshot saved to Desktop\ScreenCaptures\ folder
```

### Test 3: Web Search
```
Command: search for pizza recipes
Expected: Default browser opens Google search
```

### Test 4: System Command
```
Command: run ipconfig
Expected: Command executes and output shown
```

## Advanced Configuration

### Custom Application Mappings

Edit `Services/DesktopAutomationService.cs` in the `OpenApplicationAsync` method:

```csharp
var appMap = new Dictionary<string, string>
{
	{ "notepad", "notepad.exe" },
	{ "my custom app", "C:\\Program Files\\MyApp\\app.exe" }, // Add here
	// ... more mappings
};
```

### ChatGPT Model Selection

Edit `Services/ChatGptService.cs`:

```csharp
var request = new
{
	model = "gpt-3.5-turbo",  // Change to "gpt-4" for better results (costs more)
	// ... rest of config
};
```

### Voice Command Timeout

Edit `Program.cs` in the `RunVoiceControlLoop` method to add delays:

```csharp
Console.WriteLine("\n📻 Listening for voice command...");
// Add await Task.Delay(milliseconds); for custom timeout
```

## Cost Estimation (ChatGPT API)

- **gpt-3.5-turbo**: ~$0.0015 per 1K tokens (~300-400 commands = $0.01)
- **gpt-4**: ~$0.03 per 1K tokens (~30-40 commands = $0.01)

Most command parsing uses <200 tokens, so costs are minimal.

## Troubleshooting

### Problem: Build Fails

```
Error: NU1101: Unable to find package
```

**Solution:**
```powershell
dotnet nuget locals all --clear
dotnet restore --no-cache
dotnet build
```

### Problem: ChatGPT Not Working

1. **Check API key is set:**
   ```powershell
   $env:OPENAI_API_KEY
   ```

2. **Verify API key format:**
   - Should start with `sk-`
   - Should be long (>40 characters)

3. **Check internet connection:**
   ```powershell
   Test-Connection 8.8.8.8
   ```

4. **Verify account has credits:**
   - Visit https://platform.openai.com/account/billing/overview

### Problem: Application Won't Start

1. Ensure .NET 10.0 is installed:
   ```powershell
   dotnet --version
   ```

2. Rebuild solution:
   ```powershell
   dotnet clean
   dotnet build
   ```

3. Check if firewall is blocking:
   - Add `dotnet.exe` to Windows Defender exceptions

### Problem: Voice Commands Not Working

**Note**: Current implementation uses console input. For true voice recognition:

1. Install Windows SDK
2. Reference `Windows.Media.SpeechRecognition` NuGet package
3. Update `VoiceRecognitionService.cs` to use Windows API

Alternatively, use voice-to-text services:
- Windows 10/11 built-in voice recorder
- Third-party voice input tools
- Pipe their output to this application

## Performance Tips

1. **Use local parser** (no API key) for faster responses
2. **Cache frequently used commands** in local parser
3. **Minimize ChatGPT calls** for simple commands
4. **Use gpt-3.5-turbo** instead of gpt-4 for speed

## Security Best Practices

✅ **DO:**
- Store API key in environment variables
- Rotate API key regularly
- Monitor API usage at openai.com
- Use this on trusted machines only

❌ **DON'T:**
- Hardcode API key in source code
- Share API key with others
- Commit API key to version control
- Use untested commands on important systems

## Next Steps

1. **Test the application** with basic commands
2. **Set up ChatGPT API** (optional) for advanced features
3. **Customize app mappings** for your installed applications
4. **Explore advanced commands** for your workflow

---

For more help, see README.md
