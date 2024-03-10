using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    public GameObject[] astreoidPrefabs;
    private GameObject player;
    public GameManager gameManager;

    private float spawnRangeX = 200.0f;
    private float spawnPosY;
    private float spawnDelay = 0.0f;
    private float spawnRepeat = 7.5f;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        player = GameObject.FindWithTag("Player");
        spawnPosY = player.transform.position.y + 250.0f;

        InvokeRepeating("SpawnRandomAsteroid", spawnDelay, spawnRepeat);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnRandomAsteroid()
    {
        int asteroidIndex = Random.Range(0, astreoidPrefabs.Length);
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), spawnPosY, 9500);
        GameObject newAsteroid = Instantiate(astreoidPrefabs[asteroidIndex], spawnPos, astreoidPrefabs[asteroidIndex].transform.rotation);
        
        AsteroidMiningSite asteroidMiningSite = newAsteroid.GetComponent<AsteroidMiningSite>();

        if (asteroidMiningSite != null && gameManager != null)
        {
            gameManager.AddAsteroidToMiningSites(asteroidMiningSite);
        }
    }
}
