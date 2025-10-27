#!/bin/bash
# Automated Project Setup Script for macOS/Linux
# This script clones the repository and sets up the development environment

set -e  # Exit on any error

echo "========================================="
echo "DecartAI Quest 3 - Project Setup Script"
echo "========================================="
echo ""

# Configuration
PROJECT_DIR="$HOME/UnityProjects"
REPO_URL="https://github.com/Jakubikk/joystick-nav.git"
PROJECT_NAME="joystick-nav"

# Check for required tools
echo "Checking prerequisites..."

# Check for Git
if ! command -v git &> /dev/null; then
    echo "❌ Git is not installed."
    echo "Installing Git..."
    if [[ "$OSTYPE" == "darwin"* ]]; then
        # macOS
        if ! command -v brew &> /dev/null; then
            echo "Installing Homebrew first..."
            /bin/bash -c "$(curl -fsSL https://raw.githubusercontent.com/Homebrew/install/HEAD/install.sh)"
        fi
        brew install git
    else
        # Linux
        sudo apt-get update
        sudo apt-get install -y git
    fi
    echo "✅ Git installed successfully"
else
    echo "✅ Git is already installed"
fi

# Check for ADB
if ! command -v adb &> /dev/null; then
    echo "⚠️  ADB is not installed."
    echo "Installing Android Platform Tools..."
    if [[ "$OSTYPE" == "darwin"* ]]; then
        brew install android-platform-tools
    else
        sudo apt-get install -y android-tools-adb android-tools-fastboot
    fi
    echo "✅ ADB installed successfully"
else
    echo "✅ ADB is already installed"
fi

# Create project directory
echo ""
echo "Creating project directory at $PROJECT_DIR..."
mkdir -p "$PROJECT_DIR"
cd "$PROJECT_DIR"

# Clone or update repository
if [ -d "$PROJECT_NAME" ]; then
    echo "⚠️  Project directory already exists."
    read -p "Do you want to update it? (y/n) " -n 1 -r
    echo ""
    if [[ $REPLY =~ ^[Yy]$ ]]; then
        echo "Updating repository..."
        cd "$PROJECT_NAME"
        git pull origin main
        echo "✅ Repository updated"
    fi
else
    echo "Cloning repository..."
    git clone "$REPO_URL"
    echo "✅ Repository cloned successfully"
fi

# Final instructions
echo ""
echo "========================================="
echo "✅ Setup Complete!"
echo "========================================="
echo ""
echo "Next steps:"
echo "1. Install Unity Hub from: https://unity.com/download"
echo "2. Install Unity 6000.0.34f1 with Android Build Support"
echo "3. Open Unity Hub and add project from:"
echo "   $PROJECT_DIR/$PROJECT_NAME/DecartAI-Quest-Unity"
echo ""
echo "For detailed instructions, see:"
echo "   $PROJECT_DIR/$PROJECT_NAME/Documentation/COMPLETE_BEGINNERS_GUIDE.md"
echo ""
