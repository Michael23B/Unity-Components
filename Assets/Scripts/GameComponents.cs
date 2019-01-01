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
}
