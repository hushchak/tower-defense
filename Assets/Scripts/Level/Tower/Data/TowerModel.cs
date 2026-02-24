using UnityEngine;

public class TowerModel
{
    private TowerLevelData[] levelData;
    public int currentLevel;

    public ProjectileData ProjectileData { get; private set; }
    public float Frequency { get; private set; }
    public float Radius { get; private set; }
    public Sound ShotSound { get; private set; }
    public int SellCost { get; private set; }

    public TowerTargerStrategy CurrentTowerStrategy { get; private set; }

    public TowerModel(TowerData data)
    {
        levelData = data.LevelData;

        SetLevel(0);
        SetTowerStrategy(data.DefaultTargetStrategy);
    }

    public void SetTowerStrategy(TowerTargerStrategy strategy) => CurrentTowerStrategy = strategy;
    public bool CanUpgrade() => currentLevel < levelData.Length - 1;
    public bool EnoughMoneyForUpgrade() => PlayerMoney.Instance.GetMoney() >= GetUpgradeCost();
    public int GetUpgradeCost() => CanUpgrade() ? levelData[currentLevel + 1].UpgradeCost : 0;
    public int GetCurrentLevel() => currentLevel + 1;
    public int GetMaxLevel() => levelData.Length + 1;

    public bool TryUpgrade(out int cost)
    {
        if (!CanUpgrade() || !EnoughMoneyForUpgrade())
        {
            cost = 0;
            return false;
        }

        cost = levelData[currentLevel + 1].UpgradeCost;
        SetLevel(currentLevel + 1);
        return true;
    }

    private void SetLevel(int level)
    {
        if (level > levelData.Length)
        {
            Debug.LogWarning("You are trying to upgrade max level tower");
            return;
        }

        currentLevel = level;

        ProjectileData = levelData[currentLevel].ProjectileData;
        Frequency = levelData[currentLevel].Frequency;
        Radius = levelData[currentLevel].Radius;
        ShotSound = levelData[currentLevel].ShotSound;
        SellCost = levelData[currentLevel].SellCost;
    }
}
