using UnityEngine;

[System.Serializable]
public class TowerLevelData
{
    [field: Header("Shooting")]
    [field: SerializeField] public ProjectileData ProjectileData { get; private set; }
    [field: SerializeField] public float Frequency { get; private set; }
    [field: SerializeField] public float Radius { get; private set; }
    [field: SerializeField] public Sound ShotSound { get; private set; }
    [field: Space]
    [field: SerializeField] public int UpgradeCost { get; private set; }
    [field: SerializeField] public int SellCost { get; private set; }
}
