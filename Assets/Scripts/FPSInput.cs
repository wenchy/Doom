using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// basic WASD-style movement control
// commented out line demonstrates that transform.Translate instead of charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.8f;

    // Variable for referencing the CharacterController
    private CharacterController charController;

    void Start()
    {
        // Access other components attached to the same object.
        charController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // transform.Translate(Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0, Input.GetAxis("Vertical") * speed * Time.deltaTime);
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);
        // Limit diagonal movement to the same speed as movement along an axis.
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity;

        movement *= Time.deltaTime;
        // Transform the movement vector from local to global coordinates.
        movement = transform.TransformDirection(movement);
        charController.Move(movement);
    }
}
