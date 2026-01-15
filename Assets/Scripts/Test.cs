using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private EventChannel waveStartChannel;
    [SerializeField] private EventChannel waveStartedChannel;
    [SerializeField] private EventChannel waveDefeatedChannel;

    private bool turnedOff = false;

    private void OnEnable()
    {
        waveStartedChannel.Subscribe(TurnOff);
        waveDefeatedChannel.Subscribe(TurnOn);
    }

    private void OnDisable()
    {
        waveStartedChannel.Unsubscribe(TurnOff);
        waveDefeatedChannel.Unsubscribe(TurnOn);
    }

    private void TurnOn()
    {
        turnedOff = false;
    }

    private void TurnOff()
    {
        turnedOff = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            waveStartChannel.Raise();
        }
    }
}
