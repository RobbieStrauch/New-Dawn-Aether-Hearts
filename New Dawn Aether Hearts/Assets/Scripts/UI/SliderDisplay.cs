using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderDisplay : MonoBehaviour
{
    public Slider sliderValue;
    public TextMeshProUGUI valueText;
    
    public enum SliderType 
    { 
        fov,
        music,
        soundEffects
    };

    public SliderType type;
    
    bool check = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!check)
        {
            switch (type)
            {
                case SliderType.fov:
                    valueText.text = OptionsManager.instance.FOV.ToString();
                    sliderValue.value = OptionsManager.instance.FOV;
                    break;
                case SliderType.music:
                    valueText.text = OptionsManager.instance.musicVolume.ToString();
                    sliderValue.value = OptionsManager.instance.musicVolume;
                    break;
                case SliderType.soundEffects:
                    valueText.text = OptionsManager.instance.soundEffectVolume.ToString();
                    sliderValue.value = OptionsManager.instance.soundEffectVolume;
                    break;
                default:
                    break;
            }
            check = true;
        }

        float value = Mathf.Round(sliderValue.value * 10f) * 0.1f;
        valueText.text = value.ToString();

        switch (type)
        {
            case SliderType.fov:
                OptionsManager.instance.FOV = value;
                break;
            case SliderType.music:
                OptionsManager.instance.musicVolume = value;
                break;
            case SliderType.soundEffects:
                OptionsManager.instance.soundEffectVolume = value;
                break;
            default:
                break;
        }
    }
}
