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

    public bool GetValue(int x, int y, out T value)
    {
        value = default;
        if (!CooridnatesValid(x, y))
            return false;

        value = grid[x, y];
        return true;
    }

    public void SetValue(T value, int x, int y)
    {
        // TODO: Error
        if (!CooridnatesValid(x, y))
        {
            Debug.Log("You are trying to set grid value that does not exist");
            return;
        }

        grid[x, y] = value;
    }

    public bool TryGetValue(Vector2 worldPosition, out T value)
    {
        value = default;
        if (!PositionInsideGrid(worldPosition))
            return false;

        int xIndex = (int)(origin.x - worldPosition.x / cellSize.x);
        int yIndex = (int)(origin.y - worldPosition.y / cellSize.y);

        value = grid[xIndex, yIndex];
        return true;
    }

    public bool TryGetIndex(Vector2 worldPosition, out Vector2Int index)
    {
        index = default;
        if (PositionInsideGrid(worldPosition))
            return false;

        int xIndex = (int)((worldPosition.x - origin.x) / cellSize.x);
        int yIndex = (int)((worldPosition.y - origin.y) / cellSize.y);

        index = new Vector2Int(xIndex, yIndex);
        return true;
    }

    public int GetLength(int dimension)
    {
        return grid.GetLength(dimension);
    }

    private bool CooridnatesValid(int x, int y)
    {
        return !(x < 0 || x > grid.GetLength(0) - 1 || y < 0 || y > grid.GetLength(1) - 1);
    }

    private bool PositionInsideGrid(Vector2 worldPosition)
    {
        float maxXPosition = origin.x + cellSize.x * grid.GetLength(0);
        float maxYPosition = origin.y + cellSize.y * grid.GetLength(1);
        return worldPosition.x < origin.x || worldPosition.x > maxXPosition
            || worldPosition.y < origin.y || worldPosition.y > maxYPosition;
    }
}
