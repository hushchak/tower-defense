using UnityEngine;

public class TowerPointWindow : Window
{
    [SerializeField] private GameObject menuObject;

    private Tower currentTower;

    public override void Open()
    {
        Debug.LogError("Invalid open method");
    }

    public void Open(TowerPointClickData data)
    {
        menuObject.SetActive(true);
    }

    public override void Close()
    {
        menuObject.SetActive(false);
        currentTower = null;
    }

    public void Sell()
    {
        // TODO: proper sell logic
    }

    public void Upgrade()
    {
        // TODO: upgrade logic
    }
}
