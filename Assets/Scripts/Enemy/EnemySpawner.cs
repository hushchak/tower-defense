using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Path path;
    [SerializeField] private Transform poolTransform;

    private Dictionary<GameObject, GameObjectPool> pools = new();

    public void SpawnEnemy(EnemyData enemyData)
    {
        Enemy enemy = GetPool(enemyData.Prefab).GetObject().GetComponent<Enemy>();
        enemy.transform.position = spawnPosition.position;
        enemy.Setup(path);
        enemy.gameObject.SetActive(true);
    }

    private GameObjectPool GetPool(GameObject prefab)
    {
        if (pools.TryGetValue(prefab, out GameObjectPool pool))
        {
            return pool;
        }

        GameObjectPool newPool = new GameObjectPool(prefab, poolTransform, 3);
        pools.TryAdd(prefab, newPool);
        return newPool;
    }
}
