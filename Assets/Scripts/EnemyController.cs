using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform playerTransform; // The player's transform
    public Vector3 offset; // Offset distance between the player and camera
    public GameObject player;
    public float followSpeed = 2f; // Speed at which the camera/enemy follows the player
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
            Vector3 targetPosition = playerTransform.position + offset;
            // Use Lerp to smoothly interpolate between the current position and the target position
            // Time.deltaTime ensures that the movement is smooth and frame rate independent
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        }
    }
}
