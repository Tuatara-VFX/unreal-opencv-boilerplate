#include "FTestOpenCVModule.h"


#include <opencv2/core/core.hpp>

FTestOpenCVModule::FTestOpenCVModule()
{
}

void FTestOpenCVModule::StartupModule()
{
	IModuleInterface::StartupModule();
	UE_LOG(LogTemp, Warning, TEXT("Startup..."));
	const std::string version = cv::getVersionString();
	UE_LOG(LogTemp, Warning, TEXT("OpenCV Version is %hs"), version.c_str());
	
}

void FTestOpenCVModule::ShutdownModule()
{
	IModuleInterface::ShutdownModule();
}

IMPLEMENT_MODULE(FTestOpenCVModule, TestOpenCV)