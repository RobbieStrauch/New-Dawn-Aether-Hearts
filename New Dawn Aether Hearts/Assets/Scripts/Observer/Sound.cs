using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioClip sound;
    private AudioSource source;
    public static int temp = 0;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    public void noise()
    {
        source.Play();
    }

    public void stop()
    {
        source.Stop();
    }
}
