
using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;

    private float eta = 0.05f;
    private float threshold = 1;
    private int zero_acc_readings = 0;
    private int dir = 0;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        Input.gyro.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {

        // GYRO

        // Debug.Log("Angles: " + (Input.gyro.rotationRateUnbiased.x * 100));

        Vector3 angles = Input.gyro.rotationRateUnbiased;

        if (zero_acc_readings == 3)
        {
            if (angles.x * 100 < -70) dir = 1;
            if (angles.x * 100 > 70) dir = -1;
        }

        // STEP COUNTER

        Vector3 accleration = Input.acceleration;
        float acc = accleration.magnitude;

        //Debug.Log("Acceleration: " + acc);

        if (acc < threshold)
        {
            if (zero_acc_readings < 3)
            {
                zero_acc_readings += 1;
            }
            else
            {
                dir = 0;
            }
        }
        else
        {
            zero_acc_readings = 0;
        }
        if (zero_acc_readings < 3)
        {
            Vector2 new_position = new Vector2(
            Mathf.Clamp(rigidBody.transform.position.x + eta * acc * dir, -11.5f, 11.5f),
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