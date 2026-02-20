using UnityEngine;

public class MainMenuWindow : Window
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

    public void QuitGame()
    {
        Application.Quit();
    }
}
