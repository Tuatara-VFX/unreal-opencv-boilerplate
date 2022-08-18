#include "CustomOpenCv.h"

#include "Interfaces/IPluginManager.h"
#include "Modules/ModuleManager.h"

#include <opencv2/core/core.hpp>

FCustomOpenCv::FCustomOpenCv()
{
}

void FCustomOpenCv::StartupModule()
{
	IModuleInterface::StartupModule();
	UE_LOG(LogTemp, Warning, TEXT("Startup opencv world library..."));

	const FString PluginDir = IPluginManager::Get().FindPlugin(TEXT("OpenCVBoilerplatePlugin"))->GetBaseDir();
	FString LibraryPath;
	
#if WITH_OPENCV
	FString Version = FString::FromInt(OPENCV_VERSION);
	LibraryPath = FString::Format(TEXT("ThirdParty/CustomOpenCV{0}/bin/"), { Version });
	LibraryPath = FPaths::Combine(*PluginDir, LibraryPath);
	FString LibFileName = FString::Format(TEXT("opencv_world{0}.dll"), { Version });
	UE_LOG(LogTemp, Warning, TEXT("opencv world LibraryPath == %s"), *(LibraryPath + LibFileName));
	OpenCV_World_Handler = FPlatformProcess::GetDllHandle(*(LibraryPath + LibFileName));
	if (!OpenCV_World_Handler)
	{
		UE_LOG(LogTemp, Error, TEXT("Load OpenCV dll failed!"));
	}
#else
	UE_LOG(LogTemp, Error, TEXT("OpenCV not linked"));
#endif
	
	const std::string version = cv::getVersionString();
	UE_LOG(LogTemp, Warning, TEXT("OpenCV Version is %hs"), version.c_str());
}

void FCustomOpenCv::ShutdownModule()
{
	IModuleInterface::ShutdownModule();
	
#if WITH_OPENCV
	if (OpenCV_World_Handler)
	{
		FPlatformProcess::FreeDllHandle(OpenCV_World_Handler);
		OpenCV_World_Handler = nullptr;
	}
#endif
}

IMPLEMENT_MODULE(FCustomOpenCv, CustomOpenCv)