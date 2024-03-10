using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidProximityDetector : MonoBehaviour
{
    private AsteroidMiningSite miningSite;

    private void Start()
    {
        miningSite = gameObject.GetComponent<AsteroidMiningSite>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            GameManager.Instance.PlayerNearAsteroid(true, miningSite.hasInspected,this.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.PlayerNearAsteroid(false, miningSite.hasInspected,this.gameObject);
        }
    }
}

