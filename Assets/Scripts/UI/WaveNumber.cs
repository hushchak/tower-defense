using TMPro;
using UnityEngine;

public class WaveNumber : MonoBehaviour, ILevelInitializable
{
    [SerializeField] private EventChannel waveStartedChannel;
    [SerializeField] private TMP_Text text;

    private int startedWavesCount;
    private int wavesCount;

    public void Initialize(LevelData data)
    {
        wavesCount = data.Waves.Length;
        startedWavesCount = 0;

        text.text = startedWavesCount + "/" + wavesCount;
    }

    private void OnEnable() => waveStartedChannel.Subscribe(UpdateWaveText);
    private void OnDisable() => waveStartedChannel.Unsubscribe(UpdateWaveText);

    private void UpdateWaveText()
    {
        startedWavesCount++;
        text.text = startedWavesCount + "/" + wavesCount;
    }
}
