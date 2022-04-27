#include "FTestModule.h"


#include <opencv2/core/core.hpp>

FTestModule::FTestModule()
{
}

void FTestModule::StartupModule()
{
	IModuleInterface::StartupModule();
	UE_LOG(LogTemp, Warning, TEXT("Startup..."));
	const std::string version = cv::getVersionString();
	UE_LOG(LogTemp, Warning, TEXT("OpenCV Version is %hs"), version.c_str());
	
}

void FTestModule::ShutdownModule()
{
	IModuleInterface::ShutdownModule();
}

IMPLEMENT_MODULE(FTestModule, TestModule)