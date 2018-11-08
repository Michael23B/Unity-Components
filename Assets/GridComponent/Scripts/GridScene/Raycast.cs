using UnityEngine;

public class Raycast : MonoBehaviour
{
    void Update()
    {
        RaycastHit hit = GetRaycastHit();

        CheckHitTile(hit);
    }

    RaycastHit GetRaycastHit()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out RaycastHit hit, 120f);
        return hit;
    }

    void CheckHitTile(RaycastHit hit)
    {
        Transform objectHit = hit.transform;
        Tile hoveredTile = objectHit?.parent?.GetComponent<Tile>(); //Assumes the collider is a direct child of the script

        if (hoveredTile)
        {
            string eventName = Input.GetMouseButtonDown(0) ? Constants.EventNames.TileClick : Constants.EventNames.TileHover;
            EventHandler.Instance.TriggerEvent(eventName, this, new TileEventArgs(hoveredTile));
        }
    }
}
