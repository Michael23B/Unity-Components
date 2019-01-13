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
    }
}
