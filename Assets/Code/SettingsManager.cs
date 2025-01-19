using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] Slider musicSlider;


    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GameObject.Find("SingleSoundManager").GetComponent<AudioSource>();
        musicSource = GameObject.Find("MusicSoundManager").GetComponent<AudioSource>();

        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 0);
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0);

    }
    private void Update()
    {
        PlayerPrefs.SetFloat("SoundVolume", soundSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
        soundSource.volume = soundSlider.value;
        musicSource.volume = musicSlider.value;
    }
}
