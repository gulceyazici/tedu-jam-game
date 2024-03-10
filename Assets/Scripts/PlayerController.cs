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
    private float playerShipXSpeed;
    [SerializeField]
    private float rotationSpeed;
    //[SerializeField] private AudioSource thrusterAudio;
    [SerializeField] private ParticleSystem thrusterParticlesLeft;
    [SerializeField] private ParticleSystem thrusterParticlesRight;


    public float verticalInput;
    public float horizontalInput;

    public Rigidbody rb;

    private float maxRotateAngle = 45.0f;

    private float xRange = 300.0f;
    private float yRange = 100.0f;

    private float budget = 0f;

    // Start is called before the first frame update
    void Start()
    {
    }
    
    private void Update()
    {
        PlaySoundEffects();
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

        var velX = rb.velocity.x;
        var velY = rb.velocity.y;

        GetComponent<Rigidbody>().AddForce(verticalInput * playerShipSpeed * Time.deltaTime *Vector3.up , ForceMode.Impulse);

        if(transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.y > yRange)
        {
            transform.position = new Vector3(transform.position.x, yRange, transform.position.z);
        }
        if (transform.position.y < -yRange)
        {
            transform.position = new Vector3(transform.position.x, -yRange, transform.position.z);
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
        GetComponent<Rigidbody>().AddForce(horizontalInput * playerShipXSpeed * Time.deltaTime* Vector3.right  , ForceMode.Impulse);
    }

    private void PlaySoundEffects()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {

            thrusterParticlesLeft.Play();
            thrusterParticlesRight.Play();
            // Check if thrusterAudio is assigned and not null
            //if (thrusterAudio != null && thrusterAudio.isPlaying == false)
            //{
            //    // Play the audio
            //    thrusterAudio.Play();

            //    Debug.Log("threuster is playing?: " + thrusterParticlesLeft.isPlaying);

            //}
            //else
            //{
            //    Debug.LogWarning("thrusterAudio is not assigned!");
            //}
        }

        // Check if the W key is released
        //if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        //{

        //    thrusterParticlesLeft.Stop();
        //    thrusterParticlesRight.Stop();
        //    // Check if thrusterAudio is playing
        //    if (thrusterAudio != null && thrusterAudio.isPlaying)
        //    {
        //        // Stop the audio
        //        thrusterAudio.Stop();

        //    }
        //}
    }
    public float GetBudget()
    {
        return this.budget;
    }

    public void SetBudget(float money)
    {
        this.budget = money;
    }
}
