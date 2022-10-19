using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class CameraSettingsPlugin : MonoBehaviour
{
    public static CameraSettingsPlugin instance;

    [DllImport("SettingsPlugin")]
    public static extern float GetFOV();

    [DllImport("SettingsPlugin")]
    public static extern void SetFOV(float newFOV);

    [DllImport("SettingsPlugin")]
    public static extern Vector2 GetViewportXY();

    [DllImport("SettingsPlugin")]
    public static extern Vector2 GetViewportWH();

    [DllImport("SettingsPlugin")]
    public static extern void SetViewport(float x, float y, float w, float h);

    [DllImport("SettingsPlugin")]
    public static extern void SaveToFile(float FOV, float x, float y, float w, float h);

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

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }

        path = Application.dataPath;
        fn = path + "/CameraSettings.txt";
        
        mainCamera = Camera.main;

        StartReading(fn);
        EndReading();

        SetFOV(GetReadingValue(0));
        SetViewport(GetReadingValue(1), GetReadingValue(2), GetReadingValue(3), GetReadingValue(4));
        
        StartWriting(fn);
        SaveToFile(GetFOV(), GetViewportXY().x, GetViewportXY().y, GetViewportWH().x, GetViewportWH().y);
        EndWriting();

        OptionsManager.instance.FOV = GetFOV();
        OptionsManager.instance.viewportXY.x = GetViewportXY().x;
        OptionsManager.instance.viewportXY.y = GetViewportXY().y;
        OptionsManager.instance.viewportWH.x = GetViewportWH().x;
        OptionsManager.instance.viewportWH.y = GetViewportWH().y;

        mainCamera.fieldOfView = GetFOV();
        mainCamera.rect = new Rect(GetViewportXY().x, GetViewportXY().y, GetViewportWH().x, GetViewportWH().y);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplySettings()
    {
        SetFOV(OptionsManager.instance.FOV);
        SetViewport(OptionsManager.instance.viewportXY.x, OptionsManager.instance.viewportXY.y, OptionsManager.instance.viewportWH.x, OptionsManager.instance.viewportWH.y);
        
        StartWriting(fn);
        SaveToFile(GetFOV(), GetViewportXY().x, GetViewportXY().y, GetViewportWH().x, GetViewportWH().y);
        EndWriting();

        mainCamera.fieldOfView = GetFOV();
        mainCamera.rect = new Rect(GetViewportXY().x, GetViewportXY().y, GetViewportWH().x, GetViewportWH().y);
    }
}
