using UnityEngine;

enum TileState 
{ 
    clear, water, road
}

public class Tile : MonoBehaviour
{
    Sprite sprite;
    int x, y;

    TileState curState;


}
