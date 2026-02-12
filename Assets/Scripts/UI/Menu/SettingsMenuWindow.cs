using UnityEngine;

public class SettingsMenuWindow : Window
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
}
