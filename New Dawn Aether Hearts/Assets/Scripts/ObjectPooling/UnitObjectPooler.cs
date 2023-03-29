using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObjectPooler : MonoBehaviour
{
    public static UnitObjectPooler instance;

    public List<Pool> pools;
    public List<GameObject> objectPools;
    public Dictionary<string, List<GameObject>> poolDictionary;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
    }

    void Start()
    {
        poolDictionary = new Dictionary<string, List<GameObject>>();

        foreach (Pool pool in pools)
        {
            objectPools = new List<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(pool.poolParent.transform);
                obj.name = pool.tag + i.ToString();
                //Debug.Log(obj.name.Substring(6, obj.name.Length - 6));
                obj.SetActive(false);
                objectPools.Add(obj);
            }

            poolDictionary.Add(pool.tag, objectPools);
        }
    }

    public GameObject SpawnFromPool(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        for (int i = 0; i < poolDictionary[tag].Count; i++)
        {
            if (!poolDictionary[tag][i].activeSelf)
            {
                GameObject objectToSpawn = poolDictionary[tag][i];
                objectToSpawn.SetActive(true);
                return objectToSpawn;
            }
        }

        return null;
    }
}
