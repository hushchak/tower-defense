using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel Null", fileName = "EventChannel")]
public class EventChannel : ScriptableObject
{
    private List<Action> actions = new();

    public void Subscribe(Action action) => actions.Add(action);
    public void Unsubscribe(Action action) => actions.Remove(action);

    public void Raise()
    {
        foreach (Action actions in actions)
        {
            actions?.Invoke();
        }
    }
}
