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
        viewportX,
        viewportY,
        viewportW,
        viewportH
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
                case SliderType.viewportX:
                    valueText.text = OptionsManager.instance.viewportXY.x.ToString();
                    sliderValue.value = OptionsManager.instance.viewportXY.x;
                    break;
                case SliderType.viewportY:
                    valueText.text = OptionsManager.instance.viewportXY.y.ToString();
                    sliderValue.value = OptionsManager.instance.viewportXY.y;
                    break;
                case SliderType.viewportW:
                    valueText.text = OptionsManager.instance.viewportWH.x.ToString();
                    sliderValue.value = OptionsManager.instance.viewportWH.x;
                    break;
                case SliderType.viewportH:
                    valueText.text = OptionsManager.instance.viewportWH.y.ToString();
                    sliderValue.value = OptionsManager.instance.viewportWH.y;
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
            case SliderType.viewportX:
                OptionsManager.instance.viewportXY.x = value;
                break;
            case SliderType.viewportY:
                OptionsManager.instance.viewportXY.y = value;
                break;
            case SliderType.viewportW:
                OptionsManager.instance.viewportWH.x = value;
                break;
            case SliderType.viewportH:
                OptionsManager.instance.viewportWH.y = value;
                break;
            default:
                break;
        }
    }
}
