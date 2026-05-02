# 💡 Tips, Tricks & Best Practices

## Best Practices for Using This System

### 1. Command Phrasing

#### DO ✅
- **Be specific**: "open Google Chrome" instead of "open browser"
- **Use proper names**: "calculate 2+2" instead of "do math"
- **One action per command**: "Open notepad" (then) "type hello"
- **Natural language**: "search for pizza recipes" works better than "search pizza"

#### DON'T ❌
- **Don't be vague**: "open the thing" - ambiguous
- **Don't mumble**: Speak clearly
- **Don't combine unrelated tasks**: "open notepad, close calculator, take screenshot"
- **Don't use abbreviations**: "open VS" instead of "open Visual Studio"

### 2. Workflow Optimization

#### Morning Setup Workflow
```
"open outlook"           # Check email
"open teams"             # Check messages
"open vs code"           # Start coding
"screenshot"             # Save current state
```

#### Content Creation Workflow
```
"open notepad"           # Notes
"open firefox"           # Research
"screenshot"             # Capture content
"create note topic ideas" # Save ideas
```

#### Remote Meeting Workflow
```
"open teams"
"open calendar"
"search meeting agenda"
"screenshot"
```

### 3. Advanced Tricks

#### Using Command Chaining (Workaround)
Since the app processes one command at a time, create a batch script:

```powershell
# setup.ps1
dotnet run <<EOF
open notepad
type My Document
screenshot
exit
EOF
```

#### Custom Application Shortcuts
Edit `DesktopAutomationService.cs` to add quick mappings:

```csharp
// In OpenApplicationAsync() appMap dictionary
{ "ide", "C:\\Program Files\\Microsoft Visual Studio\\2022\\Enterprise\\devenv.exe" },
{ "email", "Outlook.exe" },
{ "chat", "Discord.exe" }
```

#### Scheduling Commands
Use Windows Task Scheduler to run commands on schedule:

```powershell
# Create scheduled task
$action = New-ScheduledTaskAction -Execute "dotnet" -Argument "run"
$trigger = New-ScheduledTaskTrigger -At 9AM -Daily
Register-ScheduledTask -TaskName "DailyDesktopSetup" -Action $action -Trigger $trigger
```

### 4. ChatGPT Tips

#### Better Prompts for AI
**Good**: "Open Google Chrome and search for the weather"
- Natural language
- Clear intent
- Specific app names

**Better**: "search for weather"
- System auto-maps to open Chrome
- Cleaner command
- AI handles interpretation

#### Cost Optimization
- Use **local parser** for basic commands (free)
- Use **ChatGPT** only for complex commands (costs money)
- Set environment variable only when needed:
  ```powershell
  $env:OPENAI_API_KEY = ""  # Disable ChatGPT
  ```

#### Model Selection
```csharp
// Change in ChatGptService.cs
model = "gpt-3.5-turbo"  // Fast, cheap (~0.5¢ per command)
model = "gpt-4"          // Slower, expensive (~3¢ per command)
```

### 5. Keyboard Shortcuts

#### In Application (Simulated via SendKeys)
```
"type {Enter}"           # Press Enter
"type {Escape}"          # Press Escape
"type ^a"                # Ctrl+A (select all)
"type ^c"                # Ctrl+C (copy)
"type ^v"                # Ctrl+V (paste)
```

Syntax reference (from SendKeys):
- `^` = Ctrl
- `+` = Shift
- `%` = Alt
- `{key}` = Special key

### 6. Troubleshooting Common Issues

#### Issue: Commands Work Sometimes, Fail Other Times
**Causes**:
- API rate limiting
- Network latency
- Window focus issues

**Solutions**:
- Add delays between commands
- Ensure target window is focused
- Check OpenAI quota

#### Issue: Mouse Commands Don't Work
**Causes**:
- Screen resolution mismatch
- Multiple monitors
- DPI scaling

**Solutions**:
- Test with specific coordinates
- Check cursor position first
- Use relative movements

#### Issue: Text Input Shows in Wrong Window
**Causes**:
- Window lost focus
- Timing issue
- Modal dialog appeared

**Solutions**:
- Click target window explicitly first
- Use application-specific shortcuts
- Wait for window to fully load

### 7. Automation Patterns

#### Pattern 1: Data Entry
```
"open excel"
"type Column Header"
"type {Tab}"
"type Value"
"type {Enter}"
```

#### Pattern 2: Web Search & Browse
```
"search recipe for chocolate cake"
[System opens browser with search results]
"screenshot"  # Capture results
```

#### Pattern 3: File Management
```
"open explorer"
"type C:\Users\Documents"
"type {Enter}"
"screenshot"  # Show contents
```

#### Pattern 4: Communication
```
"send email to john@example.com|Project Update|Attached files for review"
[Email client opens with prefilled data]
```

### 8. Security Tips

#### Protecting Your API Key
```powershell
# ✅ Good: Environment variable
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "sk-...", "User")

# ❌ Bad: Hardcoded in code
var apiKey = "sk-xxx";

# ❌ Bad: In config file
<key>OPENAI_API_KEY</key>
<value>sk-xxx</value>
```

#### Safe Command Patterns
```
# ✅ Safe
"open C:\Users\YourName\Documents\file.txt"

# ⚠️ Risky - could navigate anywhere
"run explorer"  # Then user could access sensitive areas

# ❌ Dangerous
"run format c:"  # Would format entire drive!
```

#### Restricted Commands
Consider adding validation for:
- System commands (whitelist approach)
- File paths (sandbox certain directories)
- Application launches (whitelist apps)

### 9. Performance Optimization

