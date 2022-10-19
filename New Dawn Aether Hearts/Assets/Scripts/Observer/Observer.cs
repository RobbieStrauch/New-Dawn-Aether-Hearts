using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Wants to know when another object does something interesting 
public abstract class Observer 
{
    public abstract void OnNotify();
}

public class ClickPath : Observer
{
    //The box gameobject which will do something
    GameObject player;
    //What will happen when this box gets an event
    ClickEvents nodeEvent;

    public ClickPath(GameObject player, ClickEvents nodeEvent)
    {
       this.player = player;
       this.nodeEvent = nodeEvent;
    }

    //What the box will do if the event fits it (will always fit but you will probably change that on your own)
    public override void OnNotify()
    {
        playerColorGet(nodeEvent.playerEditorColor());
    }

    //The box will always jump in this case
    void playerColorGet(Color mat)
    {
        //If the box is close to the ground
        player.GetComponent<Renderer>().materials[0].color = mat;
    }
}
