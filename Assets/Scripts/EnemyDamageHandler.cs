using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyDamageHandler : MonoBehaviour
{
    public int health = 5;
    public GameObject explosion;

    public event Action EnemyDied;

    int correctLayer;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Laser"))
        {
            health--;
        }
    }
    void Start()
    {
        correctLayer = gameObject.layer;


    }
    private void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        explosion.SetActive(true);

        Invoke("DestroyEnemy", 0.6f);


    }

    public void DestroyEnemy()
    {
        CancelInvoke();
        gameObject.SetActive(false); // Deactivate the enemy

        // Broadcast the enemy died event
        EnemyDied?.Invoke();
        explosion.SetActive(false);
    }

}


