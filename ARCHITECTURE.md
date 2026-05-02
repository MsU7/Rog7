# 🏗️ System Architecture

## High-Level Architecture

```
┌─────────────────────────────────────────────────────────────────────┐
│                    AI Desktop Controller                             │
│                 (ConsoleApp1 - .NET 10)                              │
└─────────────────────────────────────────────────────────────────────┘

							  ┌──────────────────┐
							  │  Program.Main()  │
							  │  Voice Loop      │
							  └────────┬─────────┘
									   │
					┌──────────────────┼──────────────────┐
					│                  │                  │
					▼                  ▼                  ▼
		┌─────────────────────┐  ┌──────────────┐  ┌──────────────────┐
		│  VoiceRecognition   │  │  ChatGptService  │  │  ScreenCapture   │
		│  Service            │  │  (AI Parser)     │  │  Service         │
		│                     │  │                  │  │                  │
		│ • Input from user   │  │ • OpenAI API     │  │ • Full screen    │
		│ • Console input     │  │ • Local Parser   │  │ • Save PNG       │
		│ • Async listening   │  │ • Command split  │  │ • Clipboard      │
		└──────────┬──────────┘  └────────┬─────────┘  └──────────┬───────┘
				   │                      │                        │
				   └──────────────────────┼────────────────────────┘
										  │
							┌─────────────▼─────────────┐
							│  CommandParsed Object     │
							│  {Action, Target, Params} │
							└─────────────┬─────────────┘
										  │
							┌─────────────▼──────────────────┐
							│  DesktopAutomation Service    │
							│  (Execute Command)            │
							└─────────────┬──────────────────┘
										  │
					┌─────────────────────┼─────────────────────┐
					│                     │                     │
					▼                     ▼                     ▼
			┌────────────────┐   ┌──────────────────┐   ┌────────────────┐
			│  Process API   │   │  Win32 P/Invoke  │   │  Windows Forms │
			│  (Open/Close)  │   │  (Mouse/Keyboard)│   │  (SendKeys)    │
			│                │   │                  │   │                │
			│ • Process.Start│   │ • mouse_event()  │   │ • Type text    │
			│ • Process.Kill│    │ • SetCursorPos() │   │ • Input        │
			│                │   │ • keybd_event()  │   │                │
			└────────────────┘   └──────────────────┘   └────────────────┘
					│                     │                     │
					└─────────────────────┼─────────────────────┘
										  │
							┌─────────────▼──────────────┐
							│   Windows Desktop         │
							│   (Automated Actions)     │
							└───────────────────────────┘
```

## Data Flow

```
User Input (Console)
		│
		▼
VoiceRecognitionService.ListenAsync()
		│
		│ Returns: string (user command)
		│
		▼
ChatGptService.ProcessCommandAsync(input)
		│
		├─ Has API Key?
		│  ├─ YES → Call OpenAI API
		│  │       POST /v1/chat/completions
		│  │       Returns: ChatCompletion JSON
		│  │
		│  └─ NO → Parse Locally
		│          (Dictionary + regex)
		│
		▼
ParseJsonCommand() or ParseCommandLocally()
		│
		│ Returns: CommandParsed
		│ {
		│   Action: "open_app",
		│   Target: "notepad",
		│   Parameters: {...}
		│ }
		│
		▼
DesktopAutomationService.ExecuteCommandAsync(cmd)
		│
		├─ Switch on Action
		│  ├─ "open_app" → OpenApplicationAsync()
		│  ├─ "click" → ClickMouse()
		│  ├─ "type" → TypeText()
		│  ├─ "screenshot" → [Handled in Program]
		│  └─ [11 other actions]
		│
		▼
Windows System Call
		│
		├─ Process.Start() → Launch exe
		├─ SetCursorPos() → Move mouse
		├─ mouse_event() → Click
		├─ SendKeys.SendWait() → Type
		└─ [Windows Forms APIs]
		│
		▼
Desktop Action Executed
		│
		│ Returns: bool (success/failure)
		│
		▼
Program.Main() - Report to User
		│
		▼
Loop for next command
```

