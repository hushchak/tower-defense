using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelLoader
{
    public static async void LoadLevel(LevelDataSO data)
    {
        try
        {
            await LoadLevelAsync(data);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    public static async Awaitable LoadLevelAsync(LevelDataSO data)
    {
        await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.LevelSession);

        LevelManager manager = null;
        foreach (GameObject @object in SceneManager.GetSceneByName(SceneData.Names.LevelSession).GetRootGameObjects())
            @object.TryGetComponent(out manager);

        if (manager == null)
        {
            Debug.LogError("There is no LevelManager in LevelSession scene");
            return;
        }

        manager.SetData(data);
    }
}
