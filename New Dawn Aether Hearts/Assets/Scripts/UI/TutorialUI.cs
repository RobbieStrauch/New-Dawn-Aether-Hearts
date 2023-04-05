using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialUI : MonoBehaviour
{
    public Text text;
    public AudioSource audioSource;
    public GameObject button;
    public GameObject del_button;
    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;
    public AudioClip clip4;
    public AudioClip clip5;
    public AudioClip clip6;
    public AudioClip clip7;
    public AudioClip clip8;
    public AudioClip clip9;
    public AudioClip Song;
    public Text txtbox;
    string msg;
    int temp = 0;

    public void ChangeText()
    {
        switch(temp)
        {
            case 0:
            {
                    msg = "Greetings commander, welcome to the tutorial.";
                    text.text = msg;
                    temp++;
                    txtbox.text = "Next";
                    audioSource.clip = clip1;
                    audioSource.Play();
                    break;
            }

            case 1:
            {
                    msg = "Here we will give you a simulation of the battlefield that you will soon be deployed to.";
                    text.text = msg;
                    temp++;
                    audioSource.clip = clip2;
                    audioSource.Play();
                    break;
            }

            case 2:
            {
                    msg = "To begin with, please pan around the world with your mouse by moving your cursor to the edge of the screen. You will notice that most of the world is covered by a fog of war. This will be revealed by the units you deploy.";
                    text.text = msg;
                    temp++;
                    audioSource.clip = clip3;
                    audioSource.Play();
                    break;
            }

            case 3:
            {
                    msg = "If you look towards the top left, this is the minimap. With this tool, you can quickly assess the situation on the battlefield with a cursory view.";
                    text.text = msg;
                    temp++;
                    audioSource.clip = clip4;
                    audioSource.Play();
                    break;
            }

            case 4:
            {
                    msg = "Your current balance is in the top right. This resource is used to recruit new soldiers to the battlefield.";
                    text.text = msg;
                    temp++;
                    audioSource.clip = clip5;
                    audioSource.Play();
                    break;
            }

            case 5:
            {
                    msg = "Now try to recruit a scout by selecting the scout button in the bottom left. This will bring up the information of the unit. Press recruit to continue.";
                    text.text = msg;
                    temp++;
                    audioSource.clip = clip6;
                    audioSource.Play();
                    break;
            }

            case 6:
            {
                    msg = "Now send the scout to the closest control point. It will capture the point which will in turn generate money faster.";
                    text.text = msg;
                    temp++;
                    audioSource.clip = clip7;
                    audioSource.Play();
                    break;
            }

            case 7:
            {
                    msg = "You can tell how many points a side controls by looking at the colour of the diamonds on the top of the screen.";
                    text.text = msg;
                    temp++;
                    break;
            }

            case 8:
            {
                    msg = "The way to win is to either achieve total dominance and capture all the resource points on the map or to have more points controlled by the end of the timer.";
                    text.text = msg;
                    temp++;
                    txtbox.text = "End";
                    break;
            }

            case 9:
            {
                    Destroy(button);
                    Destroy(text);
                    Destroy(del_button);
                    audioSource.clip = Song;
                    audioSource.Play();
                    break;
            }
        }
    }

    public void remove()
    {
        Destroy(button);
        Destroy(text);
        Destroy(del_button);
        audioSource.clip = Song;
        audioSource.Play();
    }

    public void goback()
    {
        SceneManager.LoadScene("MenuScene");
    }
    
}
