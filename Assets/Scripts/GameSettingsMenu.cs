using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class GameSettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Button settingsButton;
    public Slider volumeSlider;
    public Slider SFXvolumeSlider;
    private string volumeset = "Volume";
    private string sfxVolumeSet = "SFXVolume";

    private float currentSFXVolume;
    private float currentVolume;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
        PlayerPrefs.SetFloat(volumeset, volume);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
        PlayerPrefs.SetFloat(sfxVolumeSet, volume);
        PlayerPrefs.Save();
    }

    void Start()
    {
        // PlayerPrefs'ten daha �nce se�ilen temay� al
        currentVolume = PlayerPrefs.GetFloat(volumeset, 0);
        currentSFXVolume = PlayerPrefs.GetFloat(sfxVolumeSet, 0);
        SetVolume(currentVolume);
        SetSFXVolume(currentSFXVolume);
        settingsButton.onClick.AddListener(OnSettingsOpened);
       
    }

    void OnSettingsOpened()
    {
        currentVolume = PlayerPrefs.GetFloat(volumeset, 0);
        currentSFXVolume = PlayerPrefs.GetFloat(sfxVolumeSet, 0);
        volumeSlider.value = currentVolume;
        SFXvolumeSlider.value = currentSFXVolume;
        
    }
   
}
