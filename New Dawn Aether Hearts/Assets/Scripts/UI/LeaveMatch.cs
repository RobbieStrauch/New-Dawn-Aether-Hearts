using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveMatch : MonoBehaviour
{
    public void Press()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
