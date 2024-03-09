using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [SerializeField]
        private GameObject inspectionChoicePanel; // Assign this in the Unity Inspector


        private void Awake()
        {
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

        public void PlayerNearAsteroid(bool isNear, GameObject asteroid)
        {
            if (isNear)
            {
                // Prompt UI to ask player if they want to inspect the asteroid
                // This can be a simple UI panel with buttons
                // You might use events or callbacks to handle the player's choice
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
            SceneManager.LoadScene(1);
        }

        public void OnCancelButtonPressed()
        {
            Debug.Log("Cancel button pressed.");
            // Hide the inspection choice UI
            HideInspectionChoiceUI();
        }

    }
}
