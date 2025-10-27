#!/bin/bash
# Build script for Quest 3 Decart AI Application
# Usage: ./build.sh [--platform quest|pc|both] [--config debug|release]

set -e  # Exit on error

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Default values
PLATFORM="quest"
CONFIG="debug"
UNITY_PATH=""
PROJECT_PATH="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)/DecartAI-Quest-Unity"
BUILD_OUTPUT="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)/Builds"

# Parse arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        --platform)
            PLATFORM="$2"
            shift 2
            ;;
        --config)
            CONFIG="$2"
            shift 2
            ;;
        --help)
            echo "Usage: ./build.sh [OPTIONS]"
            echo ""
            echo "Options:"
            echo "  --platform [quest|pc|both]    Target platform (default: quest)"
            echo "  --config [debug|release]      Build configuration (default: debug)"
            echo "  --help                        Show this help message"
            exit 0
            ;;
        *)
            echo -e "${RED}Unknown option: $1${NC}"
            exit 1
            ;;
    esac
done

# Find Unity installation
find_unity() {
    if [[ "$OSTYPE" == "darwin"* ]]; then
        # macOS
        UNITY_PATH="/Applications/Unity/Hub/Editor/6000.0.34f1/Unity.app/Contents/MacOS/Unity"
    elif [[ "$OSTYPE" == "linux-gnu"* ]]; then
        # Linux
        UNITY_PATH="$HOME/Unity/Hub/Editor/6000.0.34f1/Editor/Unity"
    else
        # Windows (Git Bash)
        UNITY_PATH="/c/Program Files/Unity/Hub/Editor/6000.0.34f1/Editor/Unity.exe"
    fi
    
    if [ ! -f "$UNITY_PATH" ]; then
        echo -e "${RED}Error: Unity not found at $UNITY_PATH${NC}"
        echo "Please update UNITY_PATH in this script to point to your Unity installation"
        exit 1
    fi
}

# Create build output directory
create_build_dir() {
    mkdir -p "$BUILD_OUTPUT"
    echo -e "${GREEN}Build output directory: $BUILD_OUTPUT${NC}"
}

# Build for Quest (Android)
build_quest() {
    echo -e "${YELLOW}Building for Meta Quest 3...${NC}"
    
    local OUTPUT_FILE="$BUILD_OUTPUT/DecartAI-Quest-$CONFIG.apk"
    local BUILD_METHOD="BuildPipeline.BuildPlayer"
    
    "$UNITY_PATH" -quit -batchmode \
        -projectPath "$PROJECT_PATH" \
        -buildTarget Android \
        -executeMethod BuildScript.BuildQuest \
        -logFile "$BUILD_OUTPUT/build-quest-$CONFIG.log" \
        || {
            echo -e "${RED}Build failed! Check log: $BUILD_OUTPUT/build-quest-$CONFIG.log${NC}"
            exit 1
        }
    
    echo -e "${GREEN}Quest build completed: $OUTPUT_FILE${NC}"
}

# Build for PC (Windows)
build_pc() {
    echo -e "${YELLOW}Building for PC (Windows)...${NC}"
    
    local OUTPUT_FILE="$BUILD_OUTPUT/DecartAI-PC-$CONFIG.exe"
    
    "$UNITY_PATH" -quit -batchmode \
        -projectPath "$PROJECT_PATH" \
        -buildTarget Win64 \
        -executeMethod BuildScript.BuildPC \
        -logFile "$BUILD_OUTPUT/build-pc-$CONFIG.log" \
        || {
            echo -e "${RED}Build failed! Check log: $BUILD_OUTPUT/build-pc-$CONFIG.log${NC}"
            exit 1
        }
    
    echo -e "${GREEN}PC build completed: $OUTPUT_FILE${NC}"
}

# Main execution
main() {
    echo -e "${GREEN}=== Quest 3 Decart AI Build Script ===${NC}"
    echo -e "Platform: ${YELLOW}$PLATFORM${NC}"
    echo -e "Configuration: ${YELLOW}$CONFIG${NC}"
    echo ""
    
    find_unity
    create_build_dir
    
    case $PLATFORM in
        quest)
            build_quest
            ;;
        pc)
            build_pc
            ;;
        both)
            build_quest
            build_pc
            ;;
        *)
            echo -e "${RED}Invalid platform: $PLATFORM${NC}"
            exit 1
            ;;
    esac
    
    echo ""
    echo -e "${GREEN}=== Build Complete ===${NC}"
    echo -e "Build artifacts: ${YELLOW}$BUILD_OUTPUT${NC}"
}

main
