@echo off
REM Automated Project Setup Script for Windows
REM This script clones the repository and sets up the development environment

echo =========================================
echo DecartAI Quest 3 - Project Setup Script
echo =========================================
echo.

REM Configuration
set PROJECT_DIR=%USERPROFILE%\UnityProjects
set REPO_URL=https://github.com/Jakubikk/joystick-nav.git
set PROJECT_NAME=joystick-nav

REM Check for Git
echo Checking prerequisites...
where git >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo X Git is not installed.
    echo Please install Git from: https://git-scm.com/download/windows
    echo After installing Git, run this script again.
    pause
    exit /b 1
) else (
    echo + Git is already installed
)

REM Check for ADB
where adb >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo ! ADB is not installed.
    echo Please install Android Platform Tools from:
    echo https://developer.android.com/tools/releases/platform-tools
    echo Extract to C:\platform-tools and add to PATH
    echo.
    echo Continuing without ADB (you can install it later)...
) else (
    echo + ADB is already installed
)

REM Create project directory
echo.
echo Creating project directory at %PROJECT_DIR%...
if not exist "%PROJECT_DIR%" mkdir "%PROJECT_DIR%"
cd /d "%PROJECT_DIR%"

REM Clone or update repository
if exist "%PROJECT_NAME%" (
    echo ! Project directory already exists.
    set /p UPDATE="Do you want to update it? (Y/N): "
    if /i "%UPDATE%"=="Y" (
        echo Updating repository...
        cd "%PROJECT_NAME%"
        git pull origin main
        echo + Repository updated
    )
) else (
    echo Cloning repository...
    git clone %REPO_URL%
    echo + Repository cloned successfully
)

REM Final instructions
echo.
echo =========================================
echo + Setup Complete!
echo =========================================
echo.
echo Next steps:
echo 1. Install Unity Hub from: https://unity.com/download
echo 2. Install Unity 6000.0.34f1 with Android Build Support
echo 3. Open Unity Hub and add project from:
echo    %PROJECT_DIR%\%PROJECT_NAME%\DecartAI-Quest-Unity
echo.
echo For detailed instructions, see:
echo    %PROJECT_DIR%\%PROJECT_NAME%\Documentation\COMPLETE_BEGINNERS_GUIDE.md
echo.
pause
