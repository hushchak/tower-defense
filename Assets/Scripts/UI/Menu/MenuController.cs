using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private Window defaultWindow;

    private Stack<Window> windows = new();

    private void Awake()
    {
        windows.Push(defaultWindow);
        defaultWindow.Open();
    }

    public void Open(Window window)
    {
        Debug.Log(windows.Count);
        if (windows.Count >= 1)
        {
            windows.Peek().Close();
        }

        windows.Push(window);
        windows.Peek().Open();
        Debug.Log($"Opening {window.gameObject.name}");
        Debug.Log(windows.Count);
    }

    public void Close()
    {
        Debug.Log($"Try to close {windows.Peek().gameObject.name}");
        if (windows.Count > 1)
        {
            windows.Pop().Close();
            windows.Peek().Open();
        }
    }
}
