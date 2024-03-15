using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // private float MAX_SPEED = 1f;
    // private float MIN_SPEED = -0.5f;
    private float MAX_TURN_SPEED = 720f;
    private float MIN_TURN_SPEED = -720f;

    private float MOVE_ACCELLERATION = 0.05f;
    private float MOVE_DECAY = 0.8f;
    private float TURN_ACCELLERATION = 2160f;
    private float TURN_DECAY = 0.95f;

    private Vector3 velocity;
    private float turn_speed;
    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // key D accellerate rotation right
        if (Input.GetKey(KeyCode.D))
        {
            turn_speed -= TURN_ACCELLERATION*Time.deltaTime;
        }
        // key A accellerate rotation left
        if (Input.GetKey(KeyCode.A))
        {
            turn_speed += TURN_ACCELLERATION*Time.deltaTime;
        }
        // key W add forward to velocity
        if (Input.GetKey(KeyCode.W))
        {
            velocity += MOVE_ACCELLERATION*Time.deltaTime*Vector3.up;
        }
        
        // clamp turn speed
        turn_speed = Mathf.Clamp(turn_speed, MIN_TURN_SPEED, MAX_TURN_SPEED);
        
        // decelerate turn speed
        turn_speed = turn_speed * Mathf.Pow(1-TURN_DECAY, Time.deltaTime);

        // decelerate velocity
        velocity = velocity * Mathf.Pow(1-MOVE_DECAY, Time.deltaTime);

        // turn player
        transform.Rotate(0, 0, turn_speed*Time.deltaTime);

        // move player
        transform.Translate(velocity);
    }
}
