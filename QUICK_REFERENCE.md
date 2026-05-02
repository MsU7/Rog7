# 🎯 Quick Reference Card

## One-Page Cheat Sheet

### Getting Started (30 seconds)

```powershell
# Option 1: Batch
cd C:\Users\c_ish\source\repos\ConsoleApp1\
quickstart.bat
# Choose 5

# Option 2: PowerShell
dotnet run

# Option 3: Manual
dotnet restore && dotnet build && dotnet run
```

---

## Most Used Commands

| Command | Result |
|---------|--------|
| `open notepad` | Launch Notepad |
| `open chrome` | Launch Chrome |
| `screenshot` | Save desktop screenshot |
| `type hello world` | Type text |
| `click` | Click at cursor |
| `scroll down` | Scroll down |
| `search pizza` | Google search |
| `exit` | Quit application |

---

## Application Names

```
Notepad: notepad
Calculator: calculator, calc
Chrome: chrome
Firefox: firefox
Edge: edge
Word: word
Excel: excel
PowerPoint: powerpoint
Teams: teams
Discord: discord
Explorer: explorer, file explorer
VS Code: vs code
Visual Studio: visual studio
```

---

## Setup ChatGPT (2 minutes)

```powershell
# 1. Get key from https://platform.openai.com/api-keys

# 2. Set environment variable
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-...", "User")

# 3. Restart IDE and run:
dotnet run
```

---

## Troubleshooting

| Problem | Fix |
|---------|-----|
| Won't build | `dotnet clean && dotnet restore && dotnet build` |
| API key not working | Verify key: `$env:OPENAI_API_KEY` |
| No internet | Use local parser (app works offline) |
| Mouse/keyboard offline | Use console text commands |
| Specific app won't open | Check app is installed or add full path |

---

## Keyboard Shortcuts (SendKeys Syntax)

```
{Enter}       = Enter key
{Escape}      = Escape key
{Backspace}   = Backspace
{Delete}      = Delete
{Home}        = Home key
{End}         = End key
{F5}          = F5 key
^a            = Ctrl+A (select all)
^c            = Ctrl+C (copy)
^v            = Ctrl+V (paste)
^s            = Ctrl+S (save)
%{Tab}        = Alt+Tab (switch window)
+{Right}      = Shift+Right (select)
```

---

## Command Structure

```
[Action] [Target] [Parameters]

Examples:
open      notepad                    [open app]
type      hello world                [type text]
screenshot                           [no params]
search    machine learning           [web search]
send email to john@example.com|Hi|Hello  [email]
```

---

## Chat GPT-Powered? 🤖

**With API Key** → Uses ChatGPT AI  
**Without API Key** → Uses local parser  
**Works Offline** → Always has fallback  

Check: `$env:OPENAI_API_KEY` is set → Uses ChatGPT

---

## File Locations

```
Code:        C:\Users\c_ish\source\repos\ConsoleApp1\
Screenshots: C:\Users\[YourName]\Desktop\ScreenCaptures\
Notes:       C:\Users\[YourName]\Desktop\Note_*.txt
```

---

## Common Workflows

### Morning Setup (Copy-Paste)
```
open outlook
open teams
open chrome
screenshot
```

### Content Workflow
```
search trending topics
screenshot
open notepad
type My Ideas
```

### Developer Workflow
```
open vs code
run dotnet build
open chrome
screenshot
```

---

## Advanced: Custom Commands

**Add new app mapping:**
```csharp
// File: Services/DesktopAutomationService.cs
// In OpenApplicationAsync() method, add to appMap:
{ "myapp", "C:\\Program Files\\MyApp\\app.exe" }
```

**Create new action type:**
```csharp
// File: Services/DesktopAutomationService.cs
"my_action" => await MyActionAsync(cmd.Target),

private async Task<bool> MyActionAsync(string target) {
	// Your code here
	return true;
}
```

---

## Cost Estimate

| Model | Cost | Speed |
|-------|------|-------|
| **No API** | $0 | Instant |
| **gpt-3.5** | $0.15/100 cmds | 1-3s |
| **gpt-4** | $3/100 cmds | 3-5s |

**Recommendation**: Use local parser for common commands, ChatGPT for complex ones

---

## Environment Variables

```powershell
# Set ChatGPT API Key
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-...", "User")

# Check if set
$env:OPENAI_API_KEY

# Clear it
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "", "User")
```

---

## Files You Can Edit

| File | Purpose | Difficulty |
|------|---------|------------|
| `Services/DesktopAutomationService.cs` | Add apps, custom actions | ⭐⭐ |
| `Services/ChatGptService.cs` | Change AI model, improve parser | ⭐⭐⭐ |
| `Voice/VoiceRecognitionService.cs` | Add real voice recognition | ⭐⭐⭐⭐ |
| `Program.cs` | Modify command loop, add logging | ⭐⭐ |

---

## Performance Tips

1. **Use local parser** for speed (no network latency)
2. **Avoid ChatGPT** for simple commands (save money)
3. **Test locally** before using ChatGPT (gpt-3.5 is cheaper)
4. **Cache** frequently used commands
5. **Batch operations** - do multiple actions in sequence

---

## Security Checklist

- [ ] Never hardcode API key
- [ ] Use environment variables only
- [ ] Don't commit `.git` with API key
- [ ] Rotate key if exposed
- [ ] Only run on trusted machines
- [ ] Monitor API usage monthly
- [ ] Use strong unique commands for system operations

---

## Support Quick Links

| Need | Link |
|------|------|
| **Overview** | [README.md](README.md) |
| **Setup Help** | [SETUP_GUIDE.md](SETUP_GUIDE.md) |
| **Commands** | [COMMANDS_REFERENCE.md](COMMANDS_REFERENCE.md) |
| **Architecture** | [ARCHITECTURE.md](ARCHITECTURE.md) |
| **Advanced** | [TIPS_AND_TRICKS.md](TIPS_AND_TRICKS.md) |
| **All Docs** | [INDEX.md](INDEX.md) |

---

## Version Info

```
.NET Version: 10.0 (Windows)
Framework: .NET
Platform: Windows 10/11
License: Free for personal use
Status: Ready to use
```

---

## One Minute Setup

```powershell
# 1. Navigate to project
cd C:\Users\c_ish\source\repos\ConsoleApp1\

# 2. Run
dotnet run

# 3. Try a command
screenshot

# 4. Done! ✓
```

---

## Getting Help

**Can't find something?** → Check [INDEX.md](INDEX.md)  
**Build problems?** → See [SETUP_GUIDE.md](SETUP_GUIDE.md#troubleshooting)  
**Need command examples?** → See [COMMANDS_REFERENCE.md](COMMANDS_REFERENCE.md)  
**Want to extend?** → See [TIPS_AND_TRICKS.md](TIPS_AND_TRICKS.md)  

---

## Remember

✅ **Works offline** - No internet needed  
✅ **ChatGPT optional** - API key is optional  
✅ **Easy to extend** - Well-documented code  
✅ **Fully functional** - Ready to use now  
✅ **Secure** - Use environment variables  

**Start with**: `dotnet run` 🚀
