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
    [SerializeField] private GameObject unitControlEvents = null;
    [SerializeField] private GameObject gridControllerPrefab = null;
    [SerializeField] private GameObject lifecycleEventsPrefab = null;

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

    public UnitControlEvents CreateUnitControlEvents()
    {
        GameObject go = Instantiate(unitControlEvents);
        go.transform.parent = transform;

        return go.GetComponent<UnitControlEvents>();
    }

    public GridController CreateGridController()
    {
        GameObject go = Instantiate(gridControllerPrefab);
        go.transform.parent = transform;

        return go.GetComponent<GridController>();
    }

    public LifecycleEvents CreateLifecycleEvents()
    {
        GameObject go = Instantiate(lifecycleEventsPrefab);
        go.transform.parent = transform;

        return go.GetComponent<LifecycleEvents>();
    }
}
