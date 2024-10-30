using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Steamworks;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject PlayMenu;
    [SerializeField] private GameObject[] OptionsMenuAndBackButtons;
    public GameObject mainMenu;
    [SerializeField] private GameObject PrivateLobby;
    [SerializeField] private NetworkManagerSteam NetworkManagerSteam;

    public void OptionsMenuFromMainMenu(bool i)
    {
        foreach(GameObject g in OptionsMenuAndBackButtons)
        {
            if(g.name != "Back") g.SetActive(i);
            else g.SetActive(!i);
        }
        mainMenu.SetActive(!i);
    }
    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void PlayButton(bool i)
    {
        mainMenu.SetActive(!i);
        PlayMenu.SetActive(i);
    }
}
