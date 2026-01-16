using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private EventChannel WaveDefeatedChannel;
    [SerializeField] private EventChannel WaveCancelledChannel;

    [Header("Components")]
    [SerializeField] private Transform spawnPosition;
    [SerializeField] private Path path;
    [SerializeField] private Transform poolTransform;

    private Dictionary<GameObject, GameObjectPool> pools = new();

    bool waveInProgress = false;
    public bool WaveInProgress => waveInProgress;
    int enemyCounter;

    public async Awaitable SpawnWave(WaveData waveData, CancellationToken cancellationToken)
    {
        if (waveInProgress)
        {
            Debug.LogWarning("You are trying to spawn wave while another is in progress");
            return;
        }

        var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(
            cancellationToken,
            destroyCancellationToken
        );

        List<Enemy> spawnedEnemies = new();

        try
        {
            waveInProgress = true;
            enemyCounter = 0;

            for (int i = 0; i < waveData.Actions.Length; i++)
            {
                for (int j = 0; j < waveData.Actions[i].Number; j++)
                {
                    Enemy enemy = SpawnEnemy(waveData.Actions[i].Enemy);
                    enemy.Deactivated += OnEnemyDeactivated;
                    spawnedEnemies.Add(enemy);
                    enemyCounter++;

                    await Awaitable.WaitForSecondsAsync(waveData.Actions[i].Frequency, linkedCts.Token);
                }
                await Awaitable.WaitForSecondsAsync(waveData.Actions[i].WaitTimeAfter, linkedCts.Token);
            }

            while (enemyCounter > 0)
            {
                await Awaitable.NextFrameAsync(linkedCts.Token);
            }
            WaveDefeatedChannel.Raise();
        }
        catch (OperationCanceledException)
        {
            WaveCancelledChannel.Raise();
        }
        finally
        {
            foreach (Enemy enemy in spawnedEnemies)
            {
                enemy.Deactivated -= OnEnemyDeactivated;
            }

            linkedCts.Dispose();
            waveInProgress = false;
        }
    }

    private void OnEnemyDeactivated() => enemyCounter--;

    private Enemy SpawnEnemy(EnemyData enemyData)
    {
        Enemy enemy = GetPool(enemyData.Prefab).GetObject().GetComponent<Enemy>();
        enemy.transform.position = spawnPosition.position;
        enemy.Setup(path);
        enemy.gameObject.SetActive(true);

        return enemy;
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
