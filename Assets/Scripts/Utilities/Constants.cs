/*
 * Stores constants.
 */
public static class Constants
{
    // TODO consider refactoring these into enums
    public struct EventName
    {
        public const string TILEHOVERED = "TileHovered";
        public const string TILECLICKED = "TileClicked";
        public const string TILERIGHTCLICKED = "TileRightClicked";

        public const string UNITDESTROYED = "UnitDestroyed";

        public const string UNITTURNSTARTED = "UnitTurnStarted";
        public const string UNITTURNENDED = "UnitTurnEnded";
        public const string ROUNDSTARTED = "RoundStarted";
        public const string ROUNDENDED = "RoundEnded";

        public const string APPLICATIONQUITTING = "ApplicationQuitting";
    }

    public struct TileType
    {
        public const int NONE = 0;
        public const int GRASS = 1;
    }

    public struct ActionType
    {
        public const int MOVE = 0;
        public const int ATTACK = 1;
    }

    public enum ParticleEffectType
    {
        TileHovered,
        ActiveUnit
    }

    public enum UnitPrefabType
    {
        Dude
    }
}