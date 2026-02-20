using UnityEngine;

public class TowerPlacementWindow : Window, ILevelInitializable
{
    [SerializeField] private GameObject menuObject;

    private TowerData[] towers;

    public override void Open()
    {
        menuObject.SetActive(true);
    }

    public override void Close()
    {
        menuObject.SetActive(false);
    }

    public void Initialize(LevelData data)
    {
        towers = data.Towers;
        InitializeCards(towers);
    }

    private void InitializeCards(TowerData[] towerData)
    {
        // TODO: Initialize cards
    }

    public void PlaceTower(TowerData towerData)
    {
        // TODO: Place tower on point logic
        // point.TryPlaceTower(towerData);
    }
}
