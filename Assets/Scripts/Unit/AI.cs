using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;

public static class AI
{
    private static readonly Random rand = new Random();
    static GameObject debugPrefab = Resources.Load<GameObject>("DebugObject");

    //Simple test to get a random location to move.
    public static UnitAction GetAction(Unit unit)
    {
        //Get the surrounding tiles and move to a random one.
        List<Tile> tilesToMoveTo = GridController.Instance.GetSurroundingTiles(unit);

        if (tilesToMoveTo.Count == 0) return new UnitAction();

        int randomIndex = rand.Next(0, tilesToMoveTo.Count - 1);

        string actionName = "Move";

        foreach (Tile t in tilesToMoveTo) Object.Instantiate(debugPrefab, t.GetPositionWithOffset(), Quaternion.identity);

        return new UnitAction(tilesToMoveTo[randomIndex], actionName);
    }
}