using System;
using UnityEngine;

public class PlayerMoney : Singleton<PlayerMoney>, ILevelInitializable
{
    [SerializeField] private EventChannelInt moneyChangedChannel;
    private int money;

    public void Initialize(LevelData data)
    {
        money = data.PlayerStartMoney;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyChangedChannel.Raise(money);
    }

    public bool TryDecreaseMoney(int amount)
    {
        if (money < amount)
            return false;

        money -= amount;
        moneyChangedChannel.Raise(money);
        return true;
    }

    public int GetMoney()
    {
        return money;
    }
}
