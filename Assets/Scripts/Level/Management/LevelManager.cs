using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
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

        InitializeLevel(data);
    }

    private void InitializeLevel(LevelDataSO data)
    {
        List<ILevelInitializable> initializables = new();

        ILevelInitializable[] sessionInitializables =
            SceneLoader.GetObjectsOfTypeFromScene<ILevelInitializable>(SceneData.Names.LevelSession);
        ILevelInitializable[] levelInitializables =
            SceneLoader.GetObjectsOfTypeFromScene<ILevelInitializable>(data.LevelSceneName);
        ILevelInitializable[] UIInitializables =
            SceneLoader.GetObjectsOfTypeFromScene<ILevelInitializable>(SceneData.Names.LevelUI);

        if (sessionInitializables.Length > 0)
            initializables.AddRange(sessionInitializables);
        if (levelInitializables.Length > 0)
            initializables.AddRange(levelInitializables);
        if (UIInitializables.Length > 0)
            initializables.AddRange(UIInitializables);

        foreach (ILevelInitializable initializable in initializables)
        {
            initializable.Initialize(GetLevelData(data));
        }
    }

    private LevelData GetLevelData(LevelDataSO dataSO)
    {
        return new LevelData(
            waves: dataSO.Waves,
            playerMaxHealth: dataSO.PlayerMaxHealth,
            playerStartMoney: dataSO.PlayerStartMoney,
            towers: dataSO.Towers
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
            await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.LevelMenu);
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
            await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.LevelMenu);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
