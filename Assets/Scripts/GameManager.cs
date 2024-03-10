using Assets.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        private AsteroidMiningSite asteroidMiningSite;
        private PlayerController playerController;

        public AudioSource collectMineAudio;
        public ParticleSystem collectMineParticle;


        public GameObject player;

        void Start()
        {

        }
        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            playerController = player.GetComponent<PlayerController>();

            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void PlayerNearAsteroid(bool isNear, bool hasInspected,GameObject asteroid)
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



            Debug.Log(playerController);

            if (playerController != null && asteroidMiningSite.asteroidMine != AsteroidMines.None && asteroidMiningSite.hasInspected == false)
            {

                collectMineAudio.Play();
                collectMineParticle.Play();
                playerController.SetBudget(playerController.GetBudget() + (float)asteroidMiningSite.asteroidMine);
                asteroidMiningSite.hasInspected = true;
                HideInspectionChoiceUI();
            }
            else
            {
                Debug.Log("player controller is null or asteroid doesn't has a mine");

            }
            
        }

        public void OnCancelButtonPressed()
        {
            Debug.Log("Cancel button pressed.");
            // Hide the inspection choice UI
            HideInspectionChoiceUI();
        }

    }
}
