using System;
using TMPro;
using UnityEditor;
using UnityEngine;

public class TowerGrid : Singleton<TowerGrid>
{
    [SerializeField] private Vector2Int gridSize;
    [SerializeField] private Vector2 gridCellSize;
    [SerializeField] private Vector2 gridOrigin;

    [Space]
    [SerializeField] private bool gizmos;

    private Grid<bool> grid;

    protected override void Awake()
    {
        base.Awake();
        grid = new Grid<bool>(gridSize.x, gridSize.y, gridCellSize, gridOrigin);
    }

    public bool TryPlaceTower(Vector2 worldPosition, GameObject prefab)
    {
        if (grid.TryGetValue(worldPosition, out bool isTowerPlaced, out Vector2Int index))
        {
            if (isTowerPlaced)
                return false;

            GameObject tower = Instantiate(prefab, transform);
            tower.transform.position = GetCellCenter(index.x, index.y);

            grid.SetValue(index.x, index.y, true);
            return true;
        }
        return false;
    }

    private Vector2 GetCellCenter(int x, int y)
    {
        if (grid.TryGetValue(x, y, out bool _))
        {
            Vector2 cellOrigin = gridOrigin + new Vector2(x * gridCellSize.x, y * gridCellSize.y);
            Vector2 cellCenter = cellOrigin + gridCellSize * 0.5f;
            return cellCenter;
        }

        return Vector2.zero;
    }

    private void OnDrawGizmos()
    {
        if (!gizmos)
            return;

        DrawGrid();
        if (grid == null)
            DisplayIndexes();
        else
            DisplayValues();
    }

    private void DisplayValues()
    {
        GUIStyle style = new();
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                grid.TryGetValue(i, j, out bool value);
                Vector2 cellOrigin = gridOrigin + new Vector2(i * gridCellSize.x, j * gridCellSize.y);
                Handles.Label(cellOrigin + gridCellSize * 0.5f, value.ToString(), style);
            }
        }
    }

    private void DisplayIndexes()
    {
        GUIStyle style = new();
        style.normal.textColor = Color.white;
        style.alignment = TextAnchor.MiddleCenter;
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2 cellOrigin = gridOrigin + new Vector2(i * gridCellSize.x, j * gridCellSize.y);
                Handles.Label(cellOrigin + gridCellSize * 0.5f, $"[{i}, {j}]", style);
            }
        }
    }

    private void DrawGrid()
    {
        for (int i = 0; i < gridSize.x; i++)
        {
            for (int j = 0; j < gridSize.y; j++)
            {
                Vector2 cellOrigin = gridOrigin + new Vector2(i * gridCellSize.x, j * gridCellSize.y);
                Gizmos.DrawLine(cellOrigin, cellOrigin + new Vector2(gridCellSize.x, 0));
                Gizmos.DrawLine(cellOrigin, cellOrigin + new Vector2(0, gridCellSize.y));
            }
        }

        Gizmos.color = Color.red;
        float maxXPosition = gridOrigin.x + gridCellSize.x * gridSize.x;
        float maxYPosition = gridOrigin.y + gridCellSize.y * gridSize.y;
        Gizmos.DrawLine(new Vector2(maxXPosition, gridOrigin.y), new Vector2(maxXPosition, maxYPosition));
        Gizmos.DrawLine(new Vector2(gridOrigin.x, maxYPosition), new Vector2(maxXPosition, maxYPosition));
    }
}
