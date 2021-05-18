using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector3 initial;
    
    public float eta;
    public float threshold = 1;
    private int zero_acc_readings = 0;
    private int dir = 0;

    // Start is called before the first frame update
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        initial = Input.gyro.attitude.eulerAngles;
        Input.gyro.enabled = true;
    }
    
    // Update is called once per frame
    void Update()
    {

        // GYRO

        // Debug.Log("Angles: " + (Input.gyro.rotationRateUnbiased.x * 100));
        
        Vector3 angles = Input.gyro.rotationRateUnbiased;
        
        if(zero_acc_readings == 3)
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
            if(zero_acc_readings < 3) {
                zero_acc_readings += 1;
            }
            else{
                dir = 0;
            }
        }
        else
        {
            zero_acc_readings = 0;
        }
        if(zero_acc_readings < 3)
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

    private int GetDirection(Vector3 final)
    {
        float x_min, x_max;
        float y_min, y_max;
        float z_min, z_max;

        float min_x;
        float min_y;

        if (initial.x >= 180.0)
        {
            x_max = initial.x;
            x_min = initial.x - 180;
        }
        else
        {
            x_min = initial.x;
            x_max = initial.x + 180;
        }

        if (initial.y >= 180.0)
        {
            y_max = initial.y;
            y_min = initial.y - 180;
        }
        else
        {
            y_min = initial.y;
            y_max = initial.y + 180;
        }

        if (initial.z >= 180.0)
        {
            z_max = initial.z;
            z_min = initial.z - 180;
        }
        else
        {
            z_min = initial.z;
            z_max = initial.z + 180;
        }

        float dtheta_x = Mathf.Abs(final.x - initial.x);

        if (dtheta_x < 45)
        {
            min_x = 0;
        }
        else
        {
            min_x = Mathf.Min(dtheta_x, 360 - dtheta_x);
            initial.x = final.x;
        }

        float dtheta_y = Mathf.Abs(final.y - initial.y);

        if (dtheta_y < 45)
        {
            min_y = 0;
        }
        else
        {
            min_y = Mathf.Min(dtheta_y, 360 - dtheta_y);
            initial.y = final.y;
        }

        if (min_x == min_y) return 0;
        if (min_x > min_y) return -1;
        return 1;

    }
}