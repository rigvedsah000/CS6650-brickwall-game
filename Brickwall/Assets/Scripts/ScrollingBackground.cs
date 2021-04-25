using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public BoxCollider2D ground;
    public float groundVerticalLength;

    // Start is called before the first frame update
    void Start()
    {
        ground = GetComponent<BoxCollider2D>();
        groundVerticalLength = ground.size.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -groundVerticalLength)
        {
            repositionBackground();
        }
    }

    private void repositionBackground()
    {
        Vector2 groundOffset = new Vector2(0, groundVerticalLength * 2);
        transform.position = (Vector2) transform.position + groundOffset;
    }
}
