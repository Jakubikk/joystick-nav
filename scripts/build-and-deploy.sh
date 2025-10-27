#!/bin/bash

##############################################################################
# Combined Build and Deploy Script
# Builds the APK and immediately deploys it to Quest 3
##############################################################################

# Colors for output
GREEN='\033[0;32m'
RED='\033[0;31m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m' # No Color

# Get script directory
SCRIPT_DIR="$( cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd )"

echo -e "${BLUE}========================================${NC}"
echo -e "${BLUE}Quest 3 App - Build & Deploy Pipeline${NC}"
echo -e "${BLUE}========================================${NC}"
echo ""

# Step 1: Build
echo -e "${BLUE}[1/2] Building APK...${NC}"
echo ""

"$SCRIPT_DIR/build.sh"
BUILD_RESULT=$?

if [ $BUILD_RESULT -ne 0 ]; then
    echo ""
    echo -e "${RED}Build failed. Deployment cancelled.${NC}"
    exit 1
fi

echo ""
echo -e "${GREEN}Build completed successfully!${NC}"
echo ""

# Wait a moment
sleep 2

# Step 2: Deploy
echo -e "${BLUE}[2/2] Deploying to Quest 3...${NC}"
echo ""

"$SCRIPT_DIR/deploy.sh"
DEPLOY_RESULT=$?

echo ""
echo -e "${BLUE}========================================${NC}"

if [ $DEPLOY_RESULT -eq 0 ]; then
    echo -e "${GREEN}Pipeline completed successfully!${NC}"
    echo -e "${BLUE}========================================${NC}"
    echo ""
    echo "Your app is now built and running on Quest 3!"
    exit 0
else
    echo -e "${RED}Deployment failed${NC}"
    echo -e "${BLUE}========================================${NC}"
    echo ""
    echo "APK was built successfully but deployment failed."
    echo "You can manually install using SideQuest."
    exit 1
fi
