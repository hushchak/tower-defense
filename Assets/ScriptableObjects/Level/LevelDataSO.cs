using UnityEngine;

[CreateAssetMenu(menuName = "Level", fileName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    [field: Header("General")]
    [field: SerializeField] public string LevelSceneName { get; private set; }
    [field: Header("Content")]
    [field: SerializeField] public WaveData[] Waves { get; private set; }
    [field: SerializeField] public TowerCardData[] Towers { get; private set; }
    [field: Header("Player")]
    [field: SerializeField] public int PlayerMaxHealth { get; private set; }
    [field: SerializeField] public int PlayerStartMoney { get; private set; }
}
