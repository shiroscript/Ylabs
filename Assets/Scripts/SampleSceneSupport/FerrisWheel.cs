using Lightbug.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FerrisWheel : MonoBehaviour
{
    public Transform wheel;
    public Vector3 direction = Vector3.up;
    public Transform[] seats;
    public float speed = 3f;

    Rigidbody rbWheel;
    Rigidbody[] rbSeats;

    private void Awake()
    {
        rbWheel = wheel.GetComponent<Rigidbody>();
        rbSeats = new Rigidbody[seats.Length];
        for(int i = 0; i < seats.Length; i++)
        {
            Rigidbody rb = seats[i].gameObject.GetOrAddComponent<Rigidbody>();
            rbSeats[i] = rb;
            rb.mass = 1000;
            rb.isKinematic = true;
            rb.useGravity = false;
            rb.drag = 0;
            rb.angularDrag = 0;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Discrete;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void FixedUpdate()
    {
        Quaternion rbRotation = rbWheel.rotation;
        rbRotation *= Quaternion.AngleAxis(Time.deltaTime * speed, direction);

        rbWheel.MoveRotation(rbRotation);
        foreach (var seat in rbSeats)
        {
            seat.MoveRotation(Quaternion.Euler(new Vector3(0, 90 + 41.192f, 0)));
        }
    }
}
