using UnityEngine;

public class ScrollingObject : MonoBehaviour
{
    public Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = new Vector2(0, -4f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
