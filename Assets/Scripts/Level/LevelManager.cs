using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EventChannelLevelData levelIntializeEventChannel;
    [SerializeField] private EventChannel playerWinEventChannel;
    [SerializeField] private EventChannel playerDeathEventChannel;

    public async void SetData(LevelDataSO data)
    {
        try
        {
            await SceneLoader.LoadScene(SceneData.Tags.LevelUI, SceneData.Names.LevelUI);
            await SceneLoader.LoadScene(SceneData.Tags.Level, data.LevelSceneName);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        levelIntializeEventChannel.Raise(GetLevelData(data));
    }

    private LevelData GetLevelData(LevelDataSO dataSO)
    {
        TowerGrid towerGrid = SceneLoader.GetObjectOfTypeFromScene<TowerGrid>(dataSO.LevelSceneName);;

        return new LevelData(
            waves: dataSO.Waves,
            playerMaxHealth: dataSO.PlayerMaxHealth,
            playerStartMoney: dataSO.PlayerStartMoney,

            towerGrid: towerGrid
        );
    }

    private void OnEnable()
    {
        playerWinEventChannel.Subscribe(Win);
        playerDeathEventChannel.Subscribe(Defeat);
    }

    private void OnDisable()
    {
        playerWinEventChannel.Unsubscribe(Win);
        playerDeathEventChannel.Unsubscribe(Defeat);
    }

    private async void Win()
    {
        Debug.Log("Player win");
        try
        {
            await SceneLoader.UnloadScene(SceneData.Tags.Level);
            await SceneLoader.UnloadScene(SceneData.Tags.LevelUI);
            await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.MainMenu);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private async void Defeat()
    {
        Debug.Log("Player defeated");
        try
        {
            await SceneLoader.UnloadScene(SceneData.Tags.Level);
            await SceneLoader.UnloadScene(SceneData.Tags.LevelUI);
            await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.MainMenu);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
