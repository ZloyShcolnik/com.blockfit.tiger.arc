using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] GameObject cellsObject;
    private Cell[] cells;
    public static LevelController instance;
    private int cellsCount;
    [HideInInspector] public int filledCells;
    [SerializeField] private TMP_Text timeText;
    [SerializeField] GameObject winMenu;
    [SerializeField] GameObject loseMenu;

    private bool isLevelEnded;

    public float levelSeconds;
    private float timeRemaining;

    public AudioClip pickUpSound;
    public AudioClip dropSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] AudioClip loseSound;
    

    public AudioSource audioSource;

    private void Awake()
    {
        
        instance = this;

        audioSource = GameObject.Find("SingleSoundManager").GetComponent<AudioSource>();
        cells = cellsObject.GetComponentsInChildren<Cell>();
        cellsCount = cells.Length;
        timeRemaining = levelSeconds;
        
        Camera mainCamera = Camera.main;

        // Проверяем, что камера существует
        if (mainCamera != null)
        {
            // HEX-код цвета
            string hexColor = "#933B3B";

            // Преобразуем HEX в Color
            if (ColorUtility.TryParseHtmlString(hexColor, out Color color))
            {
                // Устанавливаем цвет фона камеры
                mainCamera.backgroundColor = color;
            }
            else
            {
                Debug.LogError("Неверный формат HEX-кода");
            }
        }
    }
    void Update()
    {
        if(timeRemaining >= 0)
        {
            timeRemaining -= Time.deltaTime;
        }
        

        if(filledCells >= cellsCount && !isLevelEnded)
        {
            Win();
        }
        if(timeRemaining <= 0 && !isLevelEnded)
        {
            Lose();
        }
    }

    private void OnGUI()
    {
        timeText.text = ((int)timeRemaining / 60).ToString("00") + ":" + ((int)timeRemaining % 60).ToString("00");
    }

    private void Win()
    {
        audioSource.PlayOneShot(winSound);
        TimeScore.SaveTime(timeRemaining);
        UnlockNextLevel();
        winMenu.transform.parent.gameObject.SetActive(true);
        winMenu.SetActive(true);
        isLevelEnded = true;
    }

    private void Lose()
    {
        audioSource.PlayOneShot(loseSound);
        loseMenu.transform.parent.gameObject.SetActive(true);
        loseMenu.SetActive(true);
        isLevelEnded = true;
    }

    private void UnlockNextLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("MaxReachedLevel"))
        {
            PlayerPrefs.SetInt("MaxReachedLevel", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }

    }
}
