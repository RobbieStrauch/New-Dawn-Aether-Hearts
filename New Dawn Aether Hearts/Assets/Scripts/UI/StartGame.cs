using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void Press()
    {
        SceneManager.LoadScene("CurrentScene");
    }

    public void tutorial()
    {
        SceneManager.LoadScene("TutorialScene");
    }
}
