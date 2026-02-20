using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel TowerPointClickData", fileName = "EventChannelTowerPointClickData")]
public class EventChannelTowerPointClickData : ScriptableObject
{
    private List<Action<TowerPointClickData>> actions = new();

    public void Subscribe(Action<TowerPointClickData> action) => actions.Add(action);
    public void Unsubscribe(Action<TowerPointClickData> action) => actions.Remove(action);

    public void Raise(TowerPointClickData data)
    {
        foreach (Action<TowerPointClickData> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
