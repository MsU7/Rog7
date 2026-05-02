# 📋 Example Commands Reference

## Categories

- [Application Control](#application-control)
- [Desktop Navigation](#desktop-navigation)
- [Text & Input](#text--input)
- [Web & Search](#web--search)
- [File Operations](#file-operations)
- [System Control](#system-control)
- [Advanced Commands](#advanced-commands)

---

## Application Control

### Open Applications

```
"open notepad"
Expected: Notepad launches

"open calculator"
Expected: Windows Calculator opens

"open chrome"
Expected: Google Chrome launches

"open word"
Expected: Microsoft Word opens

"open excel"
Expected: Microsoft Excel launches

"open vs code"
Expected: Visual Studio Code opens

"open file explorer"
Expected: Windows File Explorer opens

"open teams"
Expected: Microsoft Teams starts

"launch discord"
Expected: Discord application opens
```

### Close Applications

```
"close notepad"
Expected: Notepad window closes

"close chrome"
Expected: Google Chrome closes

"close all tabs"
Expected: All browser tabs close (if focused)

"quit explorer"
Expected: File Explorer closes
```

---

## Desktop Navigation

### Screenshots & Screen Capture

```
"screenshot"
Expected: Screen captured and saved to Desktop/ScreenCaptures/

"take screenshot"
Expected: Same as above

"capture screen"
Expected: Screenshot taken and opens in default viewer

"show desktop"
Expected: Current desktop captured

"copy screenshot"
Expected: Screenshot copied to clipboard
```

### Mouse & Cursor Control

```
"move mouse to 100 200"
Expected: Cursor moves to position (100, 200)

"click"
Expected: Left mouse click at current position

"click at 500 300"
Expected: Click at specific coordinates

"right click"
Expected: Right mouse click (context menu)

"double click"
Expected: Double mouse click
```

### Scrolling

```
"scroll down"
Expected: Page scrolls down

"scroll up"
Expected: Page scrolls up

"scroll down more"
Expected: Scroll down further
```

---

## Text & Input

### Type Text

```
"type hello world"
Expected: "hello world" is typed in active window

"type test@example.com"
Expected: Email address is typed

"type password123"
Expected: Text typed (careful with passwords!)

"type thank you for watching"
Expected: Phrase is typed
```

### Special Key Combinations

```
"press enter"
Expected: Enter key pressed

"press delete"
Expected: Delete key pressed

"control a"
Expected: Ctrl+A (select all)

"control c"
Expected: Ctrl+C (copy)

"control v"
Expected: Ctrl+V (paste)
```

---

## Web & Search

### Web Search

```
"search weather in New York"
Expected: Google search opens for "weather in New York"

"search how to learn C sharp"
Expected: Search results for C# learning

"google python tutorial"
Expected: Google search for "python tutorial"

"search music on youtube"
Expected: YouTube search opens
```

### Web Navigation

```
"go to google.com"
Expected: Google homepage opens in default browser

"visit youtube"
Expected: YouTube.com opens

"open reddit"
Expected: Reddit.com opens in browser
```

---

## File Operations

### Open Files

```
"open C:\Users\Documents\myfile.txt"
Expected: File opens in default application

"open desktop"
Expected: Desktop folder opens

"open downloads"
Expected: Downloads folder opens

"open documents"
Expected: Documents folder opens
```

### Create Notes

```
"create note important meeting tomorrow"
Expected: Text file created on Desktop with content

"note remember to call john"
Expected: Note file created and saved
```

---

## Email Operations

### Send Email

```
"send email to john@example.com|Hello|This is a test message"
Expected: Email compose window opens with recipient, subject, and body

"email jane@company.com|Meeting|Let's schedule a meeting for tomorrow"
Expected: Email compose opens with all details filled in

Format: recipient|subject|body
```

---

## System Control

### Run Commands

```
"run ipconfig"
Expected: Command executes, network config displayed

"run tasklist"
Expected: List of running processes shown

"run notepad c:\file.txt"
Expected: Notepad opens with specific file

"run powershell"
Expected: PowerShell terminal opens

"run cmd"
Expected: Command Prompt opens
```

### System Information

```
"show system info"
Expected: System information displayed

"check disk space"
Expected: Disk usage information shown
```

---

## Advanced Commands

### Combined Operations

```
"search weather and show me the desktop"
Expected: Both actions performed

"open notepad and take a screenshot"
Expected: Notepad opens, then screenshot taken

"close chrome and open firefox"
Expected: Close Chrome, then open Firefox
```

### Voice Command Variations

The system understands various phrasings:

```
For "open":
- "open notepad"
- "launch notepad"
- "start notepad"
- "run notepad"

For "close":
- "close notepad"
- "close the window"
- "exit notepad"
- "quit notepad"

For "screenshot":
- "screenshot"
- "take a screenshot"
- "capture screen"
- "show desktop"
- "print screen"
```

---

## Practical Workflows

### Work Session Setup
```
"open word"                    # Open document editor
"open excel"                   # Open spreadsheet
"open chrome"                  # Open browser
"open teams"                   # Open communication
```

### Developer Workflow
```
"open vs code"                 # Open code editor
"run dotnet build"             # Build project
"open powershell"              # Open terminal
"search stack overflow fix"    # Search for solution
"screenshot"                   # Capture error for reference
```

### Content Creator Workflow
```
"open notepad"                 # Open notes
"screenshot"                   # Capture content
"open discord"                 # Check messages
"search trending topics"       # Research ideas
```

### Meeting Preparation
```
"open outlook"                 # Check calendar
"open teams"                   # Test conferencing
"search meeting agenda"        # Prepare
"create note meeting points"   # Take notes
```

---

## Tips for Best Results

### 1. Be Specific
✅ Good: "open visual studio code"
❌ Bad: "open the code thing"

### 2. Use Clear Pronunciation
✅ Good: "open Note-Pad" (clear syllables)
❌ Bad: "opnnpad" (mumbled)

### 3. Complete Commands
✅ Good: "search for Italian restaurants near me"
❌ Bad: "restaurants" (too vague)

### 4. Use Familiar App Names
✅ Good: "open chrome" (well-known name)
❌ Bad: "open my browser" (ambiguous)

### 5. One Action Per Command
✅ Good: "open notepad" then "type hello"
❌ Bad: "open notepad and type hello" (might fail)

---

## Troubleshooting Command Issues

### Command Not Recognized

**Problem**: "No command recognized"

**Solution**: 
- Speak clearly and slowly
- Use exact application names
- Check application is installed
- Try local command parser (no API key)

### Partial Command Executed

**Problem**: Only part of command works

**Solution**:
- Use single command per request
- Check each word is recognized
- Use simpler phrasing

### Application Won't Open

**Problem**: "Failed to open [app]"

**Solution**:
- Verify application is installed
- Use full path: "open C:\Program Files\App\app.exe"
- Check app is in system PATH
- Use different app name

### ChatGPT Not Processing

**Problem**: Falls back to local parser

**Solution**:
- Verify API key is set: `$env:OPENAI_API_KEY`
- Check internet connection
- Verify OpenAI account has credits
- Check API key is valid

---

## Command Statistics

**Total Supported Commands**: 50+
**Online (ChatGPT) Commands**: Unlimited (natural language)
**Offline (Local Parser) Commands**: 40+
**Application Control**: 15+
**Desktop Control**: 8+
**File Operations**: 5+

---

For more info, see:
- README.md - Complete documentation
- SETUP_GUIDE.md - Installation & configuration
- Program.cs - Source code with comments
