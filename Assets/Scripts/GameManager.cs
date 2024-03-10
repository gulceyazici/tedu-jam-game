using Assets.Enums;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
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
        private GameObject mineEncounterPanel; // Assign this in the Unity Inspector
        [SerializeField]
        private GameObject enemyEncounterPanel; // Assign this in the Unity Inspector
        [SerializeField]
        private AudioSource outerSpaceAudioSource; // Assign in the Inspector
        [SerializeField]
        public AudioSource enemyEncounterAudioSource; // Assign in the Inspector
        [SerializeField]
        private AudioSource playerReachQuotaAudioSource; // Assign in the Inspector

        private AsteroidMiningSite asteroidMiningSite;
        private PlayerController playerController;

        public TextMeshProUGUI mineInfoText; // Assign this in the inspector

        public AudioSource collectMineAudio;
        public ParticleSystem collectMineParticle;

        public List<AsteroidMiningSite> asteroidMiningSites = new List<AsteroidMiningSite>();


        public GameObject enemy;
        private EnemyDamageHandler enemyDamageHandler;

        public string dataToPass;

        public GameObject player;

        public string encounterEnemyMessage = "Oh no! A rival treasure hunter appeared!!";

        public string encounterMineMessage = "You have found";

        public static float quota { get; private set; } = 1000f;

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
                //DontDestroyOnLoad(gameObject);
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

        private IEnumerator FadeOutAndPlayQuotaReached(AudioSource fadeOutAudio, AudioSource quotaReachedAudio, float fadeDuration)
        {
            // Fade out the current audio
            yield return StartCoroutine(FadeOutAudioSource(fadeOutAudio, fadeDuration));

            // Ensure fadeOutAudio is completely stopped before starting the next audio
            fadeOutAudio.Stop();

            // Play the quota reached audio and wait for it to finish
            quotaReachedAudio.Play();
            yield return new WaitForSeconds(quotaReachedAudio.clip.length);

            // Now that the quota audio has finished, load the new scene
            SceneManager.LoadScene(3);
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

                if (playerController.GetBudget() >= quota)
                {
                    // Fade out the outer space audio before playing the quota reach sound
                    dataToPass = "Congratulations you are the ruler of the outers !";
                    StartCoroutine(FadeOutAndPlayQuotaReached(outerSpaceAudioSource, playerReachQuotaAudioSource, 1f)); // 1f is the fade duration, adjust as needed
                }
                else
                {
                    mineInfoText.text = encounterMineMessage + " " + asteroidMiningSite.asteroidMine.ToString() + " !! You have earned $" + (float)asteroidMiningSite.asteroidMine;
                    mineEncounterPanel.SetActive(true);

                    Invoke("ExitMineEncounterPanel", 2.3f);
                }

                asteroidMiningSite.hasInspected = true;
                HideInspectionChoiceUI();
            }
            
            else if(asteroidMiningSite.asteroidHasEnemy == true)
            {
                asteroidMiningSite.hasInspected = true;
                HideInspectionChoiceUI();

                enemyEncounterPanel.SetActive(true);

                Invoke("ExitEnemyEncounterPanel", 1.5f);

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

        private void ExitMineEncounterPanel()
        {
            CancelInvoke("ExitMineEncounterPanel");

            mineEncounterPanel.SetActive(false);
        }

        private void ExitEnemyEncounterPanel()
        {
            CancelInvoke("ExitEnemyEncounterPanel");

            enemyEncounterPanel.SetActive(false);
        }

        private IEnumerator FadeOutAudioSource(AudioSource audioSource, float fadeDuration)
        {
            float startVolume = audioSource.volume;

            while (audioSource.volume > 0)
            {
                audioSource.volume -= startVolume * Time.deltaTime / fadeDuration;
                yield return null;
            }

            audioSource.Stop();
            audioSource.volume = startVolume;
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
        // Method to add spawned asteroids to the list
        public void AddAsteroidToMiningSites(AsteroidMiningSite asteroid)
        {
            if (!asteroidMiningSites.Contains(asteroid))
            {
                asteroidMiningSites.Add(asteroid);
            }
        }


    }
}
