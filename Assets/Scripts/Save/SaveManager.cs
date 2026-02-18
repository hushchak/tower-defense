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

    private static PreferencesData preferenceData;

    private static Dictionary<Slot, GameData> gameDataDictionary;
    private static Slot currentSlot = Slot.Empty;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        gameDataDictionary = new Dictionary<Slot, GameData>
        {
            { Slot.Slot1, ReadGameData(Slot.Slot1) },
            { Slot.Slot2, ReadGameData(Slot.Slot2) },
            { Slot.Slot3, ReadGameData(Slot.Slot3) }
        };
        preferenceData = ReadPreferencesData();

        EventChannel applicationQuitChannel = Resources.Load<EventChannel>("Events/ApplicationQuitChannel");
        applicationQuitChannel.Subscribe(SaveData);
    }

    #region Read
    private static GameData ReadGameData(Slot slot)
    {
        try
        {
            string filePath = GetSlotPath(slot.ToString());
            if (!IsFileExists(filePath))
            {
                File.WriteAllText(filePath, JsonUtility.ToJson(
                    new GameData(
                        levelAmount,
                        defaultAvailableLevels
                    ),
                    true
                ));
            }

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

    private static PreferencesData ReadPreferencesData()
    {
        try
        {
            string filePath = GetPreferencesPath();
            if (!IsFileExists(filePath))
            {
                File.WriteAllText(filePath, JsonUtility.ToJson(
                    new PreferencesData(
                        1f,
                        1f,
                        1f
                    ),
                    true
                ));
            }

            string json = File.ReadAllText(filePath);
            return JsonUtility.FromJson<PreferencesData>(json);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }

        return null;
    }
    #endregion
    #region Save
    private static void SaveData()
    {
        try
        {
            SaveGameData();
            SavePreferenceData();
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }

    private static void SaveGameData()
    {
        foreach (KeyValuePair<Slot, GameData> pair in gameDataDictionary)
        {
            string filePath = GetSlotPath(pair.Key.ToString());
            if (!IsFileExists(filePath))
            {
                File.WriteAllText(filePath, JsonUtility.ToJson(
                    new GameData(
                        levelAmount,
                        defaultAvailableLevels
                    ),
                    true
                ));
            }

            string json = JsonUtility.ToJson(pair.Value, true);
            File.WriteAllText(filePath, json);
        }
    }

    private static void SavePreferenceData()
    {
        string filePath = GetPreferencesPath();
        if (!IsFileExists(filePath))
        {
            File.WriteAllText(filePath, JsonUtility.ToJson(
                new PreferencesData(
                    1f,
                    1f,
                    1f
                ),
                true
            ));
        }

        string json = JsonUtility.ToJson(preferenceData, true);
        File.WriteAllText(filePath, json);
    }
    #endregion
    #region File Handling

    private static bool IsFileExists(string slotFilePath)
    {
        string directoryPath = System.IO.Path.GetDirectoryName(slotFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }
        return File.Exists(slotFilePath);
    }

    private static void MakeSureSlotFileInCorrectFormat(string slotFilePath)
    {
        string json = File.ReadAllText(slotFilePath);
        try
        {
            GameData data = JsonUtility.FromJson<GameData>(json);

            int availableLevels = 0;
            for (int i = 0; i < data.IsLevelAvailable.Length; i++)
            {
                if (data.IsLevelAvailable[i])
                    availableLevels++;
            }

            if (data.IsLevelAvailable.Length != levelAmount || availableLevels < defaultAvailableLevels)
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
    #endregion
    #region Path
    private static string GetSlotPath(string slotName)
    {
        return System.IO.Path.Combine(Application.persistentDataPath, "DataSlots", slotName);
    }
    private static string GetPreferencesPath()
    {
        return System.IO.Path.Combine(Application.persistentDataPath, "Preferences", "preferences.json");
    }
    #endregion

    public static GameData GetDataFromCurrentSlot()
    {
        if (gameDataDictionary.TryGetValue(currentSlot, out GameData data))
        {
            return data;
        }
        return null;
    }
    public static PreferencesData GetPreferencesData()
    {
        return preferenceData;
    }

    public static void SetCurrentSlot(Slot slot) => currentSlot = slot;
    public static void EmptyCurrentSlot() => currentSlot = Slot.Empty;
}
