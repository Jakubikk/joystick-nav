#!/bin/bash
# Deploy script for Quest 3 Decart AI Application
# Usage: ./deploy.sh [--device DEVICE_ID] [--all] [--launch]

set -e

# Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m'

# Default values
DEVICE_ID=""
DEPLOY_ALL=false
AUTO_LAUNCH=true
PACKAGE_NAME="com.DefaultCompany.DecartAI-Quest"
APK_PATH="$(cd "$(dirname "${BASH_SOURCE[0]}")/.." && pwd)/Builds/DecartAI-Quest-debug.apk"

# Parse arguments
while [[ $# -gt 0 ]]; do
    case $1 in
        --device)
            DEVICE_ID="$2"
            shift 2
            ;;
        --all)
            DEPLOY_ALL=true
            shift
            ;;
        --no-launch)
            AUTO_LAUNCH=false
            shift
            ;;
        --apk)
            APK_PATH="$2"
            shift 2
            ;;
        --help)
            echo "Usage: ./deploy.sh [OPTIONS]"
            echo ""
            echo "Options:"
            echo "  --device DEVICE_ID    Deploy to specific device"
            echo "  --all                 Deploy to all connected devices"
            echo "  --no-launch          Don't auto-launch after install"
            echo "  --apk PATH           Path to APK file"
            echo "  --help               Show this help message"
            exit 0
            ;;
        *)
            echo -e "${RED}Unknown option: $1${NC}"
            exit 1
            ;;
    esac
done

# Check if ADB is available
check_adb() {
    if ! command -v adb &> /dev/null; then
        echo -e "${RED}Error: ADB not found${NC}"
        echo "Please install Android SDK Platform Tools"
        exit 1
    fi
}

# Check if APK exists
check_apk() {
    if [ ! -f "$APK_PATH" ]; then
        echo -e "${RED}Error: APK not found at $APK_PATH${NC}"
        echo "Please build the project first with: ./build.sh"
        exit 1
    fi
}

# Get list of connected devices
get_devices() {
    adb devices | grep -v "List" | grep "device$" | awk '{print $1}'
}

# Deploy to a single device
deploy_to_device() {
    local device=$1
    echo -e "${YELLOW}Deploying to device: $device${NC}"
    
    # Install APK
    echo "Installing APK..."
    adb -s "$device" install -r "$APK_PATH" || {
        echo -e "${RED}Installation failed for device: $device${NC}"
        return 1
    }
    
    echo -e "${GREEN}Installation successful on $device${NC}"
    
    # Launch app if requested
    if [ "$AUTO_LAUNCH" = true ]; then
        echo "Launching application..."
        adb -s "$device" shell am start -n "$PACKAGE_NAME/com.unity3d.player.UnityPlayerActivity" || {
            echo -e "${YELLOW}Warning: Could not launch app on $device${NC}"
        }
    fi
    
    return 0
}

# Main execution
main() {
    echo -e "${GREEN}=== Quest 3 Decart AI Deploy Script ===${NC}"
    
    check_adb
    check_apk
    
    # Get connected devices
    devices=($(get_devices))
    
    if [ ${#devices[@]} -eq 0 ]; then
        echo -e "${RED}Error: No Quest devices connected${NC}"
        echo "Please connect your Quest via USB and enable USB debugging"
        exit 1
    fi
    
    echo -e "Found ${GREEN}${#devices[@]}${NC} connected device(s)"
    echo ""
    
    # Deploy based on options
    if [ "$DEPLOY_ALL" = true ]; then
        echo "Deploying to all devices..."
        for device in "${devices[@]}"; do
            deploy_to_device "$device" &
        done
        wait
    elif [ -n "$DEVICE_ID" ]; then
        deploy_to_device "$DEVICE_ID"
    else
        # Deploy to first device
        echo "Deploying to first device: ${devices[0]}"
        deploy_to_device "${devices[0]}"
    fi
    
    echo ""
    echo -e "${GREEN}=== Deployment Complete ===${NC}"
}

main
