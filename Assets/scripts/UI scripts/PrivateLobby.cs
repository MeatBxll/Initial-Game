using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;

public class PrivateLobby : MonoBehaviour
{
    [SerializeField] TMP_Text[] TeamNames;
    [SerializeField] GameObject OptionsMenu;
    public GameObject[] LobbyPlayersLobbyUI;
    
    public void UpdatePlayerNameSpots()
    {
        foreach(GameObject i in LobbyPlayersLobbyUI)
        {
            if(i.gameObject.GetComponent<LobbyPlayer>().IsRedTeam)
            {

            }
            else
            {

            }
        }
    }

    public void GameOptions(bool i)
    {
        OptionsMenu.SetActive(i);
    }

    public void ChangeTeamsButton()
    {
        
    }
}
