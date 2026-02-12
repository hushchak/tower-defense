using System;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;

    private void Awake()
    {
        InitializeButtons(SaveManager.GetDataFromCurrentSlot());
    }

    private void InitializeButtons(GameData data)
    {
        for (int i = 0; i < data.isLevelAvailable.Length; i++)
        {
            levelButtons[i].interactable = data.isLevelAvailable[i];
        }
    }

    public async void ExitLevelMenu()
    {
        try
        {
            SaveManager.EmptyCurrentSlot();
            await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.MainMenu, true);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public void LoadLevel(LevelDataSO data)
    {
        LevelLoader.LoadLevel(data);
    }
}
