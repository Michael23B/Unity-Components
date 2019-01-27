using UnityEngine;

/*
 * Loads GameComponents
 */
public class SetupScene : MonoBehaviour
{
    private void Start()
    {
        var playerController = GameComponents.PlayerController; // Load PlayerController
        var gridController = GameComponents.GridController; // Load GridController
        var lifecycleEvents = GameComponents.LifecycleEvents; // Load LifecycleEvents
        var gameState = GameComponents.GameState; // Load GameState
        GameComponents.TurnHandler.StartGame(); // Load TurnHandler and start game

        // Setup some default units for the grid
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 0, 1);
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 0, 2);
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 0, 3);
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 4, 1);
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 4, 2);
        UnitCreation.CreateAndSetupUnit(ResourceManager.GetUnitPrefab(Constants.UnitPrefabType.Dude), 4, 3);
    }
}