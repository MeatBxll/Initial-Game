using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JetBrains.Annotations;
using Mirror.Examples.BilliardsPredicted;
using Unity.VisualScripting;
using System;

public class ControlsMenu : MonoBehaviour
{
    [SerializeField] private GameObject OptionsMenu;
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TMP_InputField sensitivityInputField;
    [SerializeField] private TMP_Text[] keyBindTexts;
    private String keyBindHolder;
    [SerializeField] private GameObject[] OptionsUIs;
    [SerializeField] private GameObject listeningText;
    private bool listening;
    private Event listeningEvent = null;

    private void Start() 
    {
        sensitivitySlider.value = PlayerPrefs.GetFloat("Sensitivity");
        sensitivityInputField.text = (sensitivitySlider.value * 50).ToString("#.00");
    }

    public void ControlsBackButton()
    {
        OptionsMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    

    public void SensitivitySliderChanged()
    {
        sensitivityInputField.text = (sensitivitySlider.value * 50).ToString("#.00");
        PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
    }

    public void SensitivityInputFieldChanged()
    {
        float sensitivity = float.Parse(sensitivityInputField.text);
        if(sensitivity > 100f || sensitivity < 5f)
        {
            if(sensitivity > 100f)
            {
                sensitivityInputField.text = ("100.00");
                sensitivitySlider.value = 2f;
            }
            else
            {
                sensitivityInputField.text = ("5.00");
                sensitivitySlider.value = .1f;
            }
        }
        else
        {
            sensitivityInputField.text = sensitivity.ToString("#.00");
            sensitivitySlider.value = sensitivity/50f;
            PlayerPrefs.SetFloat("Sensitivity", sensitivitySlider.value);
        }
    }
    void OnGUI()
    {
        if(listening == true)
        {
            listeningEvent = Event.current;
            if(listeningEvent != null) if(listeningEvent.isKey) 
            {
                Invoke(keyBindHolder,0f);
                CloseOutListeningEvent();
            }
        }
    }
    private void SetupListeningEvent()
    {
        listeningEvent = null;
        listening = true;
        foreach(GameObject i in OptionsUIs) i.SetActive(false);
        listeningText.SetActive(true);
    }
    private void CloseOutListeningEvent()
    {
        listening = false;
        foreach(GameObject i in OptionsUIs) i.SetActive(true);
        listeningText.SetActive(false);
        foreach(TMP_Text i in keyBindTexts) if (i.name == keyBindHolder) i.text = listeningEvent.keyCode.ToString();
    }
    public void Forward()
    {
        keyBindHolder = "forwardText";
        SetupListeningEvent();
    }

    public void Backward()
    {
        keyBindHolder = "backwardText";
        SetupListeningEvent();
    }

    public void Left()
    {
        keyBindHolder = "leftText";
        SetupListeningEvent();
    }

    public void Right()
    {
        keyBindHolder = "rightText";
        SetupListeningEvent();
    }


}
