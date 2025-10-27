#!/usr/bin/env python3
"""
Unity Build Automation Script for Meta Quest 3 AI Transformation App
This script automates the Unity build process for the Quest app.
"""

import os
import sys
import subprocess
import argparse
import platform
from pathlib import Path

class UnityBuilder:
    def __init__(self, unity_path=None, project_path=None):
        self.unity_path = unity_path or self.find_unity()
        self.project_path = project_path or self.find_project()
        self.build_output = Path(self.project_path) / "Builds"
        
    def find_unity(self):
        """Attempt to find Unity installation"""
        system = platform.system()
        
        if system == "Windows":
            # Common Unity Hub installation paths
            paths = [
                r"C:\Program Files\Unity\Hub\Editor",
                r"C:\Program Files (x86)\Unity\Hub\Editor",
            ]
            for base_path in paths:
                if os.path.exists(base_path):
                    # Find the first Unity version folder
                    for folder in os.listdir(base_path):
                        if folder.startswith("6"):  # Unity 6.x
                            unity_exe = os.path.join(base_path, folder, "Editor", "Unity.exe")
                            if os.path.exists(unity_exe):
                                return unity_exe
                                
        elif system == "Darwin":  # macOS
            paths = [
                "/Applications/Unity/Hub/Editor",
            ]
            for base_path in paths:
                if os.path.exists(base_path):
                    for folder in os.listdir(base_path):
                        if folder.startswith("6"):  # Unity 6.x
                            unity_app = os.path.join(
                                base_path, folder, "Unity.app/Contents/MacOS/Unity"
                            )
                            if os.path.exists(unity_app):
                                return unity_app
                                
        raise FileNotFoundError(
            "Unity installation not found. Please specify path with --unity-path"
        )
    
    def find_project(self):
        """Find the Unity project directory"""
        # Assume script is in project root or Documentation folder
        script_dir = Path(__file__).parent.absolute()
        
        # Check if we're in Documentation folder
        if script_dir.name == "Documentation":
            project_root = script_dir.parent
        else:
            project_root = script_dir
            
        unity_project = project_root / "DecartAI-Quest-Unity"
        
        if not unity_project.exists():
            raise FileNotFoundError(
                f"Unity project not found at {unity_project}. "
                "Please specify with --project-path"
            )
            
        return str(unity_project)
    
    def build(self, output_name="QuestAI.apk", build_and_run=False):
        """Build the Unity project for Android/Quest"""
        print(f"Building Unity project...")
        print(f"Unity: {self.unity_path}")
        print(f"Project: {self.project_path}")
        
        # Create build output directory
        self.build_output.mkdir(exist_ok=True)
        output_file = self.build_output / output_name
        
        # Build command arguments
        cmd = [
            self.unity_path,
            "-quit",
            "-batchmode",
            "-nographics",
            "-projectPath", self.project_path,
            "-buildTarget", "Android",
            "-executeMethod", "BuildCommand.BuildAndroid",
        ]
        
        if build_and_run:
            cmd.append("-buildAndRun")
        
        # Set build output path via environment variable
        env = os.environ.copy()
        env["BUILD_OUTPUT_PATH"] = str(output_file)
        
        print(f"\nExecuting build command...")
        print(f"Output: {output_file}")
        
        try:
            result = subprocess.run(
                cmd,
                env=env,
                capture_output=True,
                text=True,
                check=False
            )
            
            if result.returncode == 0:
                print("\n✅ Build completed successfully!")
                print(f"APK location: {output_file}")
                return True
            else:
                print("\n❌ Build failed!")
                print("\nStdout:")
                print(result.stdout)
                print("\nStderr:")
                print(result.stderr)
                return False
                
        except Exception as e:
            print(f"\n❌ Build error: {e}")
            return False
    
    def create_build_script(self):
        """Create C# build script for Unity"""
        build_script = """using UnityEditor;
using UnityEngine;
using System;

public class BuildCommand
{
    public static void BuildAndroid()
    {
        string outputPath = Environment.GetEnvironmentVariable("BUILD_OUTPUT_PATH");
        if (string.IsNullOrEmpty(outputPath))
        {
            outputPath = "Builds/QuestAI.apk";
        }

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Samples/DecartAI-Quest/DecartAI-Main.unity" };
        buildPlayerOptions.locationPathName = outputPath;
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        Debug.Log($"Building to: {outputPath}");
        BuildPipeline.BuildPlayer(buildPlayerOptions);
    }
}
"""
        
        editor_folder = Path(self.project_path) / "Assets" / "Editor"
        editor_folder.mkdir(exist_ok=True)
        
        script_path = editor_folder / "BuildCommand.cs"
        with open(script_path, 'w') as f:
            f.write(build_script)
            
        print(f"Created build script at: {script_path}")
        return script_path

def main():
    parser = argparse.ArgumentParser(
        description="Build automation for Meta Quest 3 AI Transformation App"
    )
    parser.add_argument(
        "--unity-path",
        help="Path to Unity executable",
        default=None
    )
    parser.add_argument(
        "--project-path",
        help="Path to Unity project folder",
        default=None
    )
    parser.add_argument(
        "--output",
        help="Output APK filename",
        default="QuestAI.apk"
    )
    parser.add_argument(
        "--build-and-run",
        help="Build and install on connected Quest device",
        action="store_true"
    )
    parser.add_argument(
        "--create-script",
        help="Create build script only (don't build)",
        action="store_true"
    )
    
    args = parser.parse_args()
    
    try:
        builder = UnityBuilder(args.unity_path, args.project_path)
        
        # Create build script if needed
        if args.create_script:
            builder.create_build_script()
            print("✅ Build script created successfully")
            return 0
        
        # Ensure build script exists
        builder.create_build_script()
        
        # Run build
        success = builder.build(args.output, args.build_and_run)
        return 0 if success else 1
        
    except Exception as e:
        print(f"❌ Error: {e}")
        return 1

if __name__ == "__main__":
    sys.exit(main())
