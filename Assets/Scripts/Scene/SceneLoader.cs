using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    private static Dictionary<string, string> sceneDictionary = new();

    public static async Awaitable LoadScene(string sceneTag, string sceneName, bool setActive = true)
    {
        if (sceneDictionary.ContainsKey(sceneTag))
            await UnloadScene(sceneTag);

        await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        if (setActive)
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));

        sceneDictionary.Add(sceneTag, sceneName);
    }

    public static async Awaitable UnloadScene(string sceneTag)
    {
        if (sceneDictionary.TryGetValue(sceneTag, out string value))
        {
            await SceneManager.UnloadSceneAsync(value);
            sceneDictionary.Remove(sceneTag);
        }
        else
        {
            Debug.LogWarning($"You are trying to unload not loaded scene. Tag: {sceneTag}");
        }
    }
}
