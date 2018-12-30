using UnityEngine;

/*
 * Loads resources into arrays based on their folder/type for easy access.
 */
public static class ResourceManager
{
    // TODO The order of resource load is likely not reliable, refactor this to use a manual list or sort by tags or something
    static readonly GameObject[] gameObjects = Resources.LoadAll<GameObject>("GameObjects");

    // TODO Add an enum for resource names/ids
    public static GameObject GetResource(int resourceId)
    {
        return gameObjects[resourceId];
    }

    public static GameObject GetInstantiatedResource(int resourceId, Vector3 position)
    {
        return Object.Instantiate(gameObjects[resourceId], position, Quaternion.identity);
    }
}
