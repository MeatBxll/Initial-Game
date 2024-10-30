using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;

public class LobbyPlayer : NetworkBehaviour
{
    public NetworkManagerSteam networkManager;
    public bool IsLeader;
    public bool IsRedTeam;
    public Toggle AbleToSwitchTeamsToggle;
    [SerializeField] private PrivateLobby privateLobby;
    public List<GameObject> lobbyPlayers;

    public bool AbleToSwitchTeams = true;

    private void Update()
    {
        if(lobbyPlayers.Count != networkManager.LobbyPlayers.Count)
        {
            lobbyPlayers.Clear();
            lobbyPlayers = networkManager.LobbyPlayers;
        }
    }

    public void LobbyPlayerLeaveGame()
    {
        if(isServer && isLocalPlayer) networkManager.StopHost();
        else if(isLocalPlayer) networkManager.StopClient();

        if(SceneManager.GetActiveScene().name != "menuScene")SceneManager.LoadScene("menuScene");
        else 
        {
            MainMenu mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();
            mainMenu.mainMenu.SetActive(true);
        }
        
        networkManager.ClearLobbyPlayers();
        
        
    }

    public void ChangeTeams()
    {
        if(SceneManager.GetActiveScene().name == "menuScene" && AbleToSwitchTeams && isLocalPlayer)
        {
            if(isLocalPlayer) IsRedTeam = !IsRedTeam;
            privateLobby.ChangeTeamsButton();
        }
    }
}
