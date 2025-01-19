using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeScore : MonoBehaviour
{
    [SerializeField] GameObject timesObject;
    private TMP_Text[] timeTexts;

    private void Awake()
    {
        if(timesObject != null)
        {
            timeTexts = timesObject.GetComponentsInChildren<TMP_Text>();
        }
    }
    public static void SaveTime(float time)
    {
        for(int i = 0; i <= 4; i++)
        {
            if (time >= PlayerPrefs.GetFloat("SavedTime" + i, 0))
            {
                

                for (int j = i + 1; j <= 4; j++)
                {
                    PlayerPrefs.SetFloat("SavedTime" + j, PlayerPrefs.GetFloat("SavedTime" + (j - 1).ToString(), 0));
                }

                PlayerPrefs.SetFloat("SavedTime" + i, time);

                break;
            }
        }
    }

    public void DisplayTime()
    {
        for(int i = 0; i <= 4; i++)
        {
            timeTexts[i].text = (i + 1).ToString() + ".\t" + ((int)PlayerPrefs.GetFloat("SavedTime" + i, 0) / 60).ToString("00") + ":" + ((int)PlayerPrefs.GetFloat("SavedTime" + i, 0) % 60).ToString("00");
        }
    }
}
