using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour, ILevelInitializable
{
    public event Action WaveDefeated, WaveCancelled;

    private EnemySpawner spawner;

    bool waveInProgress = false;
    int enemyCounter;

    public void Initialize(LevelData data)
    {
        spawner = data.EnemySpawner;
    }

    public void StartWave(WaveData waveData, CancellationToken playerDeathCancellationToken)
    {
        SpawnWave(waveData, playerDeathCancellationToken);
    }

    public async void SpawnWave(WaveData waveData, CancellationToken cancellationToken)
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
                    Enemy enemy = spawner.SpawnEnemy(waveData.Actions[i].Enemy);
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
            WaveDefeated?.Invoke();
        }
        catch (OperationCanceledException)
        {
            WaveCancelled?.Invoke();
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

    private void OnEnemyDeactivated(Enemy enemy)
    {
        enemy.Deactivated -= OnEnemyDeactivated;
        enemyCounter--;
    }
}
