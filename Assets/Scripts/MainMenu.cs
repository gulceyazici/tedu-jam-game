using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play : MonoBehaviour
{

    [SerializeField]
    private AudioSource mainMenuAudioSource; // Assign in the Inspector

    void Start()
    {
        mainMenuAudioSource.Play();
    }

    public void PlayGame()
    {
        mainMenuAudioSource.Stop();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // scene builder order (it is going to the next scene)
    }

    public void QuitMenu()
    {
        Application.Quit();
    }
}
