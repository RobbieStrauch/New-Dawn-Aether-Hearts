using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class UnitTypes
{
    public abstract GameObject Spawn();
    public abstract UnitTypes Clone();
}

public class Scout : UnitTypes
{
    private int cost;
    private GameObject clone;

    public Scout(GameObject clone, int cost)
    {
        this.cost = cost;
        this.clone = clone;
    }

    public override GameObject Spawn()
    {
        return clone;
    }

    public override UnitTypes Clone()
    {
        return new Scout(MonoBehaviour.Instantiate(clone), cost);
    }
}

public class Ranged : UnitTypes
{
    private int cost;
    private GameObject clone;

    public Ranged(GameObject clone, int cost)
    {
        this.cost = cost;
        this.clone = clone;
    }

    public override GameObject Spawn()
    {
        return clone;
    }

    public override UnitTypes Clone()
    {
        return new Ranged(MonoBehaviour.Instantiate(clone), cost);
    }
}

public class Melee : UnitTypes
{
    private int cost;
    private GameObject clone;

    public Melee(GameObject clone, int cost)
    {
        this.cost = cost;
        this.clone = clone;
    }

    public override GameObject Spawn()
    {
        return clone;
    }

    public override UnitTypes Clone()
    {
        return new Melee(MonoBehaviour.Instantiate(clone), cost);
    }
}

public class Tank : UnitTypes
{
    private int cost;
    private GameObject clone;

    public Tank(GameObject clone, int cost)
    {
        this.cost = cost;
        this.clone = clone;
    }

    public override GameObject Spawn()
    {
        return clone;
    }

    public override UnitTypes Clone()
    {
        return new Tank(MonoBehaviour.Instantiate(clone), cost);
    }
}