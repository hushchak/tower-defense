using UnityEngine;

public class AssignCamera : MonoBehaviour, ILevelInitializable
{
    [SerializeField] private Canvas canvas;

    public void Initialize(LevelData data)
    {
        canvas.worldCamera = Camera.main;
        canvas.sortingLayerName = "Foreground";
    }
}