## Service Interactions

```
┌─────────────────────────────────────────────────────────────────┐
│                     Program.cs                                   │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │  Main()                                                 │   │
│  │  {                                                      │   │
│  │    Initialize services                                │   │
│  │    ↓                                                   │   │
│  │    RunVoiceControlLoop()                             │   │
│  │    {                                                  │   │
│  │      While(running):                                │   │
│  │        1. Call voiceService.ListenAsync()           │   │
│  │        2. Call chatGptService.ProcessCommandAsync() │   │
│  │        3. Call desktopService.ExecuteCommandAsync()│   │
│  │        4. Report results                            │   │
│  │    }                                                  │   │
│  │  }                                                      │   │
│  └─────────────────────────────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────┘
	  │
	  ├─► VoiceRecognitionService
	  │   • Instance: voiceService
	  │   • Methods: ListenAsync()
	  │   • Purpose: Get user input
	  │
	  ├─► ChatGptService
	  │   • Instance: chatGptService
	  │   • Methods: ProcessCommandAsync(input)
	  │   • Purpose: Parse commands with AI
	  │
	  ├─► DesktopAutomationService
	  │   • Instance: desktopService
	  │   • Methods: ExecuteCommandAsync(command)
	  │   • Purpose: Execute desktop actions
	  │
	  └─► ScreenCaptureService
		  • Instance: screenService
		  • Methods: CaptureScreen()
		  • Purpose: Take screenshots
```

## Class Hierarchy

```
VoiceRecognitionService
├─ private _useWindowsAPI: bool
├─ InitializeRecognizer(): void
└─ ListenAsync(): Task<string>

ChatGptService
├─ private _apiKey: string
├─ private _httpClient: HttpClient
├─ ProcessCommandAsync(input): Task<CommandParsed>
├─ ParseJsonCommand(json, input): CommandParsed
└─ ParseCommandLocally(input): CommandParsed

CommandParsed (Model)
├─ Action: string
├─ Target: string
└─ Parameters: Dictionary<string, object>

DesktopAutomationService
├─ Win32 P/Invoke Methods
├─ ExecuteCommandAsync(cmd): Task<bool>
├─ OpenApplicationAsync(name): Task<bool>
├─ CloseApplication(name): bool
├─ ClickMouse(target): bool
├─ TypeText(text): bool
├─ MoveMouse(target): bool
├─ ScrollMouse(direction): bool
├─ SearchWebAsync(query): Task<bool>
├─ OpenFileAsync(path): Task<bool>
├─ RunCommandAsync(cmd): Task<bool>
├─ SendEmailAsync(info): Task<bool>
└─ CreateNote(content): bool

ScreenCaptureService
├─ CaptureScreen(): string
└─ CaptureScreenToClipboard(): string
```

## Technology Stack

