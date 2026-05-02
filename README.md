# 🎤 AI Desktop Controller - ChatGPT Voice Command System

A powerful Windows desktop automation application that lets you control your entire desktop using voice commands processed by ChatGPT and local AI.

## Features

✅ **Voice Command Recognition**
- Natural language voice input (console-based, with Windows Speech API support)
- Fallback to text input if voice unavailable

✅ **ChatGPT Integration**
- Intelligent command parsing using OpenAI API
- Fallback to local rule-based command parser (works offline)
- Natural language understanding for complex commands

✅ **Full Desktop Automation**
- Open/close applications
- Mouse and keyboard control
- Window management
- File operations
- Web search and navigation
- Email composition
- Note creation

✅ **Screen Capture**
- Take screenshots and save to Desktop/ScreenCaptures
- Auto-open in default image viewer
- Copy to clipboard option

✅ **Offline & Online Support**
- Works without internet (local command parser)
- ChatGPT API integration for advanced features
- Graceful fallback mechanism

## Installation

### Prerequisites
- Windows 10/11
- .NET 10.0 or higher
- Visual Studio 2022 (recommended)

### Setup

1. **Clone or download the project**
```bash
cd C:\Users\c_ish\source\repos\ConsoleApp1\
```

2. **Install NuGet packages**
```bash
dotnet restore
```

3. **Build the project**
```bash
dotnet build
```

4. **Configure OpenAI API (Optional but Recommended)**
   - Get your API key from https://platform.openai.com/api-keys
   - Set environment variable:
   ```powershell
   [Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-your-actual-key", "User")
   ```
   - Restart Visual Studio or terminal for changes to take effect

## Usage

### Running the Application

```bash
dotnet run
```

### Voice Commands Examples

#### Application Control
- "Open Notepad"
- "Open Chrome"
- "Close Notepad"
- "Launch Visual Studio"

#### Desktop Navigation
- "Take Screenshot" / "Capture" / "Show Desktop"
- "Move Mouse to 100,200"
- "Click"
- "Scroll Down"

#### Text Input
- "Type Hello World"
- "Type test@example.com"

#### Web & File Operations
- "Search for weather in New York"
- "Open C:\Users\Documents\file.txt"
- "Send email to john@example.com | Meeting | Let's schedule a meeting"

#### System Commands
- "Run ipconfig" (executes command)
- "Create note" (saves text note to Desktop)

#### Exit
- "Exit" or "Quit"

## Architecture

### Core Services

1. **VoiceRecognitionService** (`Voice/VoiceRecognitionService.cs`)
   - Handles voice input from user
   - Currently uses console input with future Windows API support
   - Asynchronous listening

2. **ChatGptService** (`Services/ChatGptService.cs`)
   - Processes natural language commands
   - Sends requests to OpenAI API
   - Falls back to local parsing if API unavailable
   - Returns structured `CommandParsed` objects

3. **DesktopAutomationService** (`Services/DesktopAutomationService.cs`)
   - Executes parsed commands
   - P/Invoke calls to Windows API for mouse/keyboard control
   - Process management for opening/closing applications
   - Supports 12+ different command types

4. **ScreenCaptureService** (`Services/ScreenCaptureService.cs`)
   - Captures full screen images
   - Saves to PNG format
   - Clipboard support

## Command Types

| Action | Example |
|--------|---------|
| `open_app` | Open Notepad, Launch Chrome |
| `close_app` | Close Notepad, Quit Explorer |
| `click` | Click at coordinates |
| `type` | Type text input |
| `move_mouse` | Move cursor to position |
| `scroll` | Scroll up/down |
| `screenshot` | Capture screen |
| `search_web` | Search Google |
| `open_file` | Open file by path |
| `run_command` | Execute system command |
| `email` | Send email |
| `note` | Create note file |

## Local Command Parser

The application includes a built-in command parser that works **completely offline**. Examples:

- "open notepad" → Opens Notepad
- "search google" → Opens Google search
- "close chrome" → Closes Chrome
- "type hello" → Types "hello"
- "screenshot" → Captures screen

This ensures the app always works, even without internet or ChatGPT API access.

## Advanced Configuration

### Environment Variables

```powershell
# Set OpenAI API Key (optional)
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-xxx", "User")

# Verify it's set
$env:OPENAI_API_KEY
```

### ChatGPT API Setup

1. Visit https://platform.openai.com/api-keys
2. Create a new API key
3. Set it as `OPENAI_API_KEY` environment variable
4. Application will automatically use it

### Using Local Parser Only

If you don't want to use ChatGPT:
- Don't set the `OPENAI_API_KEY` environment variable
- The app will use the local command parser automatically

## Project Structure

```
ConsoleApp1/
├── Program.cs                          # Main entry point & voice control loop
├── Voice/
│   └── VoiceRecognitionService.cs     # Voice input handling
├── Services/
│   ├── ChatGptService.cs              # ChatGPT integration & command parsing
│   ├── DesktopAutomationService.cs    # Desktop automation (mouse, keyboard, etc.)
│   └── ScreenCaptureService.cs        # Screenshot capture
├── ConsoleApp1.csproj                 # Project configuration
└── README.md                           # This file
```

## Dependencies

- **System.Drawing.Common** (8.0.0) - Screen capture functionality
- **System.Windows.Forms** - Desktop automation, clipboard, screen capture
- Built-in .NET 10.0 libraries for HTTP requests, JSON processing

## Troubleshooting

### Build Issues

**Error: "Unable to find package"**
- Run: `dotnet restore --no-cache`
- Update NuGet: `nuget update -self`

**Error: "The target platform must be set to Windows"**
- Already fixed in csproj (uses `net10.0-windows`)

### Runtime Issues

**"No voice commands recognized"**
- Normal behavior in console mode
- Type commands directly into console
- For real voice recognition, use Windows Speech Recognition API (requires additional setup)

**"ChatGPT not working"**
- Check internet connection
- Verify API key is set correctly
- Check OpenAI account has credits

**Application won't open files**
- Use full paths: `C:\Users\YourName\Documents\file.txt`
- Ensure file exists

## Security Considerations

⚠️ **WARNING**: This application provides full desktop control. Security implications:

1. **API Key**: Keep your OpenAI API key secret
   - Never commit to version control
   - Use environment variables
   - Regenerate if accidentally exposed

2. **Desktop Access**: Only run on machines you own/control
   - Voice commands execute immediately
   - No safety dialogs

3. **Network Security**: If extending with remote access:
   - Implement authentication
   - Use encryption for communication
   - Limit command scope

4. **File Access**: Be careful with file operations
   - Commands can read/write any accessible file
   - Validate file paths

## Future Enhancements

- [ ] Real Windows Speech Recognition API integration
- [ ] Remote desktop control over network
- [ ] Custom voice profiles
- [ ] Command history and undo
- [ ] Advanced gesture recognition
- [ ] Multi-monitor support
- [ ] Custom macro creation
- [ ] Scheduling commands

## License

Use freely for personal projects. Respect OpenAI's terms when using their API.

## Support

For issues or questions:
1. Check the Troubleshooting section
2. Verify all dependencies are installed
3. Ensure you're using .NET 10.0+
4. Test with local command parser first (no API key)

---

**Built with ❤️ for Windows Desktop Automation**
