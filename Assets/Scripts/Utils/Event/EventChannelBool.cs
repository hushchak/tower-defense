using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "EventChannels/EventChannel Bool", fileName = "EventChannel")]
public class EventChannelBool : ScriptableObject
{
    private List<Action<bool>> actions = new();

    public void Subscribe(Action<bool> action) => actions.Add(action);
    public void Unsubscribe(Action<bool> action) => actions.Remove(action);

    public void Raise(bool data)
    {
        foreach (Action<bool> actions in actions)
        {
            actions?.Invoke(data);
        }
    }
}
