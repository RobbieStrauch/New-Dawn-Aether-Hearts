using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;

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
    public static extern float ReadFile(int j, string fileName);

    [DllImport("SettingsPlugin")]
    public static extern int GetValue(string fileName);

    [DllImport("SettingsPlugin")]
    public static extern void EndWriting();

    string path;
    string fn;
    Camera mainCamera;

    List<float> values = new List<float>(3);

    public OptionsManager options;

    // Start is called before the first frame update
    void Start()
    {
        

        path = Application.dataPath;
        fn = path + "/PlayerSettings.txt";

        mainCamera = Camera.main;

        Read(fn);

        SetFOV(values[0]);
        SetMusicVolume(values[1]);
        SetSoundEffectVolume(values[2]);

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

    void Read(string path)
    {
        StreamReader streamReader = new StreamReader(path);

        while (!streamReader.EndOfStream)
        {
            string line = streamReader.ReadLine();
            values.Add(float.Parse(line));
        }

        streamReader.Close();
    }
}
