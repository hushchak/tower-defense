using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ObstacleGrid : MonoBehaviour, ILevelInitializable
{
    [SerializeField] private Tilemap obstacleTilemap;
    [SerializeField] private TowerGrid towerGrid;

    public void Initialize(LevelData data)
    {
        Vector2[] obstacleTileWorldPositions = GetObstacleTileWorldPositions();
        for (int i = 0; i < obstacleTileWorldPositions.Length; i++)
        {
            towerGrid.TryBlockTile(obstacleTileWorldPositions[i]);
        }
    }

    private Vector2[] GetObstacleTileWorldPositions()
    {
        List<Vector2> worldPositions = new();
        BoundsInt bounds = obstacleTilemap.cellBounds;

        foreach (Vector3Int position in bounds.allPositionsWithin)
        {
            if (!obstacleTilemap.HasTile(position))
                continue;

            Vector2 tilePosition = obstacleTilemap.GetCellCenterWorld(position);
            worldPositions.Add(tilePosition);
        }

        return worldPositions.ToArray();
    }
}
