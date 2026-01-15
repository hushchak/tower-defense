using UnityEngine;

[CreateAssetMenu(menuName = "Level", fileName = "LevelData")]
public class LevelData : ScriptableObject
{
    [field: SerializeField] public WaveData[] Waves { get; private set; }
    [field: SerializeField] public int PlayerHealth { get; private set; }
    [field: SerializeField] public string LevelSceneName { get; private set; }
}
