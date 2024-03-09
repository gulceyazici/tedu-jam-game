using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageHandler : MonoBehaviour
{
    public int health = 10;
    int correctLayer;
    public GameObject explosion;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
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
        Invoke("DestroyPlayer", 0.6f);
    }

    void DestroyPlayer()
    {
        // Ensure to cancel any Invoke calls to prevent unexpected behaviors
        CancelInvoke();

        // Destroy the GameObject
        Destroy(gameObject);
    }
}
