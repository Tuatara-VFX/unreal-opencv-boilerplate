using System;
using System.ComponentModel;
using System.IO;
using UnrealBuildTool;

public class ExternalOpenCv : ModuleRules
{
	public const bool ForceDebug = false; 
	
	private string LibrariesPath
	{ 
		get { return Path.GetFullPath(Path.Combine(ModuleDirectory, "lib")); } 
	}
	
	private string IncludesPath
	{ 
		get { return Path.GetFullPath(Path.Combine(ModuleDirectory, "include")); } 
	}
	
	private string BinariesPath
	{ 
		get { return Path.GetFullPath(Path.Combine(ModuleDirectory, "bin")); } 
	}
	
	private string UnrealTargetBinariesPath
	{ 
		get { return Path.GetFullPath(Path.Combine(ModuleDirectory, "../../../Binaries")); } 
	}
	
	public ExternalOpenCv(ReadOnlyTargetRules Target) : base(Target)
	{
		Console.WriteLine("Adding External OpenCV module");
		Type = ModuleType.External;

		if (Target.Platform == UnrealTargetPlatform.Win64)
		{
			// Add Includes.
			if (!Directory.Exists(IncludesPath))
			{
				throw new Exception("Include directory missing " + IncludesPath);
			}
			PublicSystemIncludePaths.Add(IncludesPath);
			
			var libDir = Path.Combine(ModuleDirectory, "lib");

            var libName = "opencv_world452";
            if (Target.Configuration == UnrealTargetConfiguration.Debug &&
                Target.bDebugBuildsActuallyUseDebugCRT || ForceDebug)
			{
					libName += "d";
			}

			var dynamicLibName = libName + ".dll";
			var staticLibName = libName + ".lib";
			var staticLibPath = Path.Combine(libDir, staticLibName);
			
			// Add static libs (.lib).
			if (!File.Exists(staticLibPath))
			{
				throw new Exception("Missing lib file " + staticLibPath);
			}
			PublicAdditionalLibraries.Add(staticLibPath);
			
			// Copy dynamic libs to the unreal binaries folder.
			var platformDir = Target.Platform.ToString();
			var sourceDllPath = Path.Combine(BinariesPath, dynamicLibName);
			var targetDllPath = Path.Combine(UnrealTargetBinariesPath, platformDir, dynamicLibName);
			
			if (!File.Exists(sourceDllPath))
			{
				throw new Exception("Missing dll at " + sourceDllPath);
			}
			File.Copy(sourceDllPath, targetDllPath, overwrite: true);

			// Do not add in PublicDelayLoadDLLs because the runtime will then expect us to load
			// the dll by hand before calling opencv functions.
			// ⛔ PublicDelayLoadDLLs.Add(dynamicLibName);
			
			// This i'm not sure what it's for but it's working without it.
			// RuntimeDependencies.Add(targetDllPath);
		}
		else // unsupported platform
		{
			throw new Exception("Unsuported platform");
		}
	}
}
