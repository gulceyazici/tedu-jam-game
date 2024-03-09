using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    public Transform playerTransform; // Assign this in the inspector
    private Quaternion initialRotation;

    void Start()
    {
        // Capture the initial rotation of the camera at the start of the scene
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Follow the player's position
            transform.position = playerTransform.position + initialRotation * Vector3.back * 50 + Vector3.up * 5; // Example offset, adjust as needed

            // Keep the camera's original rotation constant
            transform.rotation = initialRotation;
        }
    }

}
