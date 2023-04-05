#pragma once

#ifndef __WRAPPER__
#define __WRAPPER__

#include "PluginSettings.h"

#ifdef __cplusplus
extern "C"
{
#endif // __cplusplus
	PLUGIN_API float GetFOV();
	PLUGIN_API void SetFOV(float newFOV);
	PLUGIN_API void SetMusicVolume(float newVolume = 100.f);
	PLUGIN_API float GetMusicVolume();
	PLUGIN_API void SetSoundEffectVolume(float newVolume = 100.f);
	PLUGIN_API float GetSoundEffectVolume();
	PLUGIN_API void SaveToFile(float FOV, float musicVolume, float soundEffectVolume);
	PLUGIN_API void StartWriting(const char* fileName);
	PLUGIN_API void EndWriting();
	PLUGIN_API float ReadFile(int j, const char* fileName);
	PLUGIN_API int GetValue(const char* fileName);
#ifdef __cplusplus
}
#endif // __cplusplus

#endif // _WRAPPER_