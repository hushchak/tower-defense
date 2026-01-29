using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    public enum Slot
    {
        Slot1 = 1,
        Slot2 = 2,
        Slot3 = 3
    }

    private static Dictionary<Slot, GameData> gameDataDictionary;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        gameDataDictionary = new Dictionary<Slot, GameData>
        {
            { Slot.Slot1, GetData(Slot.Slot1) },
            { Slot.Slot2, GetData(Slot.Slot2) },
            { Slot.Slot3, GetData(Slot.Slot3) }
        };

        EventChannel applicationQuitChannel = Resources.Load<EventChannel>("Events/ApplicationQuitChannel");
        applicationQuitChannel.Subscribe(SaveData);
    }

    private static GameData GetData(Slot slot)
    {
        try
        {
            string filePath = GetSlotPath(slot.ToString());
            MakeSureSlotFileExists(filePath);

            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<GameData>(json);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        return null;
    }

    private static void SaveData()
    {
        try
        {
            foreach (KeyValuePair<Slot, GameData> pair in gameDataDictionary)
            {
                string filePath = GetSlotPath(pair.Key.ToString());
                MakeSureSlotFileExists(filePath);

                string json = JsonUtility.ToJson(pair.Value, true);
                File.WriteAllText(filePath, json);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private static void MakeSureSlotFileExists(string slotFilePath)
    {
        string directoryPath = System.IO.Path.GetDirectoryName(slotFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        if (!File.Exists(slotFilePath))
        {
            File.WriteAllText(slotFilePath, JsonUtility.ToJson(new GameData(), true));
        }
    }

    public static GameData GetDataFromSlot(Slot slot)
    {
        if (gameDataDictionary.TryGetValue(slot, out GameData data))
        {
            return data;
        }
        return null;
    }

    private static string GetSlotPath(string slotName)
    {
        return System.IO.Path.Combine(Application.persistentDataPath, "DataSlots", slotName);
    }
}

public class GameData
{
    public bool level1;
    public bool level2;
    public bool level3;
    public bool level4;
    public bool level5;
    public bool level6;
    public bool level7;
    public bool level8;
    public bool level9;
    public bool level10;
}
