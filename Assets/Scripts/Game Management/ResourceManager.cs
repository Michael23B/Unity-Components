using UnityEngine;

/*
 * Loads resources into arrays based on their folder/type for easy access.
 */
public static class ResourceManager
{
    // TODO The order of resource load is likely not reliable, refactor this to create a factory
    // https://stackoverflow.com/questions/52141170/how-developers-keep-reference-to-huge-number-of-different-entity-prefabs
    static readonly GameObject[] gameObjects = Resources.LoadAll<GameObject>("GameObjects");

    // TODO Add an enum for resource names/ids
    public static GameObject GetResource(int resourceId)
    {
        return gameObjects[resourceId];
    }
}