using UnityEngine;

public class Grid {

    Tile[,] grid;

    int width, height;
    float cellSize;
    Vector2 originPos;

    public Grid(int width, int height, float cellSize, Vector2 originPos)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        grid = new Tile[width, height];

        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                //grid[i, j].tile = UnitSpawner.Instance.SpawnTile(GetWorldPos(i, j) + Vector2.one * cellSize / 2);
            }
        }
    }

    //Draws the grid purely for testing
    public void DrawGrid()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector2 pos = GetWorldPos(i, j);

                //Draws GridTiles
                Debug.DrawLine(pos, pos + Vector2.up * cellSize);
                Debug.DrawLine(pos, pos + Vector2.right * cellSize);
            }
        }

        Debug.DrawLine(GetWorldPos(width, 0), GetWorldPos(width, height));
        Debug.DrawLine(GetWorldPos(0, height), GetWorldPos(width, height));
    }

    //converts world pos to grid pos
    void GetGridPos(Vector2 pos, out int x, out int y)
    {
        pos -= originPos;

        x = (int)(pos.x / cellSize);
        y = (int)(pos.y / cellSize);
    }

    //converts grid pos to world pos
    Vector2 GetWorldPos(int x, int y)
    {
        return new Vector2(x * cellSize + cellSize / 2, y * cellSize + cellSize / 2) + originPos;
    }

    //takes in position and returns exact grid position
    public Vector2 GetWorldGridTest(Vector2 pos)
    {
        GetGridPos(pos, out int x, out int y);
        return GetWorldPos(x, y);
    }

}
