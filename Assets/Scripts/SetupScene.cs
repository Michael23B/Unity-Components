using UnityEngine;

public class SetupScene : MonoBehaviour
{
    private void Start()
    {
        var playerController = GameComponents.PlayerController; // Load PlayerController
        var gridController = GameComponents.GridController; // Load GridController
        GameComponents.TurnHandler.StartGame(); // Load TurnHandler and start game
    }
}
