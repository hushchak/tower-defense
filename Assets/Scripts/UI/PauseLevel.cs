using System;
using UnityEngine;

public class PauseLevel : MonoBehaviour
{
    private void OnEnable()
    {
        InputReader.OnPausePerformed += OnPause;
    }

    private void OnDisable()
    {
        InputReader.OnPausePerformed -= OnPause;
    }

    private void OnPause()
    {
        SessionStateManager.Instance.Pause(!SessionStateManager.Instance.IsPaused);
    }
}
