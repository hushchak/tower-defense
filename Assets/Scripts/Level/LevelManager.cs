using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private EventChannelLevelData levelIntializeEventChannel;
    [SerializeField] private EventChannel playerDeathEventChannel;

    private LevelData levelData;

    public async void SetData(LevelData data)
    {
        levelData = data;

        try
        {
            await SceneLoader.LoadScene(SceneData.Tags.Level, data.LevelSceneName);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        levelIntializeEventChannel.Raise(levelData);
    }

    private void OnEnable()
    {
        playerDeathEventChannel.Subscribe(Defeat);
    }

    private void OnDisable()
    {
        playerDeathEventChannel.Unsubscribe(Defeat);
    }

    private async void Defeat()
    {
        Debug.Log("Player defeated");
        try
        {
            await SceneLoader.UnloadScene(SceneData.Tags.Level);
            await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.MainMenu);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
}
