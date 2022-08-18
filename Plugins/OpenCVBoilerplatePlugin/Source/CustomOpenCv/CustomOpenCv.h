#pragma once

class FCustomOpenCv : public IModuleInterface
{
public:
	FCustomOpenCv();
	virtual void StartupModule() override;
	virtual void ShutdownModule() override;
private:
	void* OpenCV_World_Handler;
};
