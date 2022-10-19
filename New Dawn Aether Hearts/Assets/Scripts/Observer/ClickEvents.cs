using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClickEvents
{
    public abstract Color playerEditorColor();
}

public class YellowMaterial : ClickEvents
{
    public override Color playerEditorColor()
    {
        return Color.yellow;
    }
}

public class GreenMaterial : ClickEvents
{
    public override Color playerEditorColor()
    {
        return Color.green;
    }
}
