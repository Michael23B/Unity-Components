using UnityEngine;

/*
 * Stores a list of prefabs to instantiate when needed.
 * Instantiates the prefabs and returns the type requested.
 */
public class PrefabLoader : MonoBehaviour
{
    public static PrefabLoader Instance { get; private set; }

    [Header("Game Manager Components")]
    [SerializeField] private GameObject turnHandlerPrefab = null;

    private void Awake()
    {
        Instance = this.GetAndEnforceSingleInstance(Instance);
        DontDestroyOnLoad(this);
    }

    public TurnHandler CreateTurnHandler()
    {
        GameObject go = Instantiate(turnHandlerPrefab);
        go.transform.parent = transform;

        return go.GetComponent<TurnHandler>();
    }
}
