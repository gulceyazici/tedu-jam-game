using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDamageHandler : MonoBehaviour
{
    public int health = 10;
    int correctLayer;
    public GameObject explosion;
    // Start is called before the first frame update
    public HealthDisplay playerHpController;

    [SerializeField]
    private AudioSource playerDamageReceiveAudioSource; // Assign in the Inspector

    [SerializeField]
    private AudioSource playerDeadAudioSource; // Assign in the Inspector

    private bool isDying = false; // Flag to indicate the death sequence has started

    public string dataToPass;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy Laser"))
        {
            health--;
            
            if(health > 0)
            playerDamageReceiveAudioSource.Play();
            
            
            playerHpController.UpdateHealth(health);

            if (health <= 0 && !isDying)
            {
                isDying = true; // Set the flag to true to prevent multiple triggers
                GameManager.Instance.dataToPass = "Game Over !";
                GameManager.Instance.enemyEncounterAudioSource.Stop();
                playerDeadAudioSource.Play();
                Die();
            }

        }
    }

    void Die()
    {
        explosion.SetActive(true);
        // Wait for the death audio to finish before destroying the player
        StartCoroutine(DestroyPlayerAfterSound(playerDeadAudioSource.clip.length));
    }

    IEnumerator DestroyPlayerAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(2);
        Destroy(gameObject);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if(other.CompareTag("Enemy Laser"))
    //    {
    //        health--;
    //        playerDamageReceiveAudioSource.Play();
    //        playerHpController.UpdateHealth(health);
    //    }
    //}
    //void Start()
    //{
    //    correctLayer = gameObject.layer;


    //}
    //private void Update()
    //{
    //    if (health <= 0)
    //    {
    //        Die();
    //    }
    //}
    //void Die()
    //{
    //    explosion.SetActive(true);
    //    // Play the player death audio
    //    playerDeadAudioSource.Play();
    //    // Start a coroutine to wait for the audio to finish before destroying the player
    //    StartCoroutine(DestroyPlayerAfterSound(playerDeadAudioSource.clip.length));
    //}

    //IEnumerator DestroyPlayerAfterSound(float delay)
    //{
    //    // Wait for the length of the clip to play
    //    yield return new WaitForSeconds(delay);

    //    // Now destroy the player
    //    Destroy(gameObject);
    //}

}
