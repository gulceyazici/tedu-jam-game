using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float maxSpeed = 5f;
    //public Rigidbody rb;
    public float force = 2f;

    // Update is called once per frame
    void Update()
    {
        //rb.AddForce(force*Time.deltaTime * Vector3.right, ForceMode.Impulse);
        //rb.AddForce(20 * Time.deltaTime * -Vector3.up, ForceMode.Impulse);
        Vector3 pos = transform.position;

        Vector3 velocity = new Vector3(0, maxSpeed * Time.deltaTime, 0);

        pos += velocity;

        transform.position = pos;
    }
}
