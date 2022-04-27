// Fill out your copyright notice in the Description page of Project Settings.

using System;
using UnrealBuildTool;

public class TestOpenCV : ModuleRules
{
	public TestOpenCV(ReadOnlyTargetRules Target) : base(Target)
	{
		Console.WriteLine("Adding TestOpenCV module");

		PCHUsage = PCHUsageMode.UseExplicitOrSharedPCHs;

		PublicDependencyModuleNames.AddRange(new string[] { "Core", "ExternalOpenCv" });

		PrivateDependencyModuleNames.AddRange(
			new string[]
			{
				"CoreUObject",
				"Engine",
				"Slate",
				"SlateCore",
				"Projects",
				"RenderCore",
				"RHI",
				"UMG",

				// ... add private dependencies that you statically link with here ...	
			}
		);

		// Uncomment if you //////if (!Path.sIsare using Slate UI
		// PrivateDependencyModuleNames.AddRange(new string[] { "Slate", "SlateCore" });

		// Uncomment if you are using online features
		// PrivateDependencyModuleNames.Add("OnlineSubsystem");

		// To include OnlineSubsystemSteam, add it to the plugins section in your uproject file with the Enabled attribute set to true
	}
}