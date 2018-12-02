using UnityEngine;

public static class GridGeneration
{
    //Instantiates a number of Tiles in a grid formation. Expects hex shaped tiles.
    public static Tile[,] GenerateTileGrid(Tile tile, int[,] map, Vector2 margin, Transform parent)
    {
        Tile[,] grid = new Tile[map.GetLength(0), map.GetLength(1)];

        for (int x = 0; x < map.GetLength(0); ++x)
        {
            for (int y = 0; y < map.GetLength(1); ++y)
            {
                if (map[x, y] == Constants.TileTypes.NONE) continue;

                Vector3 position = GetTilePosition(x, y, parent.position, margin);
                grid[x, y] = Object.Instantiate(tile, position, Quaternion.identity);
                grid[x, y].Setup(x, y, map[x, y]);
                grid[x, y].transform.parent = parent;
            }
        }

        return grid;
    }

    //Calculates a tiles world position based on starting position, margin and the current coordinates.
    private static Vector3 GetTilePosition(int x, int y, Vector3 startPosition, Vector2 margin)
    {
        //Do some math to figure out where a tile should go based on parameters
        float xOffset = x * (Mathf.Sqrt(3) / 2 * margin.x);
        float yOffset = y * margin.y;

        if (y % 2 == 1) xOffset -= (Mathf.Sqrt(3) / 2 * margin.x) / 2;

        float tilePosX = startPosition.x + xOffset;
        float tilePosY = startPosition.z + yOffset;

        //Since we are working with a 2d grid, swap the Y and Z co-ordinates
        return new Vector3(tilePosX, startPosition.y, tilePosY);
    }
}