using System;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    private Func<RaycastHit, bool>[] eventTriggers;

    private void Awake()
    {
        //Add additional event triggers here
        eventTriggers = new Func<RaycastHit, bool>[] {CheckHitTile};
    }

    void Update()
    {
        RaycastHit hit = GetRaycastHit();

        //Loop through eventTriggers until one is successful
        foreach (var eventTrigger in eventTriggers)
        {
            if (eventTrigger(hit)) break;
        }
    }

    RaycastHit GetRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 120f);
        return hit;
    }

    #region Event triggers
    
    //Event triggers return true if they fired successfully. Add each trigger to the eventTriggers array in the awake function.

    //Fires TILECLICKED, TILERIGHTCLICKED or TILEHOVERED.
    bool CheckHitTile(RaycastHit hit)
    {
        Transform objectHit = hit.transform;
        Tile hoveredTile = objectHit?.parent?.GetComponent<Tile>(); //Assumes the collider is a direct child of the Tile script.

        if (hoveredTile)
        {
            string eventName;
            if (Input.GetMouseButtonDown(0)) eventName = Constants.EventName.TILECLICKED;
            else if (Input.GetMouseButtonDown(1)) eventName = Constants.EventName.TILERIGHTCLICKED;
            else eventName = Constants.EventName.TILEHOVERED;

            EventHandler.Invoke(eventName, this, new TileEventArgs(hoveredTile));

            return true;
        }

        return false;
    }

    #endregion
}
