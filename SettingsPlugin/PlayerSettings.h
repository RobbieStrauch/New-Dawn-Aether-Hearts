#ifndef __PLAYER_SETTINGS__
#define __PLAYER_SETTINGS__

#include "PluginSettings.h"

#include <fstream>
#include <iostream>
#include <string>
#include <vector>

using namespace std;

class PLUGIN_API PlayerSettings
{
public:
	PlayerSettings();

	void SetFOV(float newFOV = 60.f);
	float GetFOV();

	void SetMusicVolume(float newVolume = 100.f);
	float GetMusicVolume();

	void SetSoundEffectVolume(float newVolume = 100.f);
	float GetSoundEffectVolume();

	ofstream WriteFile;
	vector <float> lines;

	void SaveToFile(float FOV, float musicVolume, float soundEffectVolume);

	void StartWriting(const char* fileName);
	void EndWriting();

	float ReadFile(int j, const char* fileName);
	int GetValue(const char* fileName);
	
private:
	float FOV;
	float musicVolume;
	float soundEffectVolume;
};

#endif // __PLAYER_SETTINGS__