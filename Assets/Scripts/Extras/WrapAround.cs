using UnityEngine;


public class WrapAround : MonoBehaviour
{
    public float orthographicSize = 16.0f;

    public Vector2Int aspectRatio = new Vector2Int(1, 1);

    [Header("Debug Info")]
    private float left;

    private float right, top, bottom;

    private void Awake()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        float v = orthographicSize * 2;

        float har = aspectRatio.x / (float)aspectRatio.y;

        float h = v * har;

        left = -h;
        right = h;
        top = v;
        bottom = -v;
        CheckWrapping();
    }

    private Vector3 Position
    {
        get => transform.position;
        set => transform.position = value;
    }

    private void CheckWrapping()
    {
        if ((Position.x < left) || (Position.x > right) || (Position.y < bottom) || (Position.y > top))
        {
            Position = Vector3.zero;
        }
        //if (Position.x < left) Position = new Vector3(right, Position.y);
        //if (Position.x > right) Position = new Vector3(left, Position.y);
        //if (Position.y < bottom) Position = new Vector3(Position.x, top);
        //if (Position.y > top) Position = new Vector3(Position.x, bottom);
    }
}
