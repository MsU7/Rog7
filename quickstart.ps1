# AI Desktop Controller - Quick Start PowerShell Script
# Run with: powershell -ExecutionPolicy Bypass -File quickstart.ps1

Write-Host ""
Write-Host "╔════════════════════════════════════════════════════════════╗" -ForegroundColor Cyan
Write-Host "║    🎤 AI Desktop Controller - Quick Start               ║" -ForegroundColor Cyan
Write-Host "║    ChatGPT Voice Command System for Windows              ║" -ForegroundColor Cyan
Write-Host "╚════════════════════════════════════════════════════════════╝" -ForegroundColor Cyan
Write-Host ""

# Check if .NET is installed
try {
	$dotnetVersion = dotnet --version
	Write-Host "✓ .NET installed" -ForegroundColor Green
	Write-Host "$dotnetVersion" -ForegroundColor Green
} catch {
	Write-Host "❌ .NET is not installed or not in PATH" -ForegroundColor Red
	Write-Host "Please download and install .NET 10.0 from:" -ForegroundColor Yellow
	Write-Host "https://dotnet.microsoft.com/download" -ForegroundColor Yellow
	Read-Host "Press Enter to exit"
	exit 1
}

Write-Host ""

# Display menu
do {
	Write-Host "Choose an option:" -ForegroundColor Cyan
	Write-Host "1 - Restore packages" -ForegroundColor White
	Write-Host "2 - Build project" -ForegroundColor White
	Write-Host "3 - Run application" -ForegroundColor White
	Write-Host "4 - Set OpenAI API Key (ChatGPT support)" -ForegroundColor White
	Write-Host "5 - Full setup (Restore + Build + Run)" -ForegroundColor White
	Write-Host "6 - Clean and rebuild" -ForegroundColor White
	Write-Host "7 - Exit" -ForegroundColor White
	Write-Host ""

	$choice = Read-Host "Enter your choice (1-7)"

	switch ($choice) {
		"1" {
			Write-Host ""
			Write-Host "📦 Restoring NuGet packages..." -ForegroundColor Yellow
			dotnet restore
			Read-Host "Press Enter to continue"
		}
		"2" {
			Write-Host ""
			Write-Host "🔨 Building project..." -ForegroundColor Yellow
			dotnet build
			Read-Host "Press Enter to continue"
		}
		"3" {
			Write-Host ""
			Write-Host "🚀 Running application..." -ForegroundColor Yellow
			Write-Host ""
			dotnet run
		}
		"4" {
			Write-Host ""
			Write-Host "🔑 Setting OpenAI API Key" -ForegroundColor Yellow
			Write-Host ""
			Write-Host "Get your API key from: https://platform.openai.com/api-keys" -ForegroundColor Cyan

			$apikey = Read-Host "Enter your OpenAI API key (sk-...)"

			if ($apikey) {
				[Environment]::SetEnvironmentVariable("OPENAI_API_KEY", $apikey, "User")
				Write-Host ""
				Write-Host "✓ API Key saved to environment variables" -ForegroundColor Green
				Write-Host "ℹ️  Restart your IDE for changes to take effect" -ForegroundColor Yellow
			} else {
				Write-Host "❌ No API key entered" -ForegroundColor Red
			}

			Write-Host ""
			Read-Host "Press Enter to continue"
		}
		"5" {
			Write-Host ""
			Write-Host "📦 Restoring packages..." -ForegroundColor Yellow
			dotnet restore
			Write-Host ""
			Write-Host "🔨 Building project..." -ForegroundColor Yellow
			dotnet build
			Write-Host ""
			Write-Host "🚀 Running application..." -ForegroundColor Yellow
			Write-Host ""
			dotnet run
		}
		"6" {
			Write-Host ""
			Write-Host "🧹 Cleaning project..." -ForegroundColor Yellow
			dotnet clean
			Write-Host ""
			Write-Host "🔨 Rebuilding project..." -ForegroundColor Yellow
			dotnet build
			Write-Host ""
			Write-Host "✓ Rebuild complete" -ForegroundColor Green
			Read-Host "Press Enter to continue"
		}
		"7" {
			Write-Host "Goodbye!" -ForegroundColor Green
			exit 0
		}
		default {
			Write-Host "❌ Invalid choice. Please try again." -ForegroundColor Red
			Start-Sleep -Seconds 2
		}
	}

	Write-Host ""

} while ($choice -ne "7")
