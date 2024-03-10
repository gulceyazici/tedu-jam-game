using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bulletOffset1;
    public GameObject bulletOffset2;

    [SerializeField] public GameObject bulletPrefab1;
    [SerializeField] public GameObject bulletPrefab2;

    [SerializeField] private AudioSource enemyShootingAudioSource; // Assign in the Inspector

    int bulletLayer;

    [SerializeField] public float fireDelay = 0.50f;
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
            bulletGO1.transform.position = bulletOffset1.transform.position;
            GameObject bulletGO2 = (GameObject)Instantiate(bulletPrefab2);

            enemyShootingAudioSource.Play();

            bulletGO2.transform.position = bulletOffset2.transform.position;

            bulletGO1.layer = bulletLayer;
            bulletGO2.layer = bulletLayer;
        }
    }
}
