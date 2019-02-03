using UnityEngine;

/*
 * Loads GameComponents
 */
public class SetupScene : MonoBehaviour
{
    private void Start()
    {
        // Game components are lazy loaded, we want to load these ones right away
        var unitControlEvents = GameComponents.UnitEvents; // Load UnitEvents
        var gridController = GameComponents.GridController; // Load GridController
        var lifecycleEvents = GameComponents.LifecycleEvents; // Load LifecycleEvents
        var gameState = GameComponents.GameState; // Load GameState
        GameComponents.TurnHandler.StartGame(); // Load TurnHandler and start game

        // Setup some default units for the grid
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 0, 1, UnitStatCollection.GetUnitStats("Player"));
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 0, 2, UnitStatCollection.GetUnitStats("Player"));
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 0, 3, UnitStatCollection.GetUnitStats("Player"));
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 4, 1, UnitStatCollection.GetUnitStats("Enemy"));
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 4, 2, UnitStatCollection.GetUnitStats("Enemy"));
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 4, 3, UnitStatCollection.GetUnitStats("Enemy"));

        // Start particle effects
        UnitActiveParticleEffect.Start();
        TileHoveredParticleEffect.Start();

        // Starts the first turn
        GameComponents.TurnHandler.EndTurn();
    }
}