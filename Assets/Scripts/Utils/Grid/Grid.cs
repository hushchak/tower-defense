using UnityEngine;

public class Grid<T>
{
    private T[,] grid;
    private Vector2 cellSize;
    private Vector2 origin;

    public Grid(int x, int y, Vector2 cellSize, Vector2 origin)
    {
        grid = new T[x, y];
        this.cellSize = cellSize;
        this.origin = origin;
    }

    public bool TryGetValue(int x, int y, out T value)
    {
        value = default;
        if (!CoordinatesValid(x, y))
            return false;

        value = grid[x, y];
        return true;
    }

    public void SetValue(int x, int y, T value)
    {
        // TODO: Error
        if (!CoordinatesValid(x, y))
        {
            Debug.Log("You are trying to set grid value that does not exist");
            return;
        }

        grid[x, y] = value;
    }

    public bool TryGetValue(Vector2 worldPosition, out T value, out Vector2Int index)
    {
        index = Vector2Int.zero;
        value = default;
        if (!PositionInsideGrid(worldPosition))
            return false;

        int xIndex = (int)((worldPosition.x - origin.x) / cellSize.x);
        int yIndex = (int)((worldPosition.y - origin.y) / cellSize.y);
        if (!CoordinatesValid(xIndex, yIndex))
            return false;

        index = new Vector2Int(xIndex, yIndex);
        value = grid[xIndex, yIndex];
        return true;
    }

    public bool TryGetIndex(Vector2 worldPosition, out Vector2Int index)
    {
        index = default;
        if (!PositionInsideGrid(worldPosition))
            return false;

        int xIndex = (int)((worldPosition.x - origin.x) / cellSize.x);
        int yIndex = (int)((worldPosition.y - origin.y) / cellSize.y);
        if (!CoordinatesValid(xIndex, yIndex))
            return false;

        index = new Vector2Int(xIndex, yIndex);
        return true;
    }

    public int GetLength(int dimension)
    {
        return grid.GetLength(dimension);
    }

    private bool CoordinatesValid(int x, int y)
    {
        return !(x < 0 || x > grid.GetLength(0) - 1 || y < 0 || y > grid.GetLength(1) - 1);
    }

    private bool PositionInsideGrid(Vector2 worldPosition)
    {
        float maxXPosition = origin.x + cellSize.x * grid.GetLength(0);
        float maxYPosition = origin.y + cellSize.y * grid.GetLength(1);
        return worldPosition.x >= origin.x && worldPosition.x <= maxXPosition
            && worldPosition.y >= origin.y && worldPosition.y <= maxYPosition;
    }
}
