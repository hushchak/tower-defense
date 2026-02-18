using UnityEngine;

public class LevelMenuController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private MenuController menuController;
    [SerializeField] private Window pauseMenuWindow;
    [SerializeField] private Window winWindow;
    [SerializeField] private Window loseWindow;

    [Header("Events")]
    [SerializeField] private EventChannel playerWinEventChannel;
    [SerializeField] private EventChannel playerDefeatEventChannel;
    [SerializeField] private EventChannelBool endLevelChannel;

    private bool IsEndGameWindowOpened = false;

    private void OnEnable()
    {
        InputReader.OnPausePerformed += TogglePauseMenu;
        playerWinEventChannel.Subscribe(OpenWinWindow);
        playerDefeatEventChannel.Subscribe(OpenLoseWindow);
    }

    private void OnDisable()
    {
        InputReader.OnPausePerformed -= TogglePauseMenu;
        playerWinEventChannel.Unsubscribe(OpenWinWindow);
        playerDefeatEventChannel.Unsubscribe(OpenLoseWindow);
    }

    private void TogglePauseMenu()
    {
        if (SessionStateManager.Instance.State == SessionState.End)
            return;

        if (SessionStateManager.Instance.IsPaused)
            menuController.Open(pauseMenuWindow);
        else
            menuController.Close();
    }

    private void OpenWinWindow()
    {
        if (IsEndGameWindowOpened)
            return;

        menuController.ForceOpen(winWindow);
        IsEndGameWindowOpened = true;
    }

    private void OpenLoseWindow()
    {
        if (IsEndGameWindowOpened)
            return;

        menuController.ForceOpen(loseWindow);
        IsEndGameWindowOpened = true;
    }

    public void QuitLevel(bool unlockNextLevel)
    {
        Debug.Log("QuitLevel()");
        endLevelChannel.Raise(unlockNextLevel);
    }

    public void Resume()
    {
        if (!SessionStateManager.Instance.IsPaused)
            return;

        SessionStateManager.Instance.Pause(false);
        menuController.Close();
    }
}
