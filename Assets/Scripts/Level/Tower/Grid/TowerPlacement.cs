using UnityEngine;

public class TowerPlacement
{
    Transform previewParent;
    private TowerPreview currentTowerPreview;

    public TowerPlacement(Transform previewParent)
    {
        this.previewParent = previewParent;
    }

    public void CreateTowerPreview(TowerPreview preview)
    {
        currentTowerPreview = Object.Instantiate(preview, previewParent);
        currentTowerPreview.Hide();
    }

    public void DestroyTowerPreview()
    {
        if (currentTowerPreview != null)
        {
            Object.Destroy(currentTowerPreview.gameObject);
            currentTowerPreview = null;
        }
    }

    public void UpdateTowerPreview(Vector2 mouseWorldPosition)
    {
        if (TowerGrid.Instance.PositionInsideGrid(mouseWorldPosition))
        {
            currentTowerPreview.SetUnplaceableColor(
                !TowerGrid.Instance.CanPlaceTower(mouseWorldPosition, out Vector2 cellPosition)
            );
            currentTowerPreview.transform.position = cellPosition;
            currentTowerPreview.Show();
        }
        else
        {
            currentTowerPreview.Hide();
        }
    }

    public bool TryPlaceTowerAt(Tower towerPrefab, Vector2 mouseWorldPosition)
    {
        return TowerGrid.Instance.TryPlaceTower(mouseWorldPosition, towerPrefab.gameObject);
    }

    public bool InRangeForPlacement(Vector2 mouseWorldPosition)
    {
        return TowerGrid.Instance.PositionInsideGrid(mouseWorldPosition);
    }
}
