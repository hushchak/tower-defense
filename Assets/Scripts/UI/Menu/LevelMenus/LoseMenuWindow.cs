using UnityEngine;

public class LoseMenuWindow : Window
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

    public void LoseGame()
    {
        Debug.Log("Player lose");
    }
}
