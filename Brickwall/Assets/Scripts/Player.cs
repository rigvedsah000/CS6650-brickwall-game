using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private Rigidbody2D rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 accleration = Input.acceleration;
        if(accleration.x >= 0.1 || accleration.x <= -0.1)
        {
            float displacement = (accleration.x * Time.deltaTime) * Time.deltaTime * 500;
            Vector2 new_position = new Vector2(
                Mathf.Clamp(rigidBody.transform.position.x + displacement, -11.5f, 11.5f), 
                rigidBody.transform.position.y
                );
            rigidBody.transform.position = new_position;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameController.Instance.GameOver();
    }
}
