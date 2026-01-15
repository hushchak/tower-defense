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

    public static T[] GetObjectsOfTypeFromScene<T>(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (!scene.IsValid() && !scene.isLoaded)
        {
            Debug.LogWarning($"You are trying to get objects of type {typeof(T)} from scene {sceneName}, that is not valid or loaded");
            return default;
        }

        GameObject[] rooObjects = scene.GetRootGameObjects();
        List<T> gameObjects = new();
        foreach (GameObject rootObject in rooObjects)
        {
            T[] objects = rootObject.GetComponentsInChildren<T>();
            if (objects.Length > 0)
            {
                gameObjects.AddRange(objects);
            }
        }

        return gameObjects.ToArray();
    }

    public static T GetObjectOfTypeFromScene<T>(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);

        if (!scene.IsValid() && !scene.isLoaded)
        {
            Debug.LogWarning($"You are trying to get object of type {typeof(T)} from scene {sceneName}, that is not valid or loaded");
            return default;
        }

        GameObject[] rooObjects = scene.GetRootGameObjects();
        foreach (GameObject rootObject in rooObjects)
        {
            T[] objects = rootObject.GetComponentsInChildren<T>();
            if (objects.Length > 0)
            {
                return objects[0];
            }
        }

        return default;
    }
}
