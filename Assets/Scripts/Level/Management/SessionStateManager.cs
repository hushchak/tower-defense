using UnityEngine;
using System;
using System.Threading;

public class SessionStateManager : Singleton<SessionStateManager>, ILevelInitializable
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

    [Header("Pause Event")]
    [SerializeField] private EventChannel pauseChannel;

    public SessionState State { get; private set; } = SessionState.Idle;
    public bool IsPaused { get; private set; } = false;

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

    public void Pause(bool pause)
    {
        if (State == SessionState.End)
            return;

        IsPaused = pause;
        if (pause)
        {
            Time.timeScale = 0;
            pauseChannel.Raise();
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    private void ChangeStateToWave()
    {
        if (State == SessionState.End)
            return;

        EnterWave();
        waveStartedChannel.Raise();
        State = SessionState.Wave;
    }

    private void ChangeStateToIdle()
    {
        if (State == SessionState.End)
            return;

        ExitWave();
        State = SessionState.Idle;
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
        if (State == SessionState.End)
            return;

        Pause(true);
        State = SessionState.End;
        playerWinEventChannel.Raise();
    }

    private void Lose()
    {
        if (State == SessionState.End)
            return;

        Pause(true);
        State = SessionState.End;
        playerDefeatEventChannel.Raise();
    }
}

public enum SessionState
{
    Idle,
    Wave,
    End
}
