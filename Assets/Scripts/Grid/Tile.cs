using UnityEngine;

public class Tile : MonoBehaviour
{
    public int X { get; private set; } = -1;
    public int Y { get; private set; } = -1;
    public int StoredId { get; set; } = -1;
    public bool IsOccupied => StoredId != -1;
    public int TileType { get; private set; } //TODO do things with TileType

    private bool initialized = false;
    //Offset from the pivot to the top
    [SerializeField] private float offset = 0;

    //Sets the x and y of a tile. Cannot be changed once it has been set.
    public bool Setup(int x, int y, int tileType)
    {
        if (initialized) return false;

        X = x;
        Y = y;
        initialized = true;
        TileType = tileType;

        return true;
    }

    //Returns the position of the top-center of the tile
    public Vector3 GetPositionWithOffset()
    {
        Vector3 posWithOffset = transform.position;
        posWithOffset.y += offset;
        
        return posWithOffset;
    }

    public void RemoveStoredId()
    {
        StoredId = -1;
    }
}
