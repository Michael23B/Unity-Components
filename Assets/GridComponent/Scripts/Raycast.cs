using UnityEngine;

public class Raycast : MonoBehaviour
{
    private GridController grid;

    private void Start()
    {
        grid = FindObjectOfType<GridController>();
    }

    //TODO Refactor to an event handler
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Transform objectHit = hit.transform;
            Tile hoveredTile = objectHit?.transform?.parent?.GetComponent<Tile>();

            if (hoveredTile) grid.HoverEvent(hoveredTile);
        }
    }
}
