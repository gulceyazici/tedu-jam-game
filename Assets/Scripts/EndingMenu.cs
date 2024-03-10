using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingMenu : MonoBehaviour
{

    [SerializeField]
    private AudioSource endingMenuAudioSource; // Assign in the Inspector

    public TextMeshProUGUI endingMessage; // Assign this in the inspector

    void Start()
    {
        endingMenuAudioSource.Play();
        endingMessage.text = GameManager.Instance.dataToPass;
    }

    public void ReturnBackToMainMenu()
    {
        endingMenuAudioSource.Stop();
        SceneManager.LoadScene(0); // scene builder order (it is going to the next scene)
    }

    public void QuitMenu()
    {
        Debug.Log("ajksdalsýjdalýsd");
        Application.Quit();
    }
}
