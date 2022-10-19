using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsManager : MonoBehaviour
{
    public static OptionsManager instance;

    // Video
    public float FOV;
    public Vector2 viewportXY;
    public Vector2 viewportWH;

    // Start is called before the first frame update
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
