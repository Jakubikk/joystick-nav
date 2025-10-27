# Documentation

This folder contains comprehensive documentation for the Meta Quest 3 AI Transformation App.

## Files

### [COMPLETE_BEGINNER_GUIDE.md](COMPLETE_BEGINNER_GUIDE.md)
**Complete step-by-step guide for absolute beginners**
- From cloning repository to running on Quest
- No prior Unity experience required
- Detailed screenshots and instructions for every step
- Troubleshooting section
- Tips for best results

**Target Audience**: First-time Unity users, Quest developers new to AI integration

### [TECHNICAL_IMPLEMENTATION.md](TECHNICAL_IMPLEMENTATION.md)
**Technical deep-dive for developers**
- Architecture overview
- Implementation details for each feature
- Code structure and organization
- API reference
- Extension guidelines

**Target Audience**: Developers, contributors, technical users

### [build_automation.py](build_automation.py)
**Python script for automated Unity builds**
- Automatically finds Unity installation
- Builds APK for Quest 3
- Optional build-and-run to device
- Cross-platform (Windows, macOS, Linux)

**Usage**:
```bash
# Basic build
python build_automation.py

# Build and install on Quest
python build_automation.py --build-and-run

# Custom output name
python build_automation.py --output MyQuestApp.apk

# Specify Unity path
python build_automation.py --unity-path "/path/to/Unity"
```

## Quick Start

1. **New to Unity?** Start with [COMPLETE_BEGINNER_GUIDE.md](COMPLETE_BEGINNER_GUIDE.md)
2. **Want to understand the code?** Read [TECHNICAL_IMPLEMENTATION.md](TECHNICAL_IMPLEMENTATION.md)
3. **Need to automate builds?** Use [build_automation.py](build_automation.py)

## Features Documented

✅ **Time Travel** - Transform environment to different historical periods or future scenarios  
✅ **Virtual Mirror** - Try on different clothing styles using AI  
✅ **Biome Transform** - View room as different geographical locations  
✅ **Video Game Style** - Transform environment to match popular video games  
✅ **Custom Prompts** - Create your own transformations with text input  

## Navigation Controls

All features use consistent navigation:
- **Hamburger Button** (Start/Menu): Show/hide menu
- **Joystick Up/Down**: Navigate options
- **Right Trigger**: Confirm selection
- **Left Trigger**: Go back
- **Joystick Left/Right**: Adjust sliders (Time Travel)

## Additional Resources

- [Main Repository README](../README.md)
- [Decart AI Platform](https://platform.decart.ai)
- [Decart Documentation](https://docs.platform.decart.ai)
- [Discord Community](https://discord.gg/decart)

## Contributing

To improve documentation:
1. Fork the repository
2. Update relevant documentation files
3. Test all instructions on fresh Unity install
4. Submit pull request with detailed description

## Support

- **Technical Issues**: Submit GitHub issue with error logs
- **General Questions**: Ask in Discord community
- **Direct Support**: tom@decart.ai

---

*Documentation maintained by the Decart AI community*  
*Last updated: October 2025*
