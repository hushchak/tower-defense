using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool
{
    private GameObject prefab;
    private Transform parent;

    private List<GameObject> pool = new();

    public GameObjectPool(GameObject prefab, Transform parent, int startCount)
    {
        this.prefab = prefab;
        this.parent = parent;
        for (int i = 0; i < startCount; i++)
        {
            SpawnGameObject();
        }
    }

    private GameObject SpawnGameObject()
    {
        GameObject @object = Object.Instantiate(prefab, parent);
        @object.SetActive(false);
        pool.Add(@object);
        return @object;
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeSelf)
            {
                return pool[i];
            }
        }
        return SpawnGameObject();
    }
}
