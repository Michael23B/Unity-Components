/*
 * Provides a static access point to single instances of certain classes.
 */
public static class GameComponents
{
    private static TurnHandler turnHandler;
    private static UnitRegistry unitRegistry;
    private static UnitEvents unitEvents;
    private static GridController gridController;
    private static LifecycleEvents lifecycleEvents;
    private static GameState gameState = null;

    public static TurnHandler TurnHandler
    {
        get
        {
            if (!turnHandler)
            {
                turnHandler = PrefabLoader.Instance.CreateTurnHandler();
            }

            return turnHandler;
        }
    }

    public static UnitRegistry UnitRegistry
    {
        get
        {
            if (!unitRegistry)
            {
                unitRegistry = PrefabLoader.Instance.CreateUnitRegistry();
            }

            return unitRegistry;
        }
    }

    public static UnitEvents UnitEvents
    {
        get
        {
            if (!unitEvents)
            {
                unitEvents = PrefabLoader.Instance.CreateUnitEvents();
            }

            return unitEvents;
        }
    }

    public static GridController GridController
    {
        get
        {
            if (!gridController)
            {
                gridController = PrefabLoader.Instance.CreateGridController();
            }

            return gridController;
        }
    }

    public static LifecycleEvents LifecycleEvents
    {
        get
        {
            if (!lifecycleEvents)
            {
                lifecycleEvents = PrefabLoader.Instance.CreateLifecycleEvents();
            }

            return lifecycleEvents;
        }
    }

    public static GameState GameState
    {
        get
        {
            if (gameState == null)
            {
                gameState = new GameState();
            }

            return gameState;
        }
    }
}
