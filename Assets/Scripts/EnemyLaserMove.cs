using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserMove : MonoBehaviour
{
    public float maxSpeed = 20f;
    public float force = 2f;

    // Update is called once per frame
    private void Start()
    {

    }
    void Update()
    {
        Vector3 position = transform.position;
        position = new Vector3(position.x, position.y - maxSpeed * Time.deltaTime, position.z);
        transform.position = position;

    }
}
