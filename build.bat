@echo off
REM Build and Deploy Script for Decart Quest AI App
REM Works on Windows

setlocal EnableDelayedExpansion

echo ========================================
echo Decart Quest AI - Build ^& Deploy
echo ========================================
echo.

REM Configuration
set "UNITY_PATH=C:\Program Files\Unity\Hub\Editor\6000.0.34f1\Editor\Unity.exe"
set "PROJECT_PATH=%~dp0DecartAI-Quest-Unity"
set "BUILD_PATH=%~dp0Builds"
set "APK_NAME=DecartQuestAI.apk"
set "LOG_FILE=%~dp0build.log"

REM Check if Unity exists
if not exist "%UNITY_PATH%" (
    echo [ERROR] Unity not found at %UNITY_PATH%
    echo Please update UNITY_PATH in this script
    pause
    exit /b 1
)

REM Check if project exists
if not exist "%PROJECT_PATH%" (
    echo [ERROR] Project not found at %PROJECT_PATH%
    pause
    exit /b 1
)

REM Create builds directory if it doesn't exist
if not exist "%BUILD_PATH%" mkdir "%BUILD_PATH%"

echo Project Path: %PROJECT_PATH%
echo Build Path: %BUILD_PATH%
echo Log File: %LOG_FILE%
echo.

goto MENU

:BUILD_APK
echo.
echo [BUILD] Starting Unity build...
echo This may take 10-30 minutes...
echo.

REM Remove old log file
if exist "%LOG_FILE%" del "%LOG_FILE%"

REM Run Unity build in batch mode
"%UNITY_PATH%" ^
    -quit ^
    -batchmode ^
    -nographics ^
    -projectPath "%PROJECT_PATH%" ^
    -buildTarget Android ^
    -executeMethod UnityEditor.Android.AndroidBuild.Build ^
    -logFile "%LOG_FILE%"

if !ERRORLEVEL! neq 0 (
    echo [ERROR] Build failed! Check %LOG_FILE% for details
    type "%LOG_FILE%" | findstr /I "error"
    pause
    goto MENU
)

REM Check if APK was created
if exist "%BUILD_PATH%\%APK_NAME%" (
    echo [SUCCESS] Build successful!
    echo APK location: %BUILD_PATH%\%APK_NAME%
    for %%A in ("%BUILD_PATH%\%APK_NAME%") do echo APK size: %%~zA bytes
) else (
    echo [ERROR] Build failed - APK not found
    echo Check %LOG_FILE% for details
    type "%LOG_FILE%" | findstr /I "error"
)

goto MENU

:CHECK_QUEST
echo.
echo [CHECK] Checking Quest 3 connection...

where adb >nul 2>&1
if !ERRORLEVEL! neq 0 (
    echo [ERROR] adb not found. Please install Android Platform Tools
    echo Download from: https://developer.android.com/studio/releases/platform-tools
    pause
    exit /b 1
)

adb devices | findstr /C:"device" >nul
if !ERRORLEVEL! neq 0 (
    echo [ERROR] Quest 3 not detected
    echo Please:
    echo 1. Connect Quest 3 via USB-C cable
    echo 2. Enable USB debugging in headset
    echo 3. Run 'adb devices' to verify connection
    pause
    exit /b 1
) else (
    echo [SUCCESS] Quest 3 connected
    adb devices
)

exit /b 0

:DEPLOY_APK
echo.
echo [DEPLOY] Deploying to Quest 3...

if not exist "%BUILD_PATH%\%APK_NAME%" (
    echo [ERROR] APK not found at %BUILD_PATH%\%APK_NAME%
    echo Please build first
    pause
    goto MENU
)

call :CHECK_QUEST
if !ERRORLEVEL! neq 0 goto MENU

echo Installing APK...
adb install -r "%BUILD_PATH%\%APK_NAME%"

if !ERRORLEVEL! neq 0 (
    echo [ERROR] Installation failed
    echo Try:
    echo 1. Uninstall existing app from Quest
    echo 2. Run: adb uninstall com.YourCompany.DecartQuestAI
    echo 3. Try installation again
    pause
    goto MENU
)

echo [SUCCESS] Installation successful!
goto MENU

:LAUNCH_APP
echo.
echo [LAUNCH] Launching app on Quest 3...

call :CHECK_QUEST
if !ERRORLEVEL! neq 0 goto MENU

REM Try to launch app (package name may need adjustment)
adb shell am start -n com.YourCompany.DecartQuestAI/com.unity3d.player.UnityPlayerActivity 2>nul

if !ERRORLEVEL! neq 0 (
    echo [NOTE] Could not auto-launch app
    echo Please launch manually from Quest headset:
    echo 1. Press Oculus button
    echo 2. Go to App Library
    echo 3. Select 'Unknown Sources'
    echo 4. Launch 'DecartQuestAI'
) else (
    echo [SUCCESS] App launched!
    echo Put on your Quest headset to see the app
)

pause
goto MENU

:SHOW_LOGS
echo.
echo [LOGS] Fetching Quest 3 logs...
echo Press Ctrl+C to stop
echo.

call :CHECK_QUEST
if !ERRORLEVEL! neq 0 goto MENU

adb logcat -s Unity:V UnityPlayer:V
goto MENU

:MENU
echo.
echo ========================================
echo What would you like to do?
echo ========================================
echo 1) Build APK only
echo 2) Deploy existing APK to Quest
echo 3) Build and Deploy
echo 4) Launch app on Quest
echo 5) Show Quest logs
echo 6) Check Quest connection
echo 7) Exit
echo.

set /p choice="Enter choice [1-7]: "

if "%choice%"=="1" goto BUILD_APK
if "%choice%"=="2" goto DEPLOY_APK
if "%choice%"=="3" (
    call :BUILD_APK
    call :DEPLOY_APK
    call :LAUNCH_APP
    goto MENU
)
if "%choice%"=="4" goto LAUNCH_APP
if "%choice%"=="5" goto SHOW_LOGS
if "%choice%"=="6" (
    call :CHECK_QUEST
    pause
    goto MENU
)
if "%choice%"=="7" (
    echo Goodbye!
    exit /b 0
)

echo [ERROR] Invalid choice
goto MENU
