#include "PlayerSettings.h"

PlayerSettings::PlayerSettings()
{
	SetFOV();
	SetMusicVolume();
	SetSoundEffectVolume();
}

void PlayerSettings::SetFOV(float newFOV)
{
	FOV = newFOV;
}

float PlayerSettings::GetFOV()
{
	return FOV;
}

void PlayerSettings::SetMusicVolume(float newVolume)
{
	musicVolume = newVolume;
}

float PlayerSettings::GetMusicVolume()
{
	return musicVolume;
}

void PlayerSettings::SetSoundEffectVolume(float newVolume)
{
	soundEffectVolume = newVolume;
}

float PlayerSettings::GetSoundEffectVolume()
{
	return soundEffectVolume;
}

void PlayerSettings::SaveToFile(float FOV, float musicVolume, float soundEffectVolume)
{
	WriteFile << FOV << "\n";
	WriteFile << musicVolume << "\n";
	WriteFile << soundEffectVolume << "\n";
}

void PlayerSettings::StartWriting(const char* fileName)
{
	WriteFile.open(fileName);
}

void PlayerSettings::EndWriting()
{
	WriteFile.close();
}

float PlayerSettings::ReadFile(int j, const char* fileName)
{
	lines.clear();
	ifstream myFile;
	myFile.open(fileName);

	float value;
	while (myFile >> value)
	{
		lines.push_back(value);
	}
	myFile.close();

	return lines[j];
}

int PlayerSettings::GetValue(const char* fileName)
{
	lines.clear();
	ifstream myFile;
	myFile.open(fileName);

	int value = 0;
	string tempString;
	while (getline(myFile, tempString))
	{
		value++;
	}
	myFile.close();

	return value;
}
