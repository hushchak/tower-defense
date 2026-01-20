using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel TowerCardData", fileName = "EventChannelTowerCardData")]
public class EventChannelTowerCardData : ScriptableObject
{
    private List<Action<TowerCardData>> actions = new();

    public void Subscribe(Action<TowerCardData> action) => actions.Add(action);
    public void Unsubscribe(Action<TowerCardData> action) => actions.Remove(action);

    public void Raise(TowerCardData data)
    {
        foreach (Action<TowerCardData> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
