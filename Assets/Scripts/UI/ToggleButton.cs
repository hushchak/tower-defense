using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text text;
    [SerializeField] private ToggleState[] states;

    private int currentStateIndex = 0;

    private void Awake()
    {
        SetState(0);
    }

    public void Toggle()
    {
        states[currentStateIndex].OnClickEvent?.Invoke();

        int nextIndex = currentStateIndex >= states.Length - 1 ? 0 : currentStateIndex + 1;
        SetState(nextIndex);
    }

    public void SetState(int index)
    {
        if (index < 0 || index >= states.Length)
        {
            Debug.LogWarning($"You are trying to set unexisting toggle state: {gameObject.name}, index: {index}");
            return;
        }
        currentStateIndex = index;
        text.text = states[index].ToggleText;
    }
}

[System.Serializable]
public class ToggleState
{
    [field: SerializeField] public string ToggleText { get; private set; }
    [field: SerializeField] public UnityEvent OnClickEvent { get; private set; }
}
