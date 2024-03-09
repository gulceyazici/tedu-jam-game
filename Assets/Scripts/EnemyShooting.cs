using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletOffset1;
    public GameObject bulletOffset2;
    [SerializeField] public GameObject bulletPrefab1;
    [SerializeField] public GameObject bulletPrefab2;
    int bulletLayer;

    public float fireDelay = 0.50f;
    float cooldownTimer = 0;

    void Start()
    {
        bulletLayer = gameObject.layer;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0)
        {
            // SHOOT!
            cooldownTimer = fireDelay;

            GameObject bulletGO1 = (GameObject)Instantiate(bulletPrefab1);
            Debug.Log("before destroy x: " + bulletGO1.transform.position.x);
            bulletGO1.transform.position = bulletOffset1.transform.position;
            GameObject bulletGO2 = (GameObject)Instantiate(bulletPrefab2);
            bulletGO2.transform.position = bulletOffset2.transform.position;

            bulletGO1.layer = bulletLayer;
            bulletGO2.layer = bulletLayer;
        }
    }
}
