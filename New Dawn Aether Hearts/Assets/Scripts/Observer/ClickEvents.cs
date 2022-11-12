using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClickEvents
{
    public abstract Color playerEditorColor();
}

public class CyanMaterial : ClickEvents
{
    public override Color playerEditorColor()
    {
        return Color.cyan;
    }
}

public class BlueMaterial: ClickEvents
{
    public override Color playerEditorColor()
    {
        return Color.blue;
    }
}

public class GreenMaterial : ClickEvents
{
    public override Color playerEditorColor()
    {
        return Color.green;
    }
}

public class YellowMaterial : ClickEvents
{
    public override Color playerEditorColor()
    {
        return Color.yellow;
    }
}
