@echo off
REM Install APK to Meta Quest Device
REM Usage: install-apk.bat [path\to\apk]

echo =========================================
echo DecartAI Quest 3 - APK Installer
echo =========================================
echo.

REM Check if ADB is installed
where adb >nul 2>nul
if %ERRORLEVEL% NEQ 0 (
    echo X Error: ADB is not installed or not in PATH
    echo Please install Android Platform Tools from:
    echo https://developer.android.com/tools/releases/platform-tools
    echo.
    pause
    exit /b 1
)

REM Determine APK path
if "%~1"=="" (
    REM No argument provided, look for APK in common locations
    if exist "DecartQuest3.apk" (
        set APK_PATH=DecartQuest3.apk
    ) else if exist "..\Builds\DecartQuest3.apk" (
        set APK_PATH=..\Builds\DecartQuest3.apk
    ) else if exist "..\..\Builds\DecartQuest3.apk" (
        set APK_PATH=..\..\Builds\DecartQuest3.apk
    ) else (
        echo X Error: No APK file specified and couldn't find default APK
        echo Usage: %0 [path\to\apk]
        pause
        exit /b 1
    )
) else (
    set APK_PATH=%~1
)

REM Verify APK exists
if not exist "%APK_PATH%" (
    echo X Error: APK file not found: %APK_PATH%
    pause
    exit /b 1
)

echo APK: %APK_PATH%
echo.

REM Check for connected devices
echo Checking for connected Quest device...
adb devices | find "device" >nul
if %ERRORLEVEL% NEQ 0 (
    echo X No Quest device detected!
    echo.
    echo Troubleshooting steps:
    echo 1. Connect Quest to computer via USB cable
    echo 2. Put on Quest headset
    echo 3. Accept 'Allow USB Debugging' dialog
    echo 4. Ensure Developer Mode is enabled in Quest settings
    echo.
    echo Current ADB devices:
    adb devices
    pause
    exit /b 1
)

echo + Quest device connected
echo.

REM Uninstall previous version (if exists)
echo Checking for previous installation...
set PACKAGE_NAME=com.YourCompany.DecartAI-Quest
adb shell pm list packages | find "%PACKAGE_NAME%" >nul
if %ERRORLEVEL% EQU 0 (
    echo Uninstalling previous version...
    adb uninstall %PACKAGE_NAME%
)

REM Install APK
echo Installing APK to Quest...
adb install -r "%APK_PATH%"

if %ERRORLEVEL% EQU 0 (
    echo.
    echo =========================================
    echo + Installation Successful!
    echo =========================================
    echo.
    echo To launch the app:
    echo 1. Put on your Quest headset
    echo 2. Press the Meta button (Oculus logo^)
    echo 3. Go to Apps -^> Filter by 'Unknown Sources'
    echo 4. Find and launch 'DecartAI-Quest'
    echo.
    echo Grant camera permissions when prompted.
    echo.
) else (
    echo.
    echo =========================================
    echo X Installation Failed
    echo =========================================
    echo.
    echo Common issues:
    echo - Quest is in sleep mode (wake it up^)
    echo - USB debugging not authorized (check headset for dialog^)
    echo - USB cable doesn't support data (try different cable^)
    echo - Developer Mode not enabled
    echo.
)

pause
