using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound instance;

    private AudioSource source;
    PlayerActions InputAction;

    private void OnEnable()
    {
        InputAction.Sound.Enable();
    }

    private void OnDisable()
    {
        InputAction.Sound.Disable();
    }

    // Start is called before the first frame update 
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        InputAction = new PlayerActions();

        //InputAction.Sound.click.performed += cntxt => noise();
        //InputAction.Sound.send.performed += cntxt => stop();
    }

    //void noise()
    //{
    //    source.Play();
    //}

    //void stop()
    //{
    //    source.Stop();
    //}
}
