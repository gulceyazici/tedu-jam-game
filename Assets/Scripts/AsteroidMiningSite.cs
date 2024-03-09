using Assets.Enums;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidMiningSite : MonoBehaviour
{
    public bool asteroidHasMine;
    public bool asteroidHasEnemy;
    public int minePresenceProbability = 40;
    public int enemyPresenceProbability = 50;
    public AsteroidMines asteroidMine;

    private List<(AsteroidMines, float)> minesWithProbabilities = new List<(AsteroidMines, float)>
    {
        (AsteroidMines.Gold, 0.3f),
        (AsteroidMines.Uranium, 0.25f),
        (AsteroidMines.Platinum, 0.15f),
        (AsteroidMines.Diamond, 0.1f),
        (AsteroidMines.Neutronium, 0.08f),
        (AsteroidMines.Antimatter, 0.06f),
        (AsteroidMines.Tachyonite, 0.04f),
        (AsteroidMines.DarkMatter, 0.02f),
    };

    void Start()
    {
        EnemyPresence();

        if (!asteroidHasEnemy)
        {
            MinePresence();
        }
        else
        {
            //SceneManager.LoadScene(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyPresence()
    {
        int diceEnemy = Random.Range(0, 101);
        Debug.Log("Random number for enemy determination: " + diceEnemy);
        asteroidHasEnemy = diceEnemy <= enemyPresenceProbability;
        Debug.Log("Asteroid has enemy: " + asteroidHasMine);
    }

    private void MinePresence()
    {
        int diceMine = Random.Range(0, 101);
        Debug.Log("Random number for mine determination: " + diceMine);
        asteroidHasMine = diceMine <= minePresenceProbability;
        Debug.Log("Asteroid has mine: " + asteroidHasMine);

        if (asteroidHasMine)
        {
            asteroidMine = SelectMineRandomly();
            Debug.Log("Asteroid has: " + asteroidMine.ToString());
        }
    }

    private AsteroidMines SelectMineRandomly()
    {
        float total = 0;
        foreach (var mine in minesWithProbabilities)
        {
            total += mine.Item2; // Sum up the total probability
        }

        float randomPoint = UnityEngine.Random.value * total; // Get a random value between 0 and the total probability

        // Determine which mine this random value falls into
        float cumulativeProbability = 0;
        foreach (var mine in minesWithProbabilities)
        {
            cumulativeProbability += mine.Item2;
            if (randomPoint <= cumulativeProbability)
            {
                Debug.Log("randomPoint: " + randomPoint + " --- CumulativeProb: " + cumulativeProbability);
                return mine.Item1;
            }
        }

        return AsteroidMines.None;
    }

}
