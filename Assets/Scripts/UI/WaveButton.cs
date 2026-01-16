using UnityEngine;
using UnityEngine.UI;

public class WaveButton : MonoBehaviour
{
    [SerializeField] private Button button;

    [SerializeField] private EventChannel waveInitializeChannel;
    [SerializeField] private EventChannel waveStartedChannel;
    [SerializeField] private EventChannel waveDefeatedChannel;

    private void StartWave()
    {
        waveInitializeChannel.Raise();
    }

    private void OnEnable()
    {
        button.onClick.AddListener(StartWave);
        waveStartedChannel.Subscribe(TurnOff);
        waveDefeatedChannel.Subscribe(TurnOn);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(StartWave);
        waveStartedChannel.Unsubscribe(TurnOff);
        waveDefeatedChannel.Unsubscribe(TurnOn);
    }

    private void TurnOn() => button.interactable = true;
    private void TurnOff() => button.interactable = false;
}
