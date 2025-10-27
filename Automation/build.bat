@echo off
REM Build script for Quest 3 Decart AI Application (Windows)
REM Usage: build.bat [--platform quest|pc|both] [--config debug|release]

setlocal enabledelayedexpansion

REM Default values
set PLATFORM=quest
set CONFIG=debug
set UNITY_PATH=C:\Program Files\Unity\Hub\Editor\6000.0.34f1\Editor\Unity.exe
set PROJECT_PATH=%~dp0..\DecartAI-Quest-Unity
set BUILD_OUTPUT=%~dp0..\Builds

REM Parse arguments
:parse_args
if "%1"=="" goto end_parse
if "%1"=="--platform" (
    set PLATFORM=%2
    shift
    shift
    goto parse_args
)
if "%1"=="--config" (
    set CONFIG=%2
    shift
    shift
    goto parse_args
)
if "%1"=="--help" (
    echo Usage: build.bat [OPTIONS]
    echo.
    echo Options:
    echo   --platform [quest^|pc^|both]    Target platform (default: quest)
    echo   --config [debug^|release]      Build configuration (default: debug)
    echo   --help                        Show this help message
    exit /b 0
)
shift
goto parse_args

:end_parse

echo === Quest 3 Decart AI Build Script ===
echo Platform: %PLATFORM%
echo Configuration: %CONFIG%
echo.

REM Check Unity installation
if not exist "%UNITY_PATH%" (
    echo Error: Unity not found at %UNITY_PATH%
    echo Please update UNITY_PATH in this script
    exit /b 1
)

REM Create build output directory
if not exist "%BUILD_OUTPUT%" mkdir "%BUILD_OUTPUT%"
echo Build output directory: %BUILD_OUTPUT%
echo.

REM Build based on platform
if "%PLATFORM%"=="quest" goto build_quest
if "%PLATFORM%"=="pc" goto build_pc
if "%PLATFORM%"=="both" goto build_both
echo Invalid platform: %PLATFORM%
exit /b 1

:build_quest
echo Building for Meta Quest 3...
"%UNITY_PATH%" -quit -batchmode ^
    -projectPath "%PROJECT_PATH%" ^
    -buildTarget Android ^
    -logFile "%BUILD_OUTPUT%\build-quest-%CONFIG%.log"

if errorlevel 1 (
    echo Build failed! Check log: %BUILD_OUTPUT%\build-quest-%CONFIG%.log
    exit /b 1
)
echo Quest build completed!
goto end

:build_pc
echo Building for PC...
"%UNITY_PATH%" -quit -batchmode ^
    -projectPath "%PROJECT_PATH%" ^
    -buildTarget Win64 ^
    -logFile "%BUILD_OUTPUT%\build-pc-%CONFIG%.log"

if errorlevel 1 (
    echo Build failed! Check log: %BUILD_OUTPUT%\build-pc-%CONFIG%.log
    exit /b 1
)
echo PC build completed!
goto end

:build_both
call :build_quest
call :build_pc
goto end

:end
echo.
echo === Build Complete ===
echo Build artifacts: %BUILD_OUTPUT%
endlocal
