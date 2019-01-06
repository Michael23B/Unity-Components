/*
 * Provides a static access point to single instances of certain classes.
 */
public static class GameComponents
{
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

    private static TurnHandler turnHandler;

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

    private static UnitRegistry unitRegistry;

    public static PlayerController PlayerController
    {
        get
        {
            if (!playerController)
            {
                playerController = PrefabLoader.Instance.CreatePlayerController();
            }

            return playerController;
        }
    }

    private static PlayerController playerController;

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

    private static GridController gridController;
}
