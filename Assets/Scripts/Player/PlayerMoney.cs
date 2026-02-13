using System;
using UnityEngine;

public class PlayerMoney : Singleton<PlayerMoney>, ILevelInitializable
{
    [SerializeField] private EventChannelInt moneyChangedChannel;
    [SerializeField] private Sound moneyAddSound;
    private int money;

    public void Initialize(LevelData data)
    {
        money = data.PlayerStartMoney;
    }

    public void AddMoney(int amount)
    {
        money += amount;
        moneyChangedChannel.Raise(money);
        Audio.Play(moneyAddSound);
    }

    public bool TryDecreaseMoney(int amount)
    {
        if (money < amount)
            return false;

        money -= amount;
        moneyChangedChannel.Raise(money);
        return true;
    }

    public void DecreaseMoney(int amount)
    {
        money = Mathf.Clamp(money - amount, 0, int.MaxValue);
        moneyChangedChannel.Raise(money);
    }

    public int GetMoney()
    {
        return money;
    }
}
