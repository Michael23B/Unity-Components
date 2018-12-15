using UnityEngine;

public static class DebugTools
{
    static readonly GameObject debugPrefab = Resources.Load<GameObject>("DebugObject");

    //Spawns the "DebugObject" GameObject in the resources folder
    public static GameObject SpawnDebugObject(Vector3 position)
    {
        return Object.Instantiate(debugPrefab, position, Quaternion.identity);
    }
}
