using System;
using TMPro;
using UnityEngine;

public class PlayerHealthUI : MonoBehaviour, ILevelInitializable
{
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private EventChannelInt healthChangedChannel;

    private void OnEnable()
    {
        healthChangedChannel.Subscribe(UpdateText);
    }

    private void OnDisable()
    {
        healthChangedChannel.Unsubscribe(UpdateText);
    }

    public void Initialize(LevelData data)
    {
        UpdateText(data.PlayerMaxHealth);
    }

    private void UpdateText(int health)
    {
        healthText.text = health.ToString();
    }
}
