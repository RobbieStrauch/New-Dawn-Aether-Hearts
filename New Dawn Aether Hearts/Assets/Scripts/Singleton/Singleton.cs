using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton : MonoBehaviour
{
    //creates a singleton class
    public static Singleton Instance { get; private set; }
 
    //Singlton Information
        int resources = 28;
    void Awake()
    {
        if(Instance = null)
        {
            //
            Instance = this;
            
            //makes sure not to destroy the game object that the script is attached to
            GameObject.DontDestroyOnLoad(gameObject);
            //once instanciated the singleton displays information to the player
            print("you have ");
            print(resources);
            print(" gold.");

        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
