using JetBrains.Annotations;
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

        ResetTileColour();
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
    public void GetGridPos(Vector2 pos, out int x, out int y)
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

    public void SetRoad(List<Vector2> path) 
    {
        foreach (Vector2 t in path)
        {
            int x = (int)t.x;
            int y = (int)t.y;

            grid[x,y].SetState(TileState.road);
            grid[x, y].ChangeColour(Color.black);
        }

        // change the sprite of each tile to correct road based on road dir
        ResetTileColour();
    }

    public bool CheckRoad(List<Vector2> path) 
    {
        bool canBuild = true;
        foreach (Vector2 t in path)
        {

            int x = (int)t.x;
            int y = (int)t.y;

            if (grid[x, y].CheckClear())
            {
                grid[x, y].ChangeColour(Color.blue);
            }
            else
            {
                grid[x, y].ChangeColour(Color.red);
                canBuild = false;
            }
        }

        return canBuild;
    }

    public void ResetTileColour() 
    {
        foreach (Tile t in grid) 
        {
            if (t.curState == TileState.road)
            {
                t.ChangeColour(Color.black);
            }
            else 
            {
                t.ChangeColour(Color.lightGreen);
            }
        }
    }

    public bool TraverseRoad(Vector2 startPos, Vector2 endPos) 
    {
        GetGridPos(startPos, out int startX, out int startY);
        GetGridPos(endPos, out int endX, out int endY);

        Stack<Tile> stack = new Stack<Tile>();
        stack.Push(grid[startX, startY]);

        
        while (stack.Count > 0) 
        { 
            Tile cur = stack.Pop();

            //check all neighburs for road + add roads to stack
            //also check if next tile is the end
            if (cur.x + 1 < width) 
            {
                if (cur.x + 1 == endX && cur.y == endY) 
                {
                    return true;
                }

                if (grid[cur.x + 1, cur.y].curState == TileState.road) 
                {
                    stack.Push(grid[cur.x + 1, cur.y]);
                }
            }

            if (cur.x - 1 >= 0) 
            {
                if (cur.x - 1 == endX && cur.y == endY)
                {
                    return true;
                }

                if (grid[cur.x - 1, cur.y].curState == TileState.road)
                {
                    stack.Push(grid[cur.x - 1, cur.y]);
                }
            }

            if (cur.y + 1 < height)
            {
                if (cur.y + 1 == endY && cur.x == endX)
                {
                    return true;
                }

                if (grid[cur.x, cur.y + 1].curState == TileState.road)
                {
                    stack.Push(grid[cur.x, cur.y + 1]);
                }
            }

            if (cur.y - 1 >= 0)
            {
                if (cur.y - 1 == endY && cur.x == endX)
                {
                    return true;
                }

                if (grid[cur.x, cur.y - 1].curState == TileState.road)
                {
                    stack.Push(grid[cur.x, cur.y - 1]);
                }
            }
        }

        return false;
    }
}
