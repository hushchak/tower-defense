using System;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private EventChannelInt healthChangedChannel;
    [SerializeField] private EventChannelLevelData levelInitializeChannel;

    private void OnEnable()
    {
        levelInitializeChannel.Subscribe(Setup);
        healthChangedChannel.Subscribe(UpdateText);
    }

    private void OnDisable()
    {
        levelInitializeChannel.Unsubscribe(Setup);
        healthChangedChannel.Unsubscribe(UpdateText);
    }

    private void Setup(LevelData data)
    {
        UpdateText(data.PlayerMaxHealth);
    }

    private void UpdateText(int health)
    {
        healthText.text = health.ToString();
    }
}
