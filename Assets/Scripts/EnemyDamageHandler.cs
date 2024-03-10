using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyDamageHandler : MonoBehaviour
{
    public int health = 5;
    public GameObject explosion;

    public event Action EnemyDied;

    [SerializeField]
    private AudioSource enemyDamageReceiveAudioSource; // Assign in the Inspector

    [SerializeField]
    private AudioSource enemyDeadAudioSource; // Assign in the Inspector

    int correctLayer;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player Laser"))
        {
            health--;
            Debug.Log("enemy shot, health: " + health);
            enemyDamageReceiveAudioSource.Play();
        }

        if(health <= 0)
        {
            Die();
        }
    }
    void Start()
    {
        correctLayer = gameObject.layer;
    }
    private void Update()
    {
        //if (health <= 0)
        //{
        //    Die();
        //}
    }
    public void Die()
    {
        explosion.SetActive(true);
        
        enemyDeadAudioSource.Play();

        Invoke("DestroyEnemy", enemyDeadAudioSource.clip.length);
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


