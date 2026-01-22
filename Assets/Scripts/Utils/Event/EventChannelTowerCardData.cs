using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel ITowerCard", fileName = "EventChannelITowerCard")]
public class EventChannelITowerCard : ScriptableObject
{
    private List<Action<ITowerCard>> actions = new();

    public void Subscribe(Action<ITowerCard> action) => actions.Add(action);
    public void Unsubscribe(Action<ITowerCard> action) => actions.Remove(action);

    public void Raise(ITowerCard data)
    {
        foreach (Action<ITowerCard> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
