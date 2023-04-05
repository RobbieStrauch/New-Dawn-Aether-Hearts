#include "Wrapper.h"
#include "PlayerSettings.h"

PlayerSettings playerSettings;

PLUGIN_API float GetFOV()
{
	return playerSettings.GetFOV();
}

PLUGIN_API void SetFOV(float newFOV)
{
	return playerSettings.SetFOV(newFOV);
}

PLUGIN_API void SetMusicVolume(float newVolume)
{
	return playerSettings.SetMusicVolume(newVolume);
}

PLUGIN_API float GetMusicVolume()
{
	return playerSettings.GetMusicVolume();
}

PLUGIN_API void SetSoundEffectVolume(float newVolume)
{
	return playerSettings.SetSoundEffectVolume(newVolume);
}

PLUGIN_API float GetSoundEffectVolume()
{
	return playerSettings.GetSoundEffectVolume();
}

PLUGIN_API void SaveToFile(float FOV, float musicVolume, float soundEffectVolume)
{
	return playerSettings.SaveToFile(FOV, musicVolume, soundEffectVolume);
}

PLUGIN_API void StartWriting(const char* fileName)
{
	return playerSettings.StartWriting(fileName);
}

PLUGIN_API void EndWriting()
{
	return playerSettings.EndWriting();
}

PLUGIN_API float ReadFile(int j, const char* fileName)
{
	return playerSettings.ReadFile(j, fileName);
}

PLUGIN_API int GetValue(const char* fileName)
{
	return playerSettings.GetValue(fileName);
}
