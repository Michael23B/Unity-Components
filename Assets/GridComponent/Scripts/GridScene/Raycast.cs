using UnityEngine;

//TODO Implement a more elegant way of using the raycast hit; a seperate file for functions to call based on the type of GO hit?

public class Raycast : MonoBehaviour
{
    public static Raycast Instance { get; private set; }

    void Awake()
    {
        //Singleton instance ensures we always have a single static access point to this class
        if (Instance != null && Instance != this) Destroy(gameObject);
        else Instance = this;
    }
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
            string eventName = Input.GetMouseButtonDown(0) ? Constants.EventNames.TileClicked : Constants.EventNames.TileHovered;
            EventHandler.Instance.TriggerEvent(eventName, this, new TileEventArgs(hoveredTile));
        }
    }
}
