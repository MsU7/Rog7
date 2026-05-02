@echo off
REM AI Desktop Controller - Quick Start Script
REM This script builds and runs the .NET 10 application

echo.
echo ╔════════════════════════════════════════════════════════════╗
echo ║    🎤 AI Desktop Controller - Quick Start                  ║
echo ║    ChatGPT Voice Command System for Windows                ║
echo ╚════════════════════════════════════════════════════════════╝
echo.

REM Check if .NET is installed
dotnet --version >nul 2>&1
if errorlevel 1 (
	echo ❌ .NET is not installed or not in PATH
	echo Please download and install .NET 10.0 from:
	echo https://dotnet.microsoft.com/download
	pause
	exit /b 1
)

echo ✅ .NET is installed
echo.

:menu
echo ═══════════════════════════════════════════════════════════
echo Choose an option:
echo ═══════════════════════════════════════════════════════════
echo 1 - Run application (quick)
echo 2 - Build and run
echo 3 - Clean and rebuild
echo 4 - Restore packages
echo 5 - Set OpenAI API Key
echo 6 - Exit
echo.

set /p choice="Enter your choice (1-6): "

if "%choice%"=="1" (
	echo.
	echo 🚀 Running application...
	echo.
	dotnet run --no-build
	echo.
	pause
	goto menu
) else if "%choice%"=="2" (
	echo.
	echo 🔨 Building and running...
	echo.
	dotnet build
	if errorlevel 1 (
		echo ❌ Build failed!
		pause
		goto menu
	)
	echo.
	echo 🚀 Running application...
	echo.
	dotnet run --no-build
	echo.
	pause
	goto menu
) else if "%choice%"=="3" (
	echo.
	echo 🧹 Cleaning and rebuilding...
	dotnet clean
	dotnet build
	echo.
	echo ✅ Rebuild complete!
	echo.
	pause
	goto menu
) else if "%choice%"=="4" (
	echo.
	echo 📦 Restoring NuGet packages...
	dotnet restore
	echo.
	pause
	goto menu
) else if "%choice%"=="5" (
	echo.
	echo 🔑 Setting OpenAI API Key
	echo.
	echo Get your API key from: https://platform.openai.com/api-keys
	set /p apikey="Enter your OpenAI API key (sk-...): "
	setx OPENAI_API_KEY "%apikey%"
	echo.
	echo ✓ API Key saved to environment variables
	echo ℹ️  You may need to restart your IDE for changes to take effect
	echo.
	pause
	goto menu
) else if "%choice%"=="6" (
	echo.
	echo 👋 Goodbye!
	exit /b 0
) else (
	echo.
	echo ❌ Invalid choice. Please try again.
	echo.
	pause
	goto menu
)

:end
pause
exit /b 0
	echo.
	dotnet run
) else if "%choice%"=="6" (
	echo.
	echo 🧹 Cleaning project...
	dotnet clean
	echo.
	echo 🔨 Rebuilding project...
	dotnet build
	echo.
	echo ✓ Rebuild complete
	pause
) else if "%choice%"=="7" (
	echo Goodbye!
	exit /b 0
) else (
	echo ❌ Invalid choice. Please try again.
	timeout /t 2
	goto :start
)

pause
cd C:\Users\c_ish\source\repos\ConsoleApp1\
quickstart.bat
# Or just: dotnet run
