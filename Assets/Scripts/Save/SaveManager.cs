using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    public enum Slot
    {
        Empty = 0,
        Slot1 = 1,
        Slot2 = 2,
        Slot3 = 3
    }
    private static int levelAmount = 10;
    private static int defaultAvailableLevels = 1;

    private static Dictionary<Slot, GameData> gameDataDictionary;
    private static Slot currentSlot = Slot.Empty;

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
            MakeSureSlotFileInCorrectFormat(filePath);

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
            File.WriteAllText(slotFilePath, JsonUtility.ToJson(
                new GameData(
                    levelAmount,
                    defaultAvailableLevels
                ),
                true
            ));
        }
    }

    private static void MakeSureSlotFileInCorrectFormat(string slotFilePath)
    {
        string json = File.ReadAllText(slotFilePath);
        try
        {
            GameData data = JsonUtility.FromJson<GameData>(json);

            int availableLevels = 0;
            for (int i = 0; i < data.isLevelAvailable.Length; i++)
            {
                if (data.isLevelAvailable[i])
                    availableLevels++;
            }

            if (data.isLevelAvailable.Length != levelAmount || availableLevels < defaultAvailableLevels)
            {
                File.WriteAllText(slotFilePath, JsonUtility.ToJson(
                    new GameData(
                        levelAmount,
                        defaultAvailableLevels
                    ),
                    true
                ));
            }
        }
        catch
        {
            File.WriteAllText(slotFilePath, JsonUtility.ToJson(
                new GameData(
                    levelAmount,
                    defaultAvailableLevels
                ),
                true
            ));
        }
    }

    private static string GetSlotPath(string slotName)
    {
        return System.IO.Path.Combine(Application.persistentDataPath, "DataSlots", slotName);
    }

    public static GameData GetDataFromCurrentSlot()
    {
        if (gameDataDictionary.TryGetValue(currentSlot, out GameData data))
        {
            return data;
        }
        return null;
    }

    public static void SetCurrentSlot(Slot slot) => currentSlot = slot;
    public static void EmptyCurrentSlot() => currentSlot = Slot.Empty;
}

public class GameData
{
    public bool[] isLevelAvailable;

    public GameData(int levelCount, int defaultAvailableLevels)
    {
        isLevelAvailable = new bool[levelCount];
        for (int i = 0; i < defaultAvailableLevels; i++)
        {
            isLevelAvailable[i] = true;
        }
    }
}
