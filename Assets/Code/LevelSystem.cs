using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public int levelId;

    private Button levelButton;
    [SerializeField] TMP_Text levelNumberText;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        levelButton = GetComponent<Button>();
        levelNumberText.text = levelId.ToString();

        if (levelId <= unlockedLevel)
        {
            levelButton.interactable = true;


        }
        else
        {
            levelButton.interactable = false;
        }


    }

    public void OpenLevel()
    {
        Time.timeScale = 1;
        string levelName = "Level " + levelId.ToString();
        SceneManager.LoadScene(levelName);
    }
}
