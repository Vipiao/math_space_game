using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float acceleration;
    public float angularAcceleration;

    public GameObject shootObject;

    float nextShootTime = 0;

    private void FixedUpdate()
    {
        float horizontal;
        if (Input.GetKey("right"))
        {
            horizontal = 1.0f;
        }
        else if (Input.GetKey("left"))
        {
            horizontal = -1.0f;
        }
        else
        {
            horizontal = 0;
        }
        float vertical;
        if (Input.GetKey("up"))
        {
            vertical = 1.0f;
        }
        else if (Input.GetKey("down"))
        {
            vertical = -1.0f;
        }
        else
        {
            vertical = 0;
        }
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.angularVelocity += new Vector3(0, horizontal, 0) * angularAcceleration * Time.deltaTime;
        rigidBody.velocity += rigidBody.transform.forward * vertical * acceleration * Time.deltaTime;
        // Tilt effect due to turn.
        rigidBody.angularVelocity +=
            -rigidBody.transform.forward * horizontal * angularAcceleration * Time.deltaTime;
        // Straight up orientation.
        Quaternion rotation = Quaternion.FromToRotation(
            transform.up,
            Vector3.Lerp(transform.up, Vector3.up, 0.1f)
        );
        transform.rotation = rotation * transform.rotation;
        // Stop y velocity.
        rigidBody.position = new Vector3(rigidBody.position.x, 0, rigidBody.position.z);

        // Shoot.
        if (Input.GetKey("space") && Time.time >= nextShootTime)
        {
            nextShootTime = Time.time + 0.2f; // 0.5 seconds.
            GameObject bolt = Object.Instantiate(
                shootObject,
                rigidBody.position,
                rigidBody.rotation
                );
            float shootSpeed = 20.0f;
            Rigidbody boltRigidBody = bolt.GetComponent<Rigidbody>();
            boltRigidBody.velocity =
                rigidBody.velocity + rigidBody.transform.forward * shootSpeed;
            boltRigidBody.velocity = new Vector3(boltRigidBody.velocity.x, 0, boltRigidBody.velocity.z);
        }
        //Debug.Log(rigidBody.position.x);

        // Boundaries.
        Vector3 lowerLeft;
        Vector3 upperRight;
        Utilities.getBoundaries(out lowerLeft, out upperRight);
        //Debug.Log("rigidBody.position" + rigidBody.position);
        rigidBody.position = Utilities.limitWithinBounds(rigidBody.position);
    }
}
