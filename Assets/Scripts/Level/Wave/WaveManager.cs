using System;
using System.Threading;
using UnityEngine;

public class WaveManager : MonoBehaviour, ILevelInitializable
{
    [SerializeField] private EventChannel playerDeathEventChannel;
    [SerializeField] private EventChannel playerWinEventChannel;
    [Space]
    [SerializeField] private EventChannel waveInitializeChannel;
    [SerializeField] private EventChannel waveStartedChannel;
    [SerializeField] private EventChannel waveDefeatedChannel;

    [Space]
    [SerializeField] private EnemySpawner spawner;

    private WaveData[] waves;
    private int waveIndex;

    private CancellationToken playerDeathCancellationToken;

    public void Initialize(LevelData data)
    {
        waves = data.Waves;
        waveIndex = 0;
    }

    private void OnEnable()
    {
        waveInitializeChannel.Subscribe(StartWave);
    }

    private void OnDisable()
    {
        waveInitializeChannel.Unsubscribe(StartWave);
    }

    private async void StartWave()
    {
        if (waveIndex >= waves.Length || spawner.WaveInProgress)
            return;

        playerDeathCancellationToken = new CancellationToken();
        try
        {
            waveStartedChannel.Raise();
            await spawner.SpawnWave(waves[waveIndex], playerDeathCancellationToken);
            waveIndex++;

            if (waveIndex >= waves.Length)
            {
                PlayerWin();
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void PlayerWin()
    {
        playerWinEventChannel.Raise();
    }
}
