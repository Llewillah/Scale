using Unity.VisualScripting;
using UnityEngine;

public enum TileState 
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

    public bool CheckClear() 
    {
        return curState == TileState.clear;
    }

    public void SetState(TileState state) 
    { 
        curState = state;
    }
}
