using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UINavigation : MonoBehaviour
{

    [SerializeField] AudioClip clickSound;
    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = FindObjectOfType<AudioSource>();
    }
    public void MainMenu()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartLevel()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {

        Time.timeScale = 1;
        if (SceneManager.GetActiveScene().buildIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {

            MainMenu();
        }


    }


    public void MaxLevel()
    {

        Time.timeScale = 1;

        if (!(SceneManager.sceneCountInBuildSettings <= PlayerPrefs.GetInt("MaxReachedLevel", 1)))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("UnlockedLevel", 1));
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("UnlockedLevel", 1) - 1);
        }

    }

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}
