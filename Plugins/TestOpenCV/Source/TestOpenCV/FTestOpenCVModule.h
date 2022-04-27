#pragma once

class FTestOpenCVModule : public IModuleInterface
{
public:
	FTestOpenCVModule();
	virtual void StartupModule() override;
	virtual void ShutdownModule() override;
};
