// Assumes each even row is pushed to the right. Logic from here https://www.redblobgames.com/grids/hexagons/
public static class HexHelper
{
    static readonly int[,] EvenRowNeighborCoordinates = { { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, 0 }, { 0, 1 }, { 1, 1 } };
    static readonly int[,] OddRowNeighborCoordinates = { { 1, 0 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 }, { 0, 1 } };

    // Returns the x and y coordinates of the neighbor in the direction provided. Direction must be from 0 - 6.
    public static int[] GetHexNeighborCoordinates(bool isRowEven, int direction)
    {
        int[,] neighborCoords = isRowEven ? EvenRowNeighborCoordinates : OddRowNeighborCoordinates;

        if (direction > 5 || direction < 0) return null;

        return new[] { neighborCoords[direction, 0], neighborCoords[direction, 1] };
    }
}
