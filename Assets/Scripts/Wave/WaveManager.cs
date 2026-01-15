using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private EventChannelLevelData levelIntializeEventChannel;
    [SerializeField] private EnemySpawner spawner;

    private WaveData[] waveData;
    private int currentWaveIndex;

    private int currentWaveActionIndex;
    private int spawnCount;
    private float waitTime;

    private bool waveEnded;

    private void OnEnable()
    {
        levelIntializeEventChannel.Subscribe(OnLevelInitialization);
    }

    private void OnDisable()
    {
        levelIntializeEventChannel.Unsubscribe(OnLevelInitialization);
    }

    private void OnLevelInitialization(LevelData data)
    {
        SetNewWaveData(data.Waves);
    }

    public void SetNewWaveData(WaveData[] newWaveData)
    {
        waveData = newWaveData;
        currentWaveIndex = 0;

        currentWaveActionIndex = 0;
        spawnCount = 0;
        waitTime = 0;

        waveEnded = false;
    }

    private void Update()
    {
        if (waveEnded)
            return;

        HandleWave(Time.deltaTime);
    }

    private void HandleWave(float delta)
    {
        if (waitTime <= 0)
        {
            SpawnEnemy();
        }
        else
        {
            waitTime -= delta;
        }

        if (currentWaveActionIndex >= waveData[currentWaveIndex].Actions.Length)
        {
            EndWave();
        }
    }

    private void SpawnEnemy()
    {
        spawner.SpawnEnemy(waveData[currentWaveIndex].Actions[currentWaveActionIndex].Enemy);
        spawnCount++;

        if (spawnCount >= waveData[currentWaveIndex].Actions[currentWaveActionIndex].Number)
        {
            spawnCount = 0;
            waitTime = waveData[currentWaveIndex].Actions[currentWaveActionIndex].WaitTimeAfter;

            currentWaveActionIndex++;
        }
        else
        {
            waitTime = waveData[currentWaveIndex].Actions[currentWaveActionIndex].Frequency;
        }
    }

    private void EndWave()
    {
        waveEnded = true;
    }
}
