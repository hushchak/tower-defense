using UnityEngine;

[CreateAssetMenu(menuName = "Level", fileName = "LevelData")]
public class LevelDataSO : ScriptableObject
{
    [field: SerializeField] public WaveData[] Waves { get; private set; }
    [field: SerializeField] public int MaxPlayerHealth { get; private set; }
    [field: SerializeField] public string LevelSceneName { get; private set; }
}
