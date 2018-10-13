using UnityEngine;

public class Raycast : MonoBehaviour
{
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            CheckHitTile(hit);
        }
    }

    void CheckHitTile(RaycastHit hit)
    {
        Transform objectHit = hit.transform;
        Tile hoveredTile = objectHit?.transform?.parent?.GetComponent<Tile>();

        if (hoveredTile) EventHandler.Instance.TriggerEvent("TileHovered", this, new TileEventArgs(hoveredTile));
    }
}
