using Assets.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private GameObject inspectionChoicePanel; // Assign this in the Unity Inspector
        [SerializeField]
        private AudioSource outerSpaceAudioSource; // Assign in the Inspector
        [SerializeField]
        private AudioSource enemyEncounterAudioSource; // Assign in the Inspector

        private AsteroidMiningSite asteroidMiningSite;
        private PlayerController playerController;

        public AudioSource collectMineAudio;
        public ParticleSystem collectMineParticle;

        public List<AsteroidMiningSite> asteroidMiningSites;

        public GameObject enemy;
        private EnemyDamageHandler enemyDamageHandler;

        public GameObject player;

        public static float quota { get; private set; } = 1000f;

        void Start()
        {

        }
        private void Awake()
        {
            enemyDamageHandler = enemy.GetComponent<EnemyDamageHandler>();
            
            player = GameObject.FindWithTag("Player");
            playerController = player.GetComponent<PlayerController>();

            // Subscribe to the enemy died event
            enemyDamageHandler.EnemyDied += OnEnemyDied;

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            PlayOuterSpaceMusic();
        }

        // Method to play outer space music
        public void PlayOuterSpaceMusic()
        {
            if (!outerSpaceAudioSource.isPlaying)
            {
                outerSpaceAudioSource.Play();
                // Start coroutine to crossfade from enemyEncounter to outerSpace
                StartCoroutine(CrossfadeAudioSources(enemyEncounterAudioSource, outerSpaceAudioSource, 1f)); // 1f is the fade duration, change as needed
            }
        }

        // Method to play enemy encounter music
        public void PlayEnemyEncounterMusic()
        {
            if (!enemyEncounterAudioSource.isPlaying)
            {
                enemyEncounterAudioSource.Play();
                // Start coroutine to crossfade from outerSpace to enemyEncounter
                StartCoroutine(CrossfadeAudioSources(outerSpaceAudioSource, enemyEncounterAudioSource, 1f)); // 1f is the fade duration, change as needed
            }
        }


        public void PlayerNearAsteroid(bool isNear, bool hasInspected, GameObject asteroid)
        {

            if (isNear && hasInspected == false)
            {
                // Prompt UI to ask player if they want to inspect the asteroid
                // This can be a simple UI panel with buttons
                // You might use events or callbacks to handle the player's choice
                asteroidMiningSite = asteroid.GetComponent<AsteroidMiningSite>();
                Debug.Log("PLAYER IS NEAR");
                ShowInspectionChoiceUI(asteroid);
            }
            else
            {
                // Hide the prompt UI if it's visible
                HideInspectionChoiceUI();
            }
        }

        void ShowInspectionChoiceUI(GameObject asteroid)
        {
            // Implementation depends on your UI setup
            inspectionChoicePanel.SetActive(true);

            // Upon player decision, determine asteroid properties (enemy presence, mine type, etc.)
            // and load the appropriate scene or show the appropriate message
        }

        void HideInspectionChoiceUI()
        {
            // Hide or disable the inspection choice UI
            inspectionChoicePanel.SetActive(false);
        }

        public void OnInspectButtonPressed()
        {
            Debug.Log("Inspect button pressed.");
            // Add your inspection logic here
            // E.g., load a new scene, display enemy encounter, etc.
            Debug.Log("near asteroid mine: " + asteroidMiningSite.asteroidMine.ToString());


            if (playerController != null && asteroidMiningSite.asteroidMine != AsteroidMines.None && asteroidMiningSite.hasInspected == false)
            {

                collectMineAudio.Play();
                collectMineParticle.Play();
                playerController.SetBudget(playerController.GetBudget() + (float)asteroidMiningSite.asteroidMine);
                asteroidMiningSite.hasInspected = true;
                HideInspectionChoiceUI();
            }
            
            else if(asteroidMiningSite.asteroidHasEnemy == true)
            {
                asteroidMiningSite.hasInspected = true;
                HideInspectionChoiceUI();
                
                foreach (AsteroidMiningSite asteroidMSite in asteroidMiningSites)
                {
                    asteroidMSite.gameObject.SetActive(false);
                }
                
                enemy.SetActive(true);

                PlayEnemyEncounterMusic();

            }
            asteroidMiningSite.hasInspected = true;
            HideInspectionChoiceUI();
        }

        private IEnumerator CrossfadeAudioSources(AudioSource fadeOutAudio, AudioSource fadeInAudio, float duration)
        {
            float currentTime = 0;

            // Get the current volume of the soundtracks to handle cases where they're not at max volume
            float startFadeOutVolume = fadeOutAudio.volume;
            float startFadeInVolume = fadeInAudio.volume;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                fadeOutAudio.volume = Mathf.Lerp(startFadeOutVolume, 0f, currentTime / duration);
                fadeInAudio.volume = Mathf.Lerp(startFadeInVolume, 1f, currentTime / duration);

                yield return null; // Wait for next frame
            }

            fadeOutAudio.Stop();
            fadeOutAudio.volume = startFadeOutVolume; // Optionally reset the fadeOutAudio volume back to its original level if needed
        }


        public void OnCancelButtonPressed()
        {
            Debug.Log("Cancel button pressed.");
            // Hide the inspection choice UI
            HideInspectionChoiceUI();
        }

        private void OnDestroy()
        {
            // Unsubscribe from the enemy died event
            enemyDamageHandler.EnemyDied -= OnEnemyDied;
        }

        // This method will be called when the enemy dies
        private void OnEnemyDied()
        {
            // Hide the enemy if it dies after inspection button is clicked
            enemy.SetActive(false);

            // Reactivate each asteroid
            foreach(AsteroidMiningSite asteroid in asteroidMiningSites)
            {
                asteroid.gameObject.SetActive(true);
            }
            enemyDamageHandler.health = 5;
            Debug.Log("resetting enemy health, health: " + enemyDamageHandler.health);
            PlayOuterSpaceMusic();
        }


    }
}
