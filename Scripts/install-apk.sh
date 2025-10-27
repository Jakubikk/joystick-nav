#!/bin/bash
# Install APK to Meta Quest Device
# Usage: ./install-apk.sh [path/to/apk]

echo "========================================="
echo "DecartAI Quest 3 - APK Installer"
echo "========================================="
echo ""

# Check if ADB is installed
if ! command -v adb &> /dev/null; then
    echo "❌ Error: ADB is not installed or not in PATH"
    echo "Please install Android Platform Tools:"
    echo "  macOS: brew install android-platform-tools"
    echo "  Linux: sudo apt-get install android-tools-adb"
    exit 1
fi

# Determine APK path
if [ -z "$1" ]; then
    # No argument provided, look for APK in common locations
    if [ -f "DecartQuest3.apk" ]; then
        APK_PATH="DecartQuest3.apk"
    elif [ -f "../Builds/DecartQuest3.apk" ]; then
        APK_PATH="../Builds/DecartQuest3.apk"
    elif [ -f "../../Builds/DecartQuest3.apk" ]; then
        APK_PATH="../../Builds/DecartQuest3.apk"
    else
        echo "❌ Error: No APK file specified and couldn't find default APK"
        echo "Usage: $0 [path/to/apk]"
        exit 1
    fi
else
    APK_PATH="$1"
fi

# Verify APK exists
if [ ! -f "$APK_PATH" ]; then
    echo "❌ Error: APK file not found: $APK_PATH"
    exit 1
fi

echo "APK: $APK_PATH"
echo ""

# Check for connected devices
echo "Checking for connected Quest device..."
DEVICES=$(adb devices | grep -v "List of devices" | grep -v "^$" | grep "device$" | wc -l)

if [ "$DEVICES" -eq 0 ]; then
    echo "❌ No Quest device detected!"
    echo ""
    echo "Troubleshooting steps:"
    echo "1. Connect Quest to computer via USB cable"
    echo "2. Put on Quest headset"
    echo "3. Accept 'Allow USB Debugging' dialog"
    echo "4. Ensure Developer Mode is enabled in Quest settings"
    echo ""
    echo "Current ADB devices:"
    adb devices
    exit 1
elif [ "$DEVICES" -gt 1 ]; then
    echo "⚠️  Multiple devices detected. Installing to first device..."
fi

echo "✅ Quest device connected"
echo ""

# Uninstall previous version (if exists)
echo "Checking for previous installation..."
PACKAGE_NAME="com.YourCompany.DecartAI-Quest"
if adb shell pm list packages | grep -q "$PACKAGE_NAME"; then
    echo "Uninstalling previous version..."
    adb uninstall "$PACKAGE_NAME"
fi

# Install APK
echo "Installing APK to Quest..."
if adb install -r "$APK_PATH"; then
    echo ""
    echo "========================================="
    echo "✅ Installation Successful!"
    echo "========================================="
    echo ""
    echo "To launch the app:"
    echo "1. Put on your Quest headset"
    echo "2. Press the Meta button (Oculus logo)"
    echo "3. Go to Apps → Filter by 'Unknown Sources'"
    echo "4. Find and launch 'DecartAI-Quest'"
    echo ""
    echo "Grant camera permissions when prompted."
    echo ""
else
    echo ""
    echo "========================================="
    echo "❌ Installation Failed"
    echo "========================================="
    echo ""
    echo "Common issues:"
    echo "- Quest is in sleep mode (wake it up)"
    echo "- USB debugging not authorized (check headset for dialog)"
    echo "- USB cable doesn't support data (try different cable)"
    echo "- Developer Mode not enabled"
    echo ""
    exit 1
fi
