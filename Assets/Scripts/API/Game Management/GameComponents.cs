/*
 * Provides a static access point to single instances of certain classes.
 */
public static class GameComponents
{
    private static TurnHandler turnHandler;
    private static UnitRegistry unitRegistry;
    private static UnitControlEvents unitControlEvents;
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

    public static UnitControlEvents UnitControlEvents
    {
        get
        {
            if (!unitControlEvents)
            {
                unitControlEvents = PrefabLoader.Instance.CreateUnitControlEvents();
            }

            return unitControlEvents;
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
                gameState.Initialize();
            }

            return gameState;
        }
    }
}
