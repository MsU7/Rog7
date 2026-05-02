# 🚀 Getting Started Guide

## 5-Minute Setup

### Step 1: Open Terminal (30 seconds)

**Windows:**
```powershell
# Open PowerShell
WIN + R
Type: powershell
Press: Enter
```

**In PowerShell, navigate to project:**
```powershell
cd C:\Users\c_ish\source\repos\ConsoleApp1\
```

### Step 2: Run Application (1 minute)

**Option A - Easiest (Recommended):**
```powershell
# Windows - Run batch script
quickstart.bat
# Select option 5: "Full setup (Restore + Build + Run)"
```

**Option B - PowerShell:**
```powershell
powershell -ExecutionPolicy Bypass -File quickstart.ps1
# Select option 5
```

**Option C - Manual:**
```powershell
dotnet restore
dotnet build
dotnet run
```

### Step 3: You're Running! (2 minutes)

You'll see:
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
🎤 Command: 
```

### Step 4: Test It (1 minute)

Type your first command:
```
Command: open notepad
✓ Recognized: "open notepad"
⚙️  Executing: open_app
✓ Command executed successfully!
```

**Congratulations! You're up and running!** 🎉

---

## Try These First Commands

### 1. **Take a Screenshot**
```
Command: screenshot
```
Your desktop will be captured and saved to:
`C:\Users\[YourName]\Desktop\ScreenCaptures\`

### 2. **Open an Application**
```
Command: open chrome
```
Google Chrome will launch.

### 3. **Type Some Text**
```
Command: open notepad
[Wait for Notepad to open]
Command: type Hello World
```

### 4. **Search the Web**
```
Command: search python tutorial
```
Google will open with your search results.

### 5. **Exit the App**
```
Command: exit
```

---

## More Example Commands

### Application Control
```
open notepad        → Launch Notepad
open calculator     → Launch Calculator
open chrome         → Launch Chrome browser
close chrome        → Close Chrome
```

### Screen & Capture
```
screenshot          → Save desktop image
capture             → Same as screenshot
show desktop        → Capture screen
```

### Text Input
```
type hello          → Type "hello"
type test message   → Type "test message"
```

### Mouse & Click
```
click               → Click at current position
scroll down         → Scroll page down
scroll up           → Scroll page up
move mouse to 100 200  → Move to coordinates
```

### Web & Search
```
search pizza recipe        → Google: pizza recipe
search weather new york    → Google: weather new york
```

---

## Next: Set Up ChatGPT (Optional)

Once you're comfortable with basic commands, you can add ChatGPT for smarter AI processing.

### Step 1: Get API Key (2 minutes)
1. Go to: https://platform.openai.com/api-keys
2. Sign up or log in
3. Click "Create new secret key"
4. Copy the key (starts with `sk-`)

### Step 2: Save API Key (1 minute)

**PowerShell:**
```powershell
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-your-key-here", "User")
```

**Replace** `sk-your-key-here` with your actual key!

### Step 3: Restart IDE
Close Visual Studio/Code and reopen it.

### Step 4: Run Again
```powershell
dotnet run
```

Now ChatGPT will power your commands! ✨

---

## Documentation

### Need Help?
| Problem | Solution |
|---------|----------|
| Commands not working | See [COMMANDS_REFERENCE.md](COMMANDS_REFERENCE.md) |
| Build issues | See [SETUP_GUIDE.md](SETUP_GUIDE.md#troubleshooting) |
| Want to customize | See [TIPS_AND_TRICKS.md](TIPS_AND_TRICKS.md) |
| Understand the code | See [ARCHITECTURE.md](ARCHITECTURE.md) |
| Quick reference | See [QUICK_REFERENCE.md](QUICK_REFERENCE.md) |

### Documentation Map
```
START HERE:
  ↓
  QUICK_REFERENCE.md (one page)
  or
  SUMMARY.md (overview)
  ↓
  COMMANDS_REFERENCE.md (try commands)
  ↓
  SETUP_GUIDE.md (if you have issues)
  ↓
  README.md (complete reference)
  ↓
  TIPS_AND_TRICKS.md (advanced features)
  ↓
  ARCHITECTURE.md (how it works)
```

---

## Keyboard Shortcuts

In your commands, you can use:

```
{Enter}      = Press Enter key
{Escape}     = Press Escape
^a           = Ctrl+A (select all)
^c           = Ctrl+C (copy)
^v           = Ctrl+V (paste)
^s           = Ctrl+S (save)
```

Example:
```
Command: type hello world{Enter}
Result: Types "hello world" and presses Enter
```

---

## Common Patterns

### Pattern 1: Write and Save
```
1. Command: open notepad
2. [Wait for Notepad to open]
3. Command: type My important note
4. Command: type ^s
   [File save dialog opens]
