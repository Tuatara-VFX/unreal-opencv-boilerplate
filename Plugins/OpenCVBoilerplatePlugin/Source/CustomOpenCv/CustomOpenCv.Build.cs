using System;
using System.ComponentModel;
using System.IO;
using UnrealBuildTool;

public class CustomOpenCv : ModuleRules
{
	public const string OpenCvVersion = "420";
	public const bool ForceDebug = false;

	private string OpenCVPath
	{
		get { return Path.GetFullPath(Path.Combine(ModuleDirectory, $"../../ThirdParty/CustomOpenCV{OpenCvVersion}"));  }
	}
	
	private string LibrariesPath
	{ 
		get { return Path.GetFullPath(Path.Combine(OpenCVPath, "lib")); } 
	}
	
	private string IncludesPath
	{ 
		get { return Path.GetFullPath(Path.Combine(OpenCVPath, "include")); } 
	}
	
	private string BinariesPath
	{ 
		get { return Path.GetFullPath(Path.Combine(OpenCVPath, "bin")); } 
	}
	
	public CustomOpenCv(ReadOnlyTargetRules Target) : base(Target)
	{
		Console.WriteLine("Adding External OpenCV module");

		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			// Add Includes.
			if (!Directory.Exists(IncludesPath))
			{
				throw new Exception("Include directory missing " + IncludesPath);
			}
			PublicIncludePaths.Add(IncludesPath);
			
            var libName = "opencv_world" + OpenCvVersion;
            if (ForceDebug)
			{
				libName += "d";
				Console.WriteLine("Debug open cv");
			}

			var dynamicLibName = libName + ".dll";
			var staticLibName = libName + ".lib";
			var staticLibPath = Path.Combine(LibrariesPath, staticLibName);
			var sharedLibPath = Path.Combine(BinariesPath, dynamicLibName);
			
			// Add static libs (.lib).
			if (!File.Exists(staticLibPath))
			{
				throw new Exception("Missing lib file " + staticLibPath);
			}
			PublicAdditionalLibraries.Add(staticLibPath);
			
			// Add dynamic libs (.dll)
			if (!File.Exists(sharedLibPath))
			{
				throw new Exception("Missing dll file " + sharedLibPath);
			}

			PublicDelayLoadDLLs.Add(dynamicLibName);
			RuntimeDependencies.Add(sharedLibPath); // for package
			
			PublicDefinitions.Add("WITH_OPENCV=1");
			PublicDefinitions.Add($"OPENCV_VERSION={OpenCvVersion}");
		}
		else // unsupported platform
		{
			throw new Exception("Unsuported platform");
		}
		
		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				"CoreUObject",
				"Engine",
				"Slate",
				"SlateCore",
				// ... add private dependencies that you statically link with here ...	
			}
		);
		
		PublicDependencyModuleNames.AddRange(
			new string[]
			{
				"Core",
				"Projects",
				"RHI",
				"RenderCore"
				// ... add other public dependencies that you statically link with here ...
			}
		);
	}
}
