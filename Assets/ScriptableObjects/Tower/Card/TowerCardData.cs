using UnityEngine;

[CreateAssetMenu(menuName = "Tower/Tower Card Data", fileName = "TowerCard")]
public class TowerCardData : ScriptableObject
{
    [field: SerializeField] public GameObject Prefab { get; private set; }
    [field: SerializeField] public GameObject Preview { get; private set; }
    [field: SerializeField] public int Cost { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public string Name { get; private set; }
}
