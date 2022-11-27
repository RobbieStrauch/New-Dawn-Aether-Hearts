using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class PlayerSettingsPlugin : MonoBehaviour
{
    [DllImport("SettingsPlugin")]
    public static extern float GetFOV();

    [DllImport("SettingsPlugin")]
    public static extern void SetFOV(float newFOV);

    [DllImport("SettingsPlugin")]
    public static extern void SetMusicVolume(float newVolume);

    [DllImport("SettingsPlugin")]
    public static extern float GetMusicVolume();

    [DllImport("SettingsPlugin")]
    public static extern void SetSoundEffectVolume(float newVolume);

    [DllImport("SettingsPlugin")]
    public static extern float GetSoundEffectVolume();

    [DllImport("SettingsPlugin")]
    public static extern void SaveToFile(float FOV, float musicVolume, float soundEffectVolume);

    [DllImport("SettingsPlugin")]
    public static extern void StartWriting(string fileName);

    [DllImport("SettingsPlugin")]
    public static extern void StartReading(string fileName);

    [DllImport("SettingsPlugin")]
    public static extern void EndReading();

    [DllImport("SettingsPlugin")]
    public static extern float GetReadingValue(int index);

    [DllImport("SettingsPlugin")]
    public static extern void EndWriting();

    string path;
    string fn;
    Camera mainCamera;

    public OptionsManager options;

    // Start is called before the first frame update
    void Start()
    {
        path = Application.dataPath;
        fn = path + "/PlayerSettings.txt";

        mainCamera = Camera.main;

        StartReading(fn);
        EndReading();

        SetFOV(GetReadingValue(0));
        SetMusicVolume(GetReadingValue(1));
        SetSoundEffectVolume(GetReadingValue(2));

        StartWriting(fn);
        SaveToFile(GetFOV(), GetMusicVolume(), GetSoundEffectVolume());
        EndWriting();

        options.FOV = GetFOV();
        options.musicVolume = GetMusicVolume();
        options.soundEffectVolume = GetSoundEffectVolume();

        mainCamera.fieldOfView = GetFOV();
    }

    // Update is called once per frame
    void Update()
    {
        ApplySettings();
    }

    public void ApplySettings()
    {
        SetFOV(options.FOV);
        SetMusicVolume(options.musicVolume);
        SetSoundEffectVolume(options.soundEffectVolume);

        StartWriting(fn);
        SaveToFile(GetFOV(), GetMusicVolume(), GetSoundEffectVolume());
        EndWriting();

        mainCamera.fieldOfView = GetFOV();
    }
}