```
┌─────────────────────────────────────────────────────────┐
│              .NET 10.0 Windows                          │
├─────────────────────────────────────────────────────────┤
│                                                          │
│  ┌────────────────────────────────────────────────┐   │
│  │  Application Layer                             │   │
│  │  ├─ Program.cs (Main)                          │   │
│  │  ├─ Voice Input Handler                        │   │
│  │  └─ Command Execution Loop                    │   │
│  └────────────────────────────────────────────────┘   │
│                                                          │
│  ┌────────────────────────────────────────────────┐   │
│  │  Service Layer                                  │   │
│  │  ├─ VoiceRecognition                           │   │
│  │  ├─ ChatGpt AI Integration                     │   │
│  │  ├─ Desktop Automation                         │   │
│  │  └─ Screen Capture                             │   │
│  └────────────────────────────────────────────────┘   │
│                                                          │
│  ┌────────────────────────────────────────────────┐   │
│  │  Async/Await Pattern                           │   │
│  │  └─ Task-based async operations                │   │
│  └────────────────────────────────────────────────┘   │
│                                                          │
│  ┌────────────────────────────────────────────────┐   │
│  │  External APIs                                  │   │
│  │  ├─ OpenAI ChatGPT API (Optional)              │   │
│  │  │  └─ HttpClient + REST                      │   │
│  │  └─ Windows APIs (Win32 P/Invoke)             │   │
│  │     └─ mouse_event, SetCursorPos, etc.        │   │
│  └────────────────────────────────────────────────┘   │
│                                                          │
│  ┌────────────────────────────────────────────────┐   │
│  │  Libraries                                      │   │
│  │  ├─ System.Diagnostics (Process API)           │   │
│  │  ├─ System.Drawing (Screen Capture)            │   │
│  │  ├─ System.Windows.Forms (Desktop Control)    │   │
│  │  ├─ System.Text.Json (JSON Parsing)            │   │
│  │  ├─ System.Net.Http (REST Calls)               │   │
│  │  └─ System.Runtime.InteropServices (P/Invoke) │   │
│  └────────────────────────────────────────────────┘   │
│                                                          │
└─────────────────────────────────────────────────────────┘
```

## Command Processing Pipeline

```
Input: "open notepad"
	│
	▼ (VoiceRecognitionService)
Recognized: "open notepad"
	│
	▼ (ChatGptService)
Has API Key? → YES
	│
	▼ (Call OpenAI API)
POST https://api.openai.com/v1/chat/completions
{
  "model": "gpt-3.5-turbo",
  "messages": [{
	"role": "user",
	"content": "Parse this desktop command: 'open notepad'"
  }]
}
	│
	▼ (OpenAI Response)
{
  "choices": [{
	"message": {
	  "content": "{\"action\": \"open_app\", \"target\": \"notepad\"}"
	}
  }]
}
	│
	▼ (Parse JSON)
CommandParsed {
  Action = "open_app",
  Target = "notepad",
  Parameters = {}
}
	│
	▼ (DesktopAutomationService.ExecuteCommandAsync)
Switch(action) → Case "open_app"
	│
	▼ (OpenApplicationAsync("notepad"))
appMap["notepad"] = "notepad.exe"
	│
	▼ (Process.Start("notepad.exe"))
	│
	▼ (Windows)
Notepad.exe launches
	│
	▼ (Return to Program.Main)
✓ Command executed successfully!
```

## Error Handling Flow

```
Try to execute command
	│
	├─ Success? → Return true
	│
	└─ Exception caught
		│
		├─ VoiceRecognition fails
		│  └─ Fallback to console input
		│
		├─ ChatGpt API fails
		│  └─ Use local command parser
		│
		├─ Local parser unrecognized
		│  └─ Return "unknown" action
		│
		└─ Desktop automation fails
		   └─ Catch Win32 exception
			  └─ Return false
			  └─ Log error message
```

---

## Performance Characteristics

| Operation | Time | Notes |
|-----------|------|-------|
| Voice listen | < 100ms | Poll interval |
| Local parsing | < 10ms | Regex/dict |
| ChatGpt API | 1-5s | Network dependent |
| App launch | 2s | Async delay |
| Screenshot | < 500ms | Bitmap copy |
| Mouse click | < 10ms | Win32 call |
| Keyboard input | < 50ms | Win32 event |

---

## Security Architecture

```
User Input
	│
	▼ (No validation needed - local)
VoiceRecognitionService
	│
	├─ Internet? → ChatGptService
	│  │
	│  ├─ Validate API Key
	│  │  └─ OPENAI_API_KEY env var
	│  │
	│  └─ HTTPS encryption
	│
	└─ DesktopAutomationService
	   │
	   ├─ Win32 API calls
	   │  └─ Require system privileges
	   │
	   └─ File operations
		  └─ Full filesystem access
```

---

This is a clean, modular architecture with:
- Clear separation of concerns
- Async/await throughout
- Graceful fallbacks
- Extensible design
- Error handling at each layer