```

### Pattern 2: Search and Screenshot
```
1. Command: search machine learning tutorial
   [Browser opens with results]
2. Command: screenshot
   [Results saved to Desktop/ScreenCaptures/]
```

### Pattern 3: Open Multiple Apps
```
1. Command: open notepad
2. Command: open chrome
3. Command: open calculator
   [All three apps will open]
```

---

## Troubleshooting First Run

### Problem: "Command not found"
**Solution**: Check spelling. Example: `open notepad` (not `open note pad`)

### Problem: App won't open
**Solution**: App might not be installed. Try another app:
- `open chrome`
- `open notepad`
- `open calculator`

### Problem: Text input doesn't work
**Solution**: The target application must be focused (clicked). Example:
```
Command: open notepad
[Click Notepad window]
Command: type hello
```

### Problem: Screenshot not found
**Solution**: Check this folder:
```
C:\Users\[YourName]\Desktop\ScreenCaptures\
```

### Problem: Nothing happens
**Solution**: Try the "exit" command, restart, and try again:
```
Command: exit
[Then run: dotnet run again]
```

---

## System Requirements

### Required
- Windows 10 or 11
- .NET 10.0 or higher
- PowerShell (optional, but recommended)

### Check Your System
```powershell
# Check .NET version
dotnet --version
# Should show: 10.0.0 or higher
```

### If .NET Not Installed
1. Download from: https://dotnet.microsoft.com/download
2. Install .NET 10.0
3. Restart terminal
4. Try again

---

## Advanced: First Custom Command

Once comfortable, add your own app. Edit this file:
```
Services/DesktopAutomationService.cs
```

Find this section:
```csharp
var appMap = new Dictionary<string, string>
{
	{ "notepad", "notepad.exe" },
	{ "calculator", "calc.exe" },
	// ... more apps ...
};
```

Add your app:
```csharp
{ "myapp", "C:\\Program Files\\MyApp\\app.exe" },
```

Now you can say:
```
Command: open myapp
```

---

## Success Checklist

After completing this guide, you should be able to:

- [ ] Run the application
- [ ] Take a screenshot
- [ ] Open an application
- [ ] Type text
- [ ] Search the web
- [ ] Close the application
- [ ] Understand the command syntax
- [ ] Know where to find help
- [ ] (Optional) Set up ChatGPT API key

**If you've checked all boxes, you're ready!** ✅

---

## Next Steps

### Immediate (Now)
1. **Run the app** using quickstart script
2. **Try commands** from above
3. **Explore** [COMMANDS_REFERENCE.md](COMMANDS_REFERENCE.md)

### Soon (Today/Tomorrow)
1. **Set up ChatGPT** (optional)
2. **Try more commands** from the reference
3. **Create workflows** for your needs

### Later (This Week)
1. **Customize** app mappings
2. **Add custom commands** (if desired)
3. **Read** advanced documentation

### Future
1. **Explore** GUI interface options
2. **Add voice recognition**
3. **Extend** with other APIs

---

## Quick Command Lookup

| Task | Command |
|------|---------|
| Screenshot | `screenshot` |
| Open app | `open chrome` |
| Type text | `type hello` |
| Close app | `close chrome` |
| Search web | `search pizza` |
| Scroll | `scroll down` |
| Click | `click` |
| Exit | `exit` |

---

## Still Stuck?

1. **Restart**: Close and run again
   ```powershell
   dotnet run
   ```

2. **Check docs**: [QUICK_REFERENCE.md](QUICK_REFERENCE.md)

3. **Try example**: Copy from [COMMANDS_REFERENCE.md](COMMANDS_REFERENCE.md)

4. **Online help**: [SETUP_GUIDE.md](SETUP_GUIDE.md#troubleshooting)

5. **Read architecture**: [ARCHITECTURE.md](ARCHITECTURE.md)

---

## Remember

✅ **Works offline** - No internet needed  
✅ **Easy to use** - Start in 5 minutes  
✅ **Well documented** - 9 comprehensive guides  
✅ **Fully functional** - Ready to use now  
✅ **Extensible** - Easy to modify  

**You've got this!** 🚀

---

## One Last Thing

**This is YOUR project.** Feel free to:
- Modify the code
- Add new features
- Create custom commands
- Experiment and play

The best way to learn is by doing. Start using it, break things, fix them, and have fun!

**Enjoy!** 🎉

---

**Ready to start?**
```powershell
cd C:\Users\c_ish\source\repos\ConsoleApp1\
quickstart.bat
```

**Or just:**
```powershell
dotnet run
```

🚀 **Go build something amazing!**
