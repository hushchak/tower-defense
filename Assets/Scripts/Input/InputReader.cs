using System;
using UnityEngine;
using UnityEngine.InputSystem;

public static class InputReader
{
    private static InputSystem_Actions actions;

    public static event Action<Vector2> OnMouseClick;
    public static Vector2 MousePosition => actions.Player.MousePosition.ReadValue<Vector2>();

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        actions = new InputSystem_Actions();
        actions.Enable();

        actions.Player.MouseClick.performed += MouseClick;
    }

    private static void MouseClick(InputAction.CallbackContext context)
    {
        OnMouseClick?.Invoke(MousePosition);
    }
}
