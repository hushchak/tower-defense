using UnityEngine;

[CreateAssetMenu(menuName = "Wave/Wave Data", fileName = "WaveData")]
public class WaveData : ScriptableObject
{
    [field: SerializeField] public WaveAction[] Actions { get; private set; }

    [System.Serializable]
    public class WaveAction
    {
        [field: SerializeField] public EnemyData Enemy { get; private set; }
        [field: SerializeField] public int Number { get; private set; }
        [field: SerializeField] public float Frequency { get; private set; }
        [field: SerializeField] public float WaitTimeAfter { get; private set; }
    }
}
