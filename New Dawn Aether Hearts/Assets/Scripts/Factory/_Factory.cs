using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class _Factory : MonoBehaviour
{
    public abstract string Name {get;}

    public abstract GameObject Create(GameObject prefab);
}

public class Scout : _Factory
{
    public override string Name => "scout";

    public override GameObject Create(GameObject prefab)
    {
        GameObject unit1 = Instantiate(prefab);
        Debug.Log("Scout Created");
        return unit1;
    }
}

public class Hero : _Factory
{
    public override string Name => "hero";

    public override GameObject Create(GameObject prefab)
    {
        GameObject unit1 = Instantiate(prefab);
        Debug.Log("Hero Created");
        return unit1;
    }
}