using System;
using UnityEngine;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] private EventChannelLevelData levelInitializationEventChannel;

    [SerializeField] private EventChannelInt addMoneyChannel;
    [SerializeField] private EventChannelInt moneyChangedChannel;

    private int money;

    private void OnEnable()
    {
        levelInitializationEventChannel.Subscribe(Setup);
        addMoneyChannel.Subscribe(AddMoney);
    }

    private void OnDisable()
    {
        levelInitializationEventChannel.Unsubscribe(Setup);
        addMoneyChannel.Unsubscribe(AddMoney);
    }

    private void Setup(LevelData data)
    {
        money = data.PlayerStartMoney;
    }

    private void AddMoney(int amount)
    {
        money += amount;
        moneyChangedChannel.Raise(money);
    }
}
