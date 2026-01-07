using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel LevelData", fileName = "EventChannelLevelData")]
public class EventChannelLevelData : ScriptableObject
{
    private List<Action<LevelData>> actions = new();

    public void Subsribe(Action<LevelData> action) => actions.Add(action);
    public void Unsubscribe(Action<LevelData> action) => actions.Remove(action);

    public void Raise(LevelData data)
    {
        foreach (Action<LevelData> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
