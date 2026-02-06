using UnityEngine;

public class LevelMenuWindow : Window
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

    public void LoadLevel(LevelDataSO levelDataSO)
    {
        LevelLoader.LoadLevel(levelDataSO);
    }
}
