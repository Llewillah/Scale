using UnityEngine;
using UnityEngine.Rendering;

enum TileState 
{ 
    clear, water, road, building
}

public class Tile : MonoBehaviour
{
    Sprite sprite;
    int x, y;

    TileState curState;

    public void SetUp(int x, int y) 
    {
        this.x = x;
        this.y = y;

        curState = TileState.clear;
    }
}
