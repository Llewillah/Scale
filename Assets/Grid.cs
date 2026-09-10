using System.Collections.Generic;
using UnityEngine;

public class Grid {

    Tile[,] grid;

    int width, height;
    float cellSize;
    Vector2 originPos;

    public Grid(int width, int height, float cellSize, Vector2 originPos, SpawningManager sM)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPos = originPos;

        grid = new Tile[width, height];
        GameObject gridObj = new GameObject();
        gridObj.name = "Grid";


        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = sM.SpawnTile(GetWorldPos(x,y), gridObj.transform);
                grid[x, y].SetUp(x, y);
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

    public bool CheckEmpty(Vector2 pos) 
    {
        GetGridPos(pos, out int x, out int y);
        return grid[x, y].CheckClear();
    }

    public void SetBuilding(Vector2 pos) 
    {
        GetGridPos(pos, out int x, out int y);
        grid[x, y].SetState(TileState.building);
    }

    public void SetRoad(List<Vector2Int> path) 
    {
        foreach (Vector2Int t in path)
        {
            grid[t.x,t.y].SetState(TileState.road);
            
        }

        // change the sprite of each tile to correct road based on road dir
    }
}
