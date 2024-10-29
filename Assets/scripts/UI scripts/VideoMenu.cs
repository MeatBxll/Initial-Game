using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class VideoMenu : MonoBehaviour
{
    [SerializeField] private GameObject playerUI;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private TMP_Dropdown QualityDropdown;
    [SerializeField] private TMP_Dropdown WindowDropdown;
    [SerializeField] private TMP_Dropdown ResolutionDropdown;
    [SerializeField] private TMP_Text FOVText;
    [SerializeField] private Slider FOVSlider;
    private int res;
    private void Start()
    {
        QualityDropdown.value = PlayerPrefs.GetInt("QualityFactor");
        WindowDropdown.value = PlayerPrefs.GetInt("FullScreenChoice");
        ResolutionDropdown.value = PlayerPrefs.GetInt("ScreenResolutionSaver");
        res = PlayerPrefs.GetInt("ScreenResolutionSaver");

        FOVSlider.value = PlayerPrefs.GetFloat("FOV");
    }
    public void VideoMenuBackButton()
    {
        optionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    public void SetQuality(int qualityFactor)
    {
        QualitySettings.SetQualityLevel(qualityFactor);
        PlayerPrefs.SetInt("QualityFactor", qualityFactor);
    }
    public void SetWindow(int windowMode)
    {
        if(windowMode == 0) 
        {
            if(res == 0) Screen.SetResolution(1920, 1080, Screen.fullScreen = true);
            if(res == 1) Screen.SetResolution(2560, 1440, Screen.fullScreen = true);
            if(res == 2) Screen.SetResolution(1366, 768, Screen.fullScreen = true);
            if(res == 3) Screen.SetResolution(1440, 900, Screen.fullScreen = true);
            if(res == 4) Screen.SetResolution(1280, 720, Screen.fullScreen = true);
            if(res == 5) Screen.SetResolution(1280, 1024, Screen.fullScreen = true);
        }
        if(windowMode == 1) Screen.fullScreen = false;
        

        PlayerPrefs.SetInt("FullScreenChoice", windowMode);
        
    }
    public void SetResolution(int resolutionIndex)
    {
        if(resolutionIndex == 0) Screen.SetResolution(1920, 1080, Screen.fullScreen);
        if(resolutionIndex == 1) Screen.SetResolution(2560, 1440, Screen.fullScreen);
        if(resolutionIndex == 2) Screen.SetResolution(1366, 768, Screen.fullScreen);
        if(resolutionIndex == 3) Screen.SetResolution(1440, 900, Screen.fullScreen);
        if(resolutionIndex == 4) Screen.SetResolution(1280, 720, Screen.fullScreen);
        if(resolutionIndex == 5) Screen.SetResolution(1280, 1024, Screen.fullScreen);
        PlayerPrefs.SetInt("ScreenResolutionSaver", resolutionIndex);
        res = resolutionIndex;
    }
    public void FieldOfViewSlider()
    {
        FOVText.text = FOVSlider.value.ToString("#.0");
        PlayerPrefs.SetFloat("FOV", FOVSlider.value);
    }

}
