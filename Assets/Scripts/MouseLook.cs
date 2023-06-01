using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MouseLook rotates the transform based on the mouse delta.
// To make an FPS style character:
// - Create a capsule.
// - Add the MouseLook script to the capsule.
//   -> Set the mouse look to use MouseX. (You want to only turn character but not tilt it)
// - Add FPSInput script to the capsule
//   -> A CharacterController component will be automatically added.
//
// - Create a camera. Make the camera a child of the capsule. Position in the head and reset the rotation.
// - Add a MouseLook script to the camera.
//   -> Set the mouse look to use MouseY. (You want the camera to tilt up and down like a head. The character already turns.)

[AddComponentMenu("Control Script/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    // Define an enum data structure to associate names with settings.
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    // Declare a public variable to set in Unity’s editor.
    public RotationAxes axes = RotationAxes.MouseXAndY;

    // Declare variables used for vertical rotation
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    // Declare a private variable for the vertical angle.
    private float verticalRot = 0;

    void Start()
    {
        // Make the rigid body not change rotation
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }
    }

    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            // horizontal rotation here
            // Note the use of GetAxis() to get mouse input.
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            // vertical rotation here
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);
            // Keep the same Y angle (i.e., no horizontal rotation).
            float horizontalRot = transform.localEulerAngles.y;

            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
        else
        {
            // both horizontal and vertical rotation here
            // Increment the vertical angle based on the mouse.
            verticalRot -= Input.GetAxis("Mouse Y") * sensitivityVert;
            // Clamp the vertical angle between minimum and maximum limits.
            verticalRot = Mathf.Clamp(verticalRot, minimumVert, maximumVert);
            // delta is the amount to change the rotation by
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            // Increment the rotation angle by delta.
            float horizontalRot = transform.localEulerAngles.y + delta;
            // Create a new vector from the stored rotation values.
            transform.localEulerAngles = new Vector3(verticalRot, horizontalRot, 0);
        }
    }
}