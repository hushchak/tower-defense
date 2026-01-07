using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel Int", fileName = "EventChannel")]
public class EventChannelInt : ScriptableObject
{
    private List<Action<int>> actions = new();

    public void Subsribe(Action<int> action) => actions.Add(action);
    public void Unsubscribe(Action<int> action) => actions.Remove(action);

    public void Raise(int data)
    {
        foreach (Action<int> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
