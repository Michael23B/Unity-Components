using UnityEngine;

public class Tile : MonoBehaviour
{
    public GameObject BodyGameObject;

    //Offset is the distance from the middle to the top of the body
    public float Offset { get; set; }

    private GameObject body;

    private void Awake()
    {
        body = Instantiate(BodyGameObject, transform.position, transform.rotation);
        body.transform.parent = transform;

        //The offset is half of the bodies height
        Offset = body.GetComponent<Renderer>().bounds.size.y / 2;
    }

    //Returns the position of the top-center of the tile
    public Vector3 GetPositionWithOffset()
    {
        Vector3 posWithOffset = transform.position;
        posWithOffset.y += Offset;

        return posWithOffset;
    }
}
