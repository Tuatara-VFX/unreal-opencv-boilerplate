#pragma once

class FTestModule : public IModuleInterface
{
public:
	FTestModule();
	virtual void StartupModule() override;
	virtual void ShutdownModule() override;
};
