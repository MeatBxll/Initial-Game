using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PrivateLobby : MonoBehaviour
{
    [SerializeField] TMP_Text[] TeamNames;
    [SerializeField] GameObject OptionsMenu;
    [SerializeField] LobbyPlayer MyLobbyPlayer;
    [SerializeField] GameObject[] HostOnlyOptions;
    public List<GameObject> LobbyPlayers;

    [SerializeField] private ChooseCharacter ChooseCharacterMenu;

    
    private void Start()
    {
        if(!MyLobbyPlayer.IsLeader)
        {
            foreach(GameObject g in HostOnlyOptions)
            {
                if(g.name == "StartGameButton") g.GetComponent<Button>().interactable = false;
                if(g.GetComponent<TMP_Dropdown>() != null) g.GetComponent<TMP_Dropdown>().interactable = false;
                if(g.GetComponent<Toggle>() != null) g.GetComponent<Toggle>().interactable = false;

                if(g.name == "SwapToggle") MyLobbyPlayer.GetComponent<LobbyPlayer>().AbleToSwitchTeamsToggle = g.GetComponent<Toggle>();
            }
        }
    }
    private void Update()
    {
        if(LobbyPlayers.Count != MyLobbyPlayer.GetComponent<LobbyPlayer>().lobbyPlayers.Count)
        {
            LobbyPlayers.Clear();
            LobbyPlayers = MyLobbyPlayer.GetComponent<LobbyPlayer>().lobbyPlayers;
            UpdatePlayerNameSpots();
        }
    }


    public void UpdatePlayerNameSpots()
    {
        int saveRedPos = 0;
        int saveBluePos = 5;

        foreach(TMP_Text m in TeamNames) m.fontStyle = FontStyles.Normal;


        foreach(GameObject i in LobbyPlayers)
        {
            if(i.gameObject.GetComponent<LobbyPlayer>().IsRedTeam)
            {
                TeamNames[saveRedPos].text = i.name;
                TeamNames[saveRedPos].fontStyle = FontStyles.Bold;
                saveRedPos++;
            }
            else
            {
                TeamNames[saveBluePos].text = i.name;
                TeamNames[saveBluePos].fontStyle = FontStyles.Bold;
                saveBluePos++;
            }
        }

        foreach(TMP_Text m in TeamNames) if(m.fontStyle != FontStyles.Bold)
        {
            m.text = ". . .";
        }

    }

    public void GameOptions(bool i)
    {
        OptionsMenu.SetActive(i);
    }

    public void ChangeTeamsButton()
    {
        Invoke("ChangeTeamsButtonDelay", 1f);
        foreach(GameObject g in HostOnlyOptions) if(g.name == "ChangeTeamsButton") g.GetComponent<Button>().interactable = false;
        UpdatePlayerNameSpots();
    }
    private void ChangeTeamsButtonDelay()
    {
        foreach(GameObject g in HostOnlyOptions) if(g.name == "ChangeTeamsButton") g.GetComponent<Button>().interactable = true;
    }

    public void LoadChooseCharacterMenu()
    {
        if(SceneManager.GetActiveScene().name == "menuScene")
        {
            GameObject.Find("NetworkManager").GetComponent<NetworkManagerSteam>().LoadMap();
            Destroy(gameObject);

            // ChooseCharacterMenu.gameObject.SetActive(true);

            // int saveRedPos = 0;
            // int saveBluePos = 5;

            // foreach(TMP_Text m in TeamNames) m.fontStyle = FontStyles.Normal;


            // foreach(GameObject i in LobbyPlayers)
            // {
            //     if(i.gameObject.GetComponent<LobbyPlayer>().IsRedTeam)
            //     {
            //         ChooseCharacterMenu.CCTeamNames[saveRedPos].text = i.name;
            //         ChooseCharacterMenu.CCTeamNames[saveRedPos].fontStyle = FontStyles.Bold;
            //         saveRedPos++;
            //     }
            //     else
            //     {
            //         ChooseCharacterMenu.CCTeamNames[saveBluePos].text = i.name;
            //         ChooseCharacterMenu.CCTeamNames[saveBluePos].fontStyle = FontStyles.Bold;
            //         saveBluePos++;
            //     }
            // }

            // foreach(TMP_Text m in ChooseCharacterMenu.CCTeamNames) if(m.fontStyle != FontStyles.Bold) m.text = ". . .";
            // gameObject.SetActive(false);
            
        }
    }
}
