using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float playerShipSpeed;
    [SerializeField]
    private float rotationSpeed;
    
    public float verticalInput;
    public float horizontalInput;

    public Rigidbody rb;

    private float maxRotateAngle = 45.0f;

    private float xRange = 100.0f;

    private float budget = 0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveShip();
        RotateShip();
    }

    private void MoveShip()
    {
        verticalInput = UnityEngine.Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().AddForce(verticalInput * playerShipSpeed * Time.deltaTime *Vector3.up , ForceMode.Impulse);

        if(transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
    }

    private void RotateShip()
    {
        horizontalInput = UnityEngine.Input.GetAxis("Horizontal");
        transform.Rotate(rotationSpeed * Time.deltaTime * -horizontalInput * Vector3.forward );

        float currentRotationY = transform.rotation.eulerAngles.y;
        float currentRotationX = transform.rotation.eulerAngles.x;
        float currentRotationZ = transform.rotation.eulerAngles.z;

        if (currentRotationY <= 315.0f && currentRotationY >= 310.0f)
        {
            currentRotationY = 315.0f;
        }

        else if (currentRotationY <= 50.0f && currentRotationY >= 45)
        {
            currentRotationY = maxRotateAngle;
        }

        transform.rotation = Quaternion.Euler(currentRotationX, currentRotationY, currentRotationZ);

        if (transform.position.x >= xRange)
        {
            GetComponent<Rigidbody>().AddForce(-1 * playerShipSpeed * Time.deltaTime * Vector3.right , ForceMode.Impulse);
        }
        if (transform.position.x <= -xRange)
        {
            GetComponent<Rigidbody>().AddForce(1 * playerShipSpeed * Time.deltaTime* Vector3.right, ForceMode.Impulse);
        }
        GetComponent<Rigidbody>().AddForce(horizontalInput * playerShipSpeed * Time.deltaTime* Vector3.right  , ForceMode.Impulse);
    }

    public float GetBudget()
    {
        return this.budget;
    }

}
