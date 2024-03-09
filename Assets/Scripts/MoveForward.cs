using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float maxSpeed = 5f;
    public float force = 2f;

    // Update is called once per frame
    private void Start()
    {
        
    }
    void Update()
    {
      Vector2 position = transform.position;
        position = new Vector2(position.x, position.y + maxSpeed*Time.deltaTime);
        transform.position = position;  

    }
}
