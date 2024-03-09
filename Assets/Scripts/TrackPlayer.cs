using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{

    public Transform playerTransform; // The player's transform
    public Vector3 offset; // Offset distance between the player and camera
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate()
    {
        // Check if the playerTransform is not null to avoid errors
        if (playerTransform != null)
        {
            // Set the camera's position to the player's position with the offset
            transform.position = playerTransform.position + offset;
        }
    }

}
