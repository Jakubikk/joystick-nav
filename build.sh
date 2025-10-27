#!/bin/bash
# Build and Deploy Script for Decart Quest AI App
# Works on macOS and Linux

set -e  # Exit on error

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

echo -e "${GREEN}========================================${NC}"
echo -e "${GREEN}Decart Quest AI - Build & Deploy${NC}"
echo -e "${GREEN}========================================${NC}"
echo ""

# Configuration
UNITY_PATH="/Applications/Unity/Hub/Editor/6000.0.34f1/Unity.app/Contents/MacOS/Unity"
PROJECT_PATH="$(cd "$(dirname "$0")/.." && pwd)/DecartAI-Quest-Unity"
BUILD_PATH="$(cd "$(dirname "$0")/.." && pwd)/Builds"
APK_NAME="DecartQuestAI.apk"
LOG_FILE="$(cd "$(dirname "$0")/.." && pwd)/build.log"

# Check if Unity exists
if [ ! -f "$UNITY_PATH" ]; then
    echo -e "${RED}Error: Unity not found at $UNITY_PATH${NC}"
    echo "Please update UNITY_PATH in this script"
    exit 1
fi

# Check if project exists
if [ ! -d "$PROJECT_PATH" ]; then
    echo -e "${RED}Error: Project not found at $PROJECT_PATH${NC}"
    exit 1
fi

# Create builds directory if it doesn't exist
mkdir -p "$BUILD_PATH"

echo -e "${YELLOW}Project Path: $PROJECT_PATH${NC}"
echo -e "${YELLOW}Build Path: $BUILD_PATH${NC}"
echo -e "${YELLOW}Log File: $LOG_FILE${NC}"
echo ""

# Function to build APK
build_apk() {
    echo -e "${GREEN}Starting Unity build...${NC}"
    echo "This may take 10-30 minutes..."
    echo ""
    
    # Remove old log file
    rm -f "$LOG_FILE"
    
    # Run Unity build in batch mode
    "$UNITY_PATH" \
        -quit \
        -batchmode \
        -nographics \
        -projectPath "$PROJECT_PATH" \
        -buildTarget Android \
        -executeMethod UnityEditor.Android.AndroidBuild.Build \
        -logFile "$LOG_FILE" || {
            echo -e "${RED}Build failed! Check $LOG_FILE for details${NC}"
            tail -n 50 "$LOG_FILE"
            exit 1
        }
    
    # Check if APK was created
    if [ -f "$BUILD_PATH/$APK_NAME" ]; then
        echo -e "${GREEN}✓ Build successful!${NC}"
        echo -e "APK location: $BUILD_PATH/$APK_NAME"
        APK_SIZE=$(du -h "$BUILD_PATH/$APK_NAME" | cut -f1)
        echo -e "APK size: $APK_SIZE"
        return 0
    else
        echo -e "${RED}✗ Build failed - APK not found${NC}"
        echo "Check $LOG_FILE for details"
        tail -n 50 "$LOG_FILE"
        return 1
    fi
}

# Function to check Quest connection
check_quest() {
    echo ""
    echo -e "${GREEN}Checking Quest 3 connection...${NC}"
    
    if ! command -v adb &> /dev/null; then
        echo -e "${RED}Error: adb not found. Please install Android Platform Tools${NC}"
        return 1
    fi
    
    DEVICE=$(adb devices | grep -w "device" | head -n 1 | awk '{print $1}')
    
    if [ -z "$DEVICE" ]; then
        echo -e "${RED}✗ Quest 3 not detected${NC}"
        echo "Please:"
        echo "1. Connect Quest 3 via USB-C cable"
        echo "2. Enable USB debugging in headset"
        echo "3. Run 'adb devices' to verify connection"
        return 1
    else
        echo -e "${GREEN}✓ Quest 3 connected: $DEVICE${NC}"
        return 0
    fi
}

# Function to deploy APK
deploy_apk() {
    echo ""
    echo -e "${GREEN}Deploying to Quest 3...${NC}"
    
    if [ ! -f "$BUILD_PATH/$APK_NAME" ]; then
        echo -e "${RED}Error: APK not found at $BUILD_PATH/$APK_NAME${NC}"
        echo "Please build first"
        return 1
    fi
    
    if ! check_quest; then
        return 1
    fi
    
    echo "Installing APK..."
    adb install -r "$BUILD_PATH/$APK_NAME" || {
        echo -e "${RED}✗ Installation failed${NC}"
        echo "Try:"
        echo "1. Uninstall existing app from Quest"
        echo "2. Run: adb uninstall com.YourCompany.DecartQuestAI"
        echo "3. Try installation again"
        return 1
    }
    
    echo -e "${GREEN}✓ Installation successful!${NC}"
    return 0
}

# Function to launch app
launch_app() {
    echo ""
    echo -e "${GREEN}Launching app on Quest 3...${NC}"
    
    # Try to launch app (package name may need adjustment)
    adb shell am start -n com.YourCompany.DecartQuestAI/com.unity3d.player.UnityPlayerActivity 2>/dev/null || {
        echo -e "${YELLOW}Note: Could not auto-launch app${NC}"
        echo "Please launch manually from Quest headset:"
        echo "1. Press Oculus button"
        echo "2. Go to App Library"
        echo "3. Select 'Unknown Sources'"
        echo "4. Launch 'DecartQuestAI'"
        return 0
    }
    
    echo -e "${GREEN}✓ App launched!${NC}"
    echo "Put on your Quest headset to see the app"
    return 0
}

# Function to show logs
show_logs() {
    echo ""
    echo -e "${GREEN}Fetching Quest 3 logs...${NC}"
    
    if ! check_quest; then
        return 1
    fi
    
    echo "Showing Unity logs (Ctrl+C to stop)..."
    adb logcat -s Unity:V UnityPlayer:V
}

# Main menu
show_menu() {
    echo ""
    echo "What would you like to do?"
    echo "1) Build APK only"
    echo "2) Deploy existing APK to Quest"
    echo "3) Build and Deploy"
    echo "4) Launch app on Quest"
    echo "5) Show Quest logs"
    echo "6) Check Quest connection"
    echo "7) Exit"
    echo ""
    read -p "Enter choice [1-7]: " choice
    
    case $choice in
        1)
            build_apk
            show_menu
            ;;
        2)
            deploy_apk
            show_menu
            ;;
        3)
            build_apk && deploy_apk && launch_app
            show_menu
            ;;
        4)
            launch_app
            show_menu
            ;;
        5)
            show_logs
            show_menu
            ;;
        6)
            check_quest
            show_menu
            ;;
        7)
            echo -e "${GREEN}Goodbye!${NC}"
            exit 0
            ;;
        *)
            echo -e "${RED}Invalid choice${NC}"
            show_menu
            ;;
    esac
}

# Run main menu
show_menu
