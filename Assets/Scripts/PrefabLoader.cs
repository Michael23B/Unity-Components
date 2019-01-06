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
    [SerializeField] private GameObject unitRegistryPrefab = null;
    [SerializeField] private GameObject playerControllerPrefab = null;
    [SerializeField] private GameObject gridControllerPrefab = null;

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

    public UnitRegistry CreateUnitRegistry()
    {
        GameObject go = Instantiate(unitRegistryPrefab);
        go.transform.parent = transform;

        return go.GetComponent<UnitRegistry>();
    }

    public PlayerController CreatePlayerController()
    {
        GameObject go = Instantiate(playerControllerPrefab);
        go.transform.parent = transform;

        return go.GetComponent<PlayerController>();
    }

    public GridController CreateGridController()
    {
        GameObject go = Instantiate(gridControllerPrefab);
        go.transform.parent = transform;

        return go.GetComponent<GridController>();
    }
}
