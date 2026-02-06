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
        if (windows.Count >= 1)
        {
            windows.Peek().Close();
        }

        windows.Push(window);
        windows.Peek().Open();
    }

    public void Close()
    {
        if (windows.Count > 1)
        {
            windows.Pop().Close();
            windows.Peek().Open();
        }
    }
}
