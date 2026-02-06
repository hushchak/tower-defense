using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlotMenuWindow : Window
{
    [SerializeField] private GameObject menuObject;

    public override void Open()
    {
        menuObject.SetActive(true);
    }

    public override void Close()
    {
        menuObject.SetActive(false);
    }

    public async void LoadLevelMenuScene(int slot)
    {
        try
        {
            SaveManager.SetCurrentSlot((SaveManager.Slot)slot);
            await SceneLoader.LoadScene(SceneData.Tags.Main, SceneData.Names.LevelMenu, true);
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
