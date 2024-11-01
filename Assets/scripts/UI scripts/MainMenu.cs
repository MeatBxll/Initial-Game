using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Steamworks;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public GameObject PlayMenu;
    [SerializeField] private GameObject[] OptionsMenuAndBackButtons;
    public GameObject mainMenu;

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

    public void HostPrivateMatch()
    {
        GameObject.Find("NetworkManager").GetComponent<SteamLobby>().HostLobby();
    }
}
