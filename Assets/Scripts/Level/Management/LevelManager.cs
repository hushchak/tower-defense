using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EventChannel playerWinChannel;
    [SerializeField] private EventChannel playerDefeatChannel;
    [SerializeField] private EventChannelBool endLevelChannel;

    private LevelData levelData;

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

        levelData = GetLevelData(data);
        foreach (ILevelInitializable initializable in initializables)
        {
            initializable.Initialize(levelData);
        }
    }

    private LevelData GetLevelData(LevelDataSO dataSO)
    {
        return new LevelData(
            nextUnlockableLevelIndex: dataSO.NextUnlockableLevelIndex,
            waves: dataSO.Waves,
            playerMaxHealth: dataSO.PlayerMaxHealth,
            playerStartMoney: dataSO.PlayerStartMoney,
            towers: dataSO.Towers,
            enemySpawner: GetEnemySpawner(dataSO.LevelSceneName)
        );
    }

    private EnemySpawner GetEnemySpawner(string LevelSceneName)
    {
        EnemySpawner[] possibleSpawners = SceneLoader.GetObjectsOfTypeFromScene<EnemySpawner>(LevelSceneName);
        if (possibleSpawners.Length > 0)
            return possibleSpawners[0];
        return null;
    }

    private void OnEnable() => endLevelChannel.Subscribe(EndLevel);
    private void OnDisable() => endLevelChannel.Unsubscribe(EndLevel);

    private async void EndLevel(bool unlockNextLevel)
    {
        if (unlockNextLevel)
        {
            UnlockNextLevel();
            Debug.Log("Level unlocked");
        }

        try
        {
            await SceneLoader.UnloadScene(SceneData.Tags.Level);
            await SceneLoader.UnloadScene(SceneData.Tags.LevelUI);
            SessionStateManager.Instance.Pause(false);
            await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.LevelMenu);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private void UnlockNextLevel()
    {
        GameData data = SaveManager.GetDataFromCurrentSlot();
        data.isLevelAvailable[levelData.NextUnlockableLevelIndex - 1] = true;
    }
}
