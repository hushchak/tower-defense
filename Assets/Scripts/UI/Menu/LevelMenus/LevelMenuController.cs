using UnityEngine;

public class LevelMenuController : MonoBehaviour
{
    [Header("General")]
    [SerializeField] private MenuController menuController;
    [SerializeField] private Window emptyWindow;
    [SerializeField] private TowerPlacementWindow towerPlacementWindow;
    [SerializeField] private TowerPointWindow towerPointWindow;
    [SerializeField] private Window pauseMenuWindow;
    [SerializeField] private Window settingsMenuWindow;
    [SerializeField] private Window winWindow;
    [SerializeField] private Window loseWindow;

    [Header("Events")]
    [SerializeField] private EventChannel playerWinEventChannel;
    [SerializeField] private EventChannel playerDefeatEventChannel;
    [SerializeField] private EventChannelBool endLevelChannel;
    [SerializeField] private EventChannelTowerPointClickData onTowerPointClickChannel;

    private bool IsEndGameWindowOpened = false;

    private void OnEnable()
    {
        InputReader.OnPausePerformed += OnPause;

        playerWinEventChannel.Subscribe(OpenWinWindow);
        playerDefeatEventChannel.Subscribe(OpenLoseWindow);

        onTowerPointClickChannel.Subscribe(OpenTowerPointWindow);
    }

    private void OnDisable()
    {
        InputReader.OnPausePerformed -= OnPause;

        playerWinEventChannel.Unsubscribe(OpenWinWindow);
        playerDefeatEventChannel.Unsubscribe(OpenLoseWindow);

        onTowerPointClickChannel.Unsubscribe(OpenTowerPointWindow);
    }

    private void OnPause()
    {
        if (SessionStateManager.Instance.State == SessionState.End)
            return;

        if (menuController.GetCurrentWindow() == emptyWindow)
        {
            menuController.Open(pauseMenuWindow);
            SessionStateManager.Instance.Pause(true);
        }
        else
        {
            menuController.ForceOpen(emptyWindow);
            if (SessionStateManager.Instance.IsPaused)
                SessionStateManager.Instance.Pause(false);
        }
    }

    public void Resume()
    {
        if (!SessionStateManager.Instance.IsPaused)
            return;

        SessionStateManager.Instance.Pause(false);
        menuController.ForceOpen(emptyWindow);
    }

    public void QuitLevel(bool unlockNextLevel)
    {
        Debug.Log("QuitLevel()");
        endLevelChannel.Raise(unlockNextLevel);
    }

    public void OpenTowerPointWindow(TowerPointClickData data)
    {
        if (SessionStateManager.Instance.IsPaused)
            return;

        if (data.Tower == null)
        {
            menuController.UnsafePush(towerPlacementWindow);
            towerPlacementWindow.Open(data);
        }
        else
        {
            menuController.UnsafePush(towerPointWindow);
            towerPointWindow.Open(data);
        }
    }

#region Win-Lose Windows
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
#endregion
}
