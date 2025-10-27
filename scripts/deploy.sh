#!/bin/bash

##############################################################################
# Automated Quest 3 Deployment Script
# This script deploys the built APK to a connected Quest 3 device
##############################################################################

# Colors for output
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Configuration
APK_NAME="QuestAI.apk"
PACKAGE_NAME="com.decart.questai"
BUILD_OUTPUT_DIR="Builds"

# Get script directory and build path
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
BUILD_PATH="$(dirname "$SCRIPT_DIR")/$BUILD_OUTPUT_DIR"
APK_PATH="$BUILD_PATH/$APK_NAME"

# Find ADB
if [[ "$OSTYPE" == "darwin"* ]]; then
    # macOS
    ADB_PATH="$HOME/Library/Android/sdk/platform-tools/adb"
else
    # Windows
    ADB_PATH="C:/Users/$USER/AppData/Local/Android/Sdk/platform-tools/adb.exe"
fi

# Alternative ADB locations
if [ ! -f "$ADB_PATH" ]; then
    # Try Unity's Android SDK
    if [[ "$OSTYPE" == "darwin"* ]]; then
        ADB_PATH="/Applications/Unity/Hub/Editor/6000.0.34f1/PlaybackEngines/AndroidPlayer/SDK/platform-tools/adb"
    else
        ADB_PATH="C:/Program Files/Unity/Hub/Editor/6000.0.34f1/Editor/Data/PlaybackEngines/AndroidPlayer/SDK/platform-tools/adb.exe"
    fi
fi

echo -e "${BLUE}================================${NC}"
echo -e "${BLUE}Quest 3 App - Deployment${NC}"
echo -e "${BLUE}================================${NC}"
echo ""

# Check if ADB exists
if [ ! -f "$ADB_PATH" ]; then
    echo -e "${RED}✗ ADB not found${NC}"
    echo -e "${YELLOW}  Expected location: $ADB_PATH${NC}"
    echo ""
    echo "Please install Android SDK or update ADB_PATH in the script"
    exit 1
fi

echo -e "${GREEN}✓ ADB found${NC}"

# Check if APK exists
if [ ! -f "$APK_PATH" ]; then
    echo -e "${RED}✗ APK not found at: $APK_PATH${NC}"
    echo ""
    echo "Please run ./scripts/build.sh first to create the APK"
    exit 1
fi

APK_SIZE=$(du -h "$APK_PATH" | cut -f1)
echo -e "${GREEN}✓ APK found ($APK_SIZE)${NC}"

# Check device connection
echo ""
echo "Checking for connected Quest device..."

# Start ADB server if needed
"$ADB_PATH" start-server > /dev/null 2>&1

# Get device list
DEVICES=$("$ADB_PATH" devices | grep -v "List" | grep "device$" | wc -l | tr -d ' ')

if [ "$DEVICES" -eq "0" ]; then
    echo -e "${RED}✗ No Quest device connected${NC}"
    echo ""
    echo "Please:"
    echo "  1. Connect your Quest 3 via USB cable"
    echo "  2. Put on the headset"
    echo "  3. Enable USB debugging when prompted"
    echo "  4. Run this script again"
    exit 1
fi

echo -e "${GREEN}✓ Quest device connected${NC}"

# Get device info
DEVICE_MODEL=$("$ADB_PATH" shell getprop ro.product.model 2>/dev/null | tr -d '\r')
DEVICE_VERSION=$("$ADB_PATH" shell getprop ro.build.version.release 2>/dev/null | tr -d '\r')

echo "  Device: $DEVICE_MODEL"
echo "  Android: $DEVICE_VERSION"

# Uninstall old version (if exists)
echo ""
echo "Checking for existing installation..."
if "$ADB_PATH" shell pm list packages | grep -q "$PACKAGE_NAME"; then
    echo -e "${YELLOW}  Uninstalling old version...${NC}"
    "$ADB_PATH" uninstall "$PACKAGE_NAME" > /dev/null 2>&1
    echo -e "${GREEN}  ✓ Old version removed${NC}"
else
    echo "  No previous installation found"
fi

# Install new APK
echo ""
echo "Installing new APK..."
INSTALL_OUTPUT=$("$ADB_PATH" install "$APK_PATH" 2>&1)

if echo "$INSTALL_OUTPUT" | grep -q "Success"; then
    echo -e "${GREEN}✓ Installation successful!${NC}"
    
    # Launch app automatically
    echo ""
    echo "Launching app on Quest..."
    "$ADB_PATH" shell am start -n "$PACKAGE_NAME/com.unity3d.player.UnityPlayerActivity" > /dev/null 2>&1
    
    if [ $? -eq 0 ]; then
        echo -e "${GREEN}✓ App launched${NC}"
    else
        echo -e "${YELLOW}  Could not auto-launch app${NC}"
        echo "  Please launch manually from Quest headset"
    fi
    
    echo ""
    echo -e "${BLUE}================================${NC}"
    echo -e "${GREEN}Deployment Complete!${NC}"
    echo -e "${BLUE}================================${NC}"
    echo ""
    echo "App Details:"
    echo "  Package: $PACKAGE_NAME"
    echo "  APK Size: $APK_SIZE"
    echo "  Device: $DEVICE_MODEL"
    echo ""
    echo "Put on your Quest headset to use the app!"
    echo ""
    echo "Find it in: Apps → All → Unknown Sources → DecartAI Quest"
    exit 0
else
    echo -e "${RED}✗ Installation failed${NC}"
    echo ""
    echo "Error details:"
    echo "$INSTALL_OUTPUT"
    echo ""
    echo "Common solutions:"
    echo "  - Make sure Quest is unlocked"
    echo "  - Try unplugging and replugging USB"
    echo "  - Check USB cable supports data transfer"
    echo "  - Verify Developer Mode is enabled"
    exit 1
fi
