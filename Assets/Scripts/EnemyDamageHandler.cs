using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageHandler : MonoBehaviour
{
    public int health = 5;
    public GameObject explosion;

    int correctLayer;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
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
    void Die()
    {
        explosion.SetActive(true);

        // Delay the destruction of the GameObject by 1 second
        Invoke("DestroyEnemy", 0.6f);
    }

    void DestroyEnemy()
    {
        // Ensure to cancel any Invoke calls to prevent unexpected behaviors
        CancelInvoke();

        // Destroy the GameObject
        Destroy(gameObject);
    }
}
