using UnityEngine;
using System;
using System.Threading;

public class SessionStateManager : MonoBehaviour, ILevelInitializable
{
    [SerializeField] private WaveManager waveManager;

    [Header("Wave Events")]
    [SerializeField] private EventChannel waveInitializeChannel;
    [SerializeField] private EventChannel waveStartedChannel;
    [SerializeField] private EventChannel waveDefeatedChannel;
    [SerializeField] private EventChannel playerDeathEventChannel;

    [Header("End Events")]
    [SerializeField] private EventChannel playerWinEventChannel;
    [SerializeField] private EventChannel playerDefeatEventChannel;

    private SessionState state = SessionState.Idle;

    private WaveData[] waves;
    private int currentWaveIndex = 0;
    private CancellationTokenSource playerDeathCts;
    private CancellationToken playerDeathCancellationToken;

    public void Initialize(LevelData data)
    {
        waves = data.Waves;
    }

    private void OnEnable()
    {
        waveInitializeChannel.Subscribe(ChangeStateToWave);
        playerDeathEventChannel.Subscribe(OnPlayerDeath);
        waveManager.WaveDefeated += ChangeStateToIdle;
        waveManager.WaveCancelled += Lose;
    }

    private void OnDisable()
    {
        waveInitializeChannel.Unsubscribe(ChangeStateToWave);
        playerDeathEventChannel.Unsubscribe(OnPlayerDeath);
        waveManager.WaveDefeated -= ChangeStateToIdle;
        waveManager.WaveCancelled -= Lose;
    }

    private void ChangeStateToWave()
    {
        if (state == SessionState.End)
            return;

        EnterWave();
        waveStartedChannel.Raise();
        state = SessionState.Wave;
    }

    private void ChangeStateToIdle()
    {
        if (state == SessionState.End)
            return;

        ExitWave();
        state = SessionState.Idle;
    }

    private void EnterWave()
    {
        playerDeathCts = new CancellationTokenSource();
        playerDeathCancellationToken = playerDeathCts.Token;
        waveManager.StartWave(waves[currentWaveIndex], playerDeathCancellationToken);
        waveStartedChannel.Raise();
    }

    private void ExitWave()
    {
        waveDefeatedChannel.Raise();
        currentWaveIndex++;
        if (currentWaveIndex >= waves.Length)
        {
            Win();
        }
    }

    private void OnPlayerDeath() => playerDeathCts?.Cancel();

    private void Win()
    {
        if (state == SessionState.End)
            return;

        state = SessionState.End;
        playerWinEventChannel.Raise();
    }

    private void Lose()
    {
        if (state == SessionState.End)
            return;

        state = SessionState.End;
        playerDefeatEventChannel.Raise();
    }
}

public enum SessionState
{
    Idle,
    Wave,
    End
}
