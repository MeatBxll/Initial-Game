using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
   
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider effectsVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider voicesVolumeSlider;

    
    private void Start()
    {
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        voicesVolumeSlider.value = PlayerPrefs.GetFloat("VoicesVolume");
    }

    public void AudioMenuBack()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void MasterVolumeSlider()
    {
        PlayerPrefs.SetFloat("MasterVolume", masterVolumeSlider.value);
    }
    public void EffectsVolumeSlider()
    {
        PlayerPrefs.SetFloat("EffectsVolume", effectsVolumeSlider.value);
    }
    public void MusicVolumeSlider()
    {
        PlayerPrefs.SetFloat("MusicVolume", musicVolumeSlider.value);
    }
    public void VoicesVolumeSlider()
    {
        PlayerPrefs.SetFloat("VoicesVolume", voicesVolumeSlider.value);
    }
    public void ResetToDefaultsAudioMenu()
    {
        PlayerPrefs.SetFloat("MasterVolume", 1.8f);
        PlayerPrefs.SetFloat("EffectsVolume", 1.5f);
        PlayerPrefs.SetFloat("MusicVolume", 1.7f);
        PlayerPrefs.SetFloat("VoicesVolume", 1f);
        masterVolumeSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        effectsVolumeSlider.value = PlayerPrefs.GetFloat("EffectsVolume");
        musicVolumeSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        voicesVolumeSlider.value = PlayerPrefs.GetFloat("VoicesVolume");
    }
}

