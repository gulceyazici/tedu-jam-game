using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    public Transform playerTransform; // Assign this in the inspector
    private Quaternion initialRotation;

    [SerializeField]
    private float smoothSpeed = 0.125f;

    [SerializeField]
    public Vector3 offset;

    void Start()
    {
        // Capture the initial rotation of the camera at the start of the scene
        initialRotation = transform.rotation;
    }

    void FixedUpdate()
    {
        if (playerTransform != null)
        {
            //Vector3 targetPosition = playerTransform.position + initialRotation * Vector3.back * 50 + Vector3.up * 10 + offset;

            Vector3 targetPosition = playerTransform.position + offset; 

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            transform.position = smoothedPosition;

            transform.rotation = initialRotation;
        }
    }
}
