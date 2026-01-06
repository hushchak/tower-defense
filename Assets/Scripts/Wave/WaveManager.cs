using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private WaveData waveData;
    [SerializeField] private EnemySpawner spawner;

    private int currentIndex;
    private int spawnCount;
    private float waitTime;

    private bool waveEnded;

    public void SetNewWave(WaveData newWaveData)
    {
        waveData = newWaveData;
        currentIndex = 0;
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

        if (currentIndex >= waveData.Actions.Length)
        {
            EndWave();
        }
    }

    private void SpawnEnemy()
    {
        spawner.SpawnEnemy(waveData.Actions[currentIndex].Enemy);
        spawnCount++;

        if (spawnCount >= waveData.Actions[currentIndex].Number)
        {
            spawnCount = 0;
            waitTime = waveData.Actions[currentIndex].WaitTimeAfter;

            currentIndex++;
        }
        else
        {
            waitTime = waveData.Actions[currentIndex].Frequency;
        }
    }

    private void EndWave()
    {
        waveEnded = true;
    }
}
