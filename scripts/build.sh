#!/bin/bash

##############################################################################
# Automated Unity Build Script for Quest 3 AI Transformation App
# This script automates the Unity build process for Android/Quest
##############################################################################

# Colors for output
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Configuration
PROJECT_NAME="DecartAI-Quest-Unity"
BUILD_OUTPUT_DIR="Builds"
APK_NAME="QuestAI.apk"
LOG_FILE="build_log.txt"

# Try to find Unity installation
if [[ "$OSTYPE" == "darwin"* ]]; then
    # macOS
    UNITY_PATH="/Applications/Unity/Hub/Editor/6000.0.34f1/Unity.app/Contents/MacOS/Unity"
    if [ ! -f "$UNITY_PATH" ]; then
        UNITY_PATH=$(find /Applications/Unity/Hub/Editor -name Unity -type f 2>/dev/null | grep "6000" | head -n 1)
    fi
else
    # Windows/Linux
    UNITY_PATH="C:/Program Files/Unity/Hub/Editor/6000.0.34f1/Editor/Unity.exe"
fi

# Get script directory and project path
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"
PROJECT_PATH="$(dirname "$SCRIPT_DIR")/$PROJECT_NAME"
BUILD_PATH="$(dirname "$SCRIPT_DIR")/$BUILD_OUTPUT_DIR"

echo -e "${BLUE}================================${NC}"
echo -e "${BLUE}Quest 3 App - Automated Build${NC}"
echo -e "${BLUE}================================${NC}"
echo ""

# Verify Unity installation
if [ ! -f "$UNITY_PATH" ] && [ ! -f "$UNITY_PATH.exe" ]; then
    echo -e "${RED}✗ Unity not found at: $UNITY_PATH${NC}"
    echo -e "${YELLOW}  Please update UNITY_PATH in the script${NC}"
    exit 1
fi

echo -e "${GREEN}✓ Unity found${NC}"

# Verify project exists
if [ ! -d "$PROJECT_PATH" ]; then
    echo -e "${RED}✗ Project not found at: $PROJECT_PATH${NC}"
    exit 1
fi

echo -e "${GREEN}✓ Project found${NC}"

# Create build directory
mkdir -p "$BUILD_PATH"
echo -e "${GREEN}✓ Build directory ready${NC}"

# Clean old build (optional)
if [ -f "$BUILD_PATH/$APK_NAME" ]; then
    echo -e "${YELLOW}  Removing old APK...${NC}"
    rm "$BUILD_PATH/$APK_NAME"
fi

echo ""
echo -e "${BLUE}Starting Unity build process...${NC}"
echo -e "${YELLOW}This may take 10-30 minutes depending on your system${NC}"
echo ""

# Build command
"$UNITY_PATH" \
  -quit \
  -batchmode \
  -nographics \
  -projectPath "$PROJECT_PATH" \
  -buildTarget Android \
  -executeMethod BuildAutomation.BuildAndroid \
  -logFile "$LOG_FILE"

BUILD_EXIT_CODE=$?

echo ""
echo -e "${BLUE}================================${NC}"

# Check build result
if [ $BUILD_EXIT_CODE -eq 0 ] && [ -f "$BUILD_PATH/$APK_NAME" ]; then
    APK_SIZE=$(du -h "$BUILD_PATH/$APK_NAME" | cut -f1)
    echo -e "${GREEN}✓ Build completed successfully!${NC}"
    echo ""
    echo "Build Details:"
    echo "  APK Location: $BUILD_PATH/$APK_NAME"
    echo "  APK Size: $APK_SIZE"
    echo "  Log File: $LOG_FILE"
    echo ""
    echo -e "${GREEN}Ready to deploy to Quest 3!${NC}"
    echo "Run ./scripts/deploy.sh to install on your device"
    exit 0
else
    echo -e "${RED}✗ Build failed${NC}"
    echo ""
    echo "Check the log file for details:"
    echo "  $LOG_FILE"
    echo ""
    if [ -f "$LOG_FILE" ]; then
        echo "Last 20 lines of log:"
        tail -n 20 "$LOG_FILE"
    fi
    exit 1
fi
