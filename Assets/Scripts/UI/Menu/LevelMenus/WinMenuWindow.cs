using UnityEngine;

public class WinMenuWindow : Window
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

    public void WinGame()
    {
        Debug.Log("Player win");
    }
}
