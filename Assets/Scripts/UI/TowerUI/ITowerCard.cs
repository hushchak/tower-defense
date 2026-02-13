using UnityEngine;

public interface ITowerCard
{
    public Tower GetPrefab();
    public TowerPreview GetPreview();
    public int GetCost();

    public void Select();
    public void Deselect();
}