#### Local vs. ChatGPT
```
Local Parser: < 10ms, Free, No internet needed
ChatGPT API: 1-5s, ~0.15¢ per command, Better AI

Strategy:
- Use local parser for common commands
- Use ChatGPT for complex/unusual requests
- Never enable API key if not needed
```

#### Reduce API Calls
```csharp
// Cache frequently used commands
private static Dictionary<string, CommandParsed> _commandCache = new();

public async Task<CommandParsed> ProcessCommandAsync(string input)
{
	if (_commandCache.TryGetValue(input.ToLower(), out var cached))
		return cached;  // Return cached result

	// Otherwise, process normally
}
```

### 10. Extending the System

#### Adding Custom Actions
1. Edit `DesktopAutomationService.cs`
2. Add new case in `ExecuteCommandAsync` switch:
```csharp
"custom_action" => await CustomActionAsync(command.Target),

private async Task<bool> CustomActionAsync(string target)
{
	// Your custom logic here
	return true;
}
```

3. Update `ChatGptService.cs` to recognize it:
```csharp
if (lower.Contains("your trigger phrase"))
	return new CommandParsed { Action = "custom_action", Target = input };
```

#### Adding New Applications
Edit `DesktopAutomationService.cs` appMap:
```csharp
var appMap = new Dictionary<string, string>
{
	// Add your application
	{ "your app", "C:\\Program Files\\YourApp\\app.exe" }
};
```

#### Custom Voice Recognition
Replace `VoiceRecognitionService.cs` with:
- Windows Speech API implementation
- Azure Speech Services
- Google Speech-to-Text API
- Local Vosk library

---

## Real-World Scenarios

### Scenario 1: Data Analysis Workflow
```
1. "open excel"
2. "open chrome" (reference data)
3. "screenshot" (save reference)
4. "open vs code" (write analysis)
5. "create note findings"
```

### Scenario 2: Content Creation
```
1. "search latest trends"
2. "screenshot" (capture ideas)
3. "open notepad" (outline)
4. "create note content structure"
5. "search more content"
```

### Scenario 3: Development Workflow
```
1. "open vs code"
2. "run dotnet build"
3. "open chrome" (check docs)
4. "screenshot" (capture error)
5. "create note bug details"
6. "search stack overflow fix"
```

### Scenario 4: System Maintenance
```
1. "run ipconfig" (check network)
2. "run disk usage" (check storage)
3. "screenshot" (save metrics)
4. "create note maintenance tasks"
```

---

## Keyboard/Mouse Combinations

### Text Entry Patterns
```
"type {Backspace}{Backspace}{Backspace}"  # Delete last 3 chars
"type ^a"                                   # Select all
"type ^x"                                   # Cut
"type {Delete}"                             # Delete key
```

### Navigation Patterns
```
"type {Home}"               # Go to start of line
"type {End}"                # Go to end of line
"type ^{Right}"             # Jump to next word
"type +{Right Right Right}" # Select 3 characters
```

### Control Patterns
```
"type %{Tab}"      # Alt+Tab (switch windows)
"type ^s"          # Ctrl+S (save)
"type {F5}"        # F5 (refresh)
"type ^{F5}"       # Ctrl+F5 (hard refresh)
```

---

## Monitoring & Logging

### Enable Verbose Output
```csharp
// In Program.cs
Console.WriteLine($"DEBUG: Command: {command.Action}");
Console.WriteLine($"DEBUG: Target: {command.Target}");
Console.WriteLine($"DEBUG: Success: {result}");
```

### Save Command History
```csharp
private static List<string> _commandHistory = new();

public async Task<CommandParsed> ProcessCommandAsync(string input)
{
	_commandHistory.Add($"{DateTime.Now}: {input}");
	// Process command...
}
```

---

## Maintenance

### Regular Tasks
- [ ] Monitor OpenAI API usage monthly
- [ ] Update .NET packages quarterly
- [ ] Review application mappings if installing new apps
- [ ] Backup environment variables

### Troubleshooting Checklist
- [ ] Is .NET 10.0+ installed?
- [ ] Are NuGet packages installed?
- [ ] Is API key valid (if using ChatGPT)?
- [ ] Is internet connection working?
- [ ] Is application running with admin rights?
- [ ] Are Windows APIs accessible?

---

## Advanced Configuration Examples

### Only Use Local Parser (No Internet)
```powershell
# Clear API key
[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", "", "User")
```

### Use ChatGPT Exclusively
```csharp
// In ChatGptService.cs
// Comment out the fallback to force ChatGPT
if (result.StatusCode != HttpStatusCode.OK)
	throw new Exception("ChatGPT unavailable");
```

### Add Application-Specific Commands
```csharp
// In ChatGptService.cs ParseCommandLocally()
if (lower.Contains("compile"))
	return new CommandParsed { Action = "run_command", Target = "dotnet build" };

if (lower.Contains("test"))
	return new CommandParsed { Action = "run_command", Target = "dotnet test" };
```

---

## Final Tips

1. **Start Simple**: Use local commands first (no API key)
2. **Test Thoroughly**: Test each command individually
3. **Document**: Keep notes of custom commands you add
4. **Backup**: Save your environment variables
5. **Monitor**: Check API usage and costs monthly
6. **Extend Gradually**: Add features incrementally
7. **Automate**: Use scheduling for repetitive tasks
8. **Secure**: Never share API keys or source code with keys
9. **Optimize**: Prefer local parser for speed and cost
10. **Enjoy**: Have fun automating your desktop!

---

For more information, see:
- README.md - Complete documentation
- SETUP_GUIDE.md - Installation guide
- COMMANDS_REFERENCE.md - Command examples
- ARCHITECTURE.md - System design
