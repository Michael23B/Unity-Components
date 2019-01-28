using UnityEngine;
using UnityEngine.UI;

public class CurrentTurnUI : MonoBehaviour
{
    private Text textComponent;

    void Start()
    {
        Canvas canvas = FindObjectOfType<Canvas>();
        textComponent = Utilities.CreateText(canvas.transform, 120, 270, "Current Turn", 15, Color.yellow).GetComponent<Text>();

        Listener.CreateListener(transform, (sender, e) => TurnStartedEvent(((TurnEventArgs)e).CurrentUnit, ((TurnEventArgs)e).Turn, ((TurnEventArgs)e).RoundLength), Constants.EventName.UNITTURNSTARTED);
    }

    private void TurnStartedEvent(Unit unit, int turn, int roundLength)
    {
        textComponent.text = $"{unit.Id} is taking turn {turn + 1} of {roundLength}";
    }

    // TODO Turn modified event, when a unit is destroyed or added the ui should be updated
}
