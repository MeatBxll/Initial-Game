using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using Steamworks;
using System;

public class LobbyPlayer : NetworkBehaviour
{
    public NetworkManagerSteam networkManager;
    public bool IsLeader;
    public bool IsRedTeam;
    public Toggle AbleToSwitchTeamsToggle;
    [SerializeField] private PrivateLobby privateLobby;
    public List<GameObject> lobbyPlayers;
    public GameObject playerPrefab;

    private GameObject yourTeamsSpawnLocation;
    public bool AbleToSwitchTeams = true;

    public NetworkConnectionToClient conn;
    public float respawnTimer;
    private GameObject myPlayer;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
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

        if(SceneManager.GetActiveScene().name != "menuScene") SceneManager.LoadScene("menuScene");
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

    public void OpenSteamFriendsList()
    {
        if(isLocalPlayer) SteamFriends.ActivateGameOverlayInviteDialog(SteamUser.GetSteamID());
    }

    public void GameLoaded()
    {
        if(isLocalPlayer)
        {
            GameObject[] SpawnLocations = GameObject.FindGameObjectsWithTag("SpawnLocation");

            foreach(GameObject g in SpawnLocations)
            {
                if(IsRedTeam) 
                {
                    if(g.name == "RedTeamSpawnLocation") yourTeamsSpawnLocation = g;
                }
                else 
                {
                    if(g.name == "BlueTeamSpawnLocation") yourTeamsSpawnLocation = g;
                }
                
            } 
            CmdSpawnPlayer();
        }
    }

    [Command]
    private void CmdSpawnPlayer()
    {
        myPlayer = Instantiate(playerPrefab, yourTeamsSpawnLocation.transform.position, yourTeamsSpawnLocation.transform.rotation);
        myPlayer.GetComponentInChildren<Health>().myLobbyPlayer = gameObject;
        myPlayer.GetComponentInChildren<Health>().IsRedTeam = IsRedTeam;
        NetworkServer.Spawn(myPlayer, conn);

        if(isOwned) myPlayer.GetComponentInChildren<Slider>().gameObject.SetActive(false);
    }

    public void KillPlayer()
    {
        myPlayer.SetActive(false);
        Invoke("RespawnPlayer", respawnTimer);
    }

    public void RespawnPlayer()
    {
        myPlayer.SetActive(true);
        myPlayer.GetComponentInChildren<Health>().Respawn(yourTeamsSpawnLocation.transform);
    }

    public void PlayerHealthBar(float i, float g)
    {
        if(isOwned) return;
        myPlayer.GetComponentInChildren<Slider>().maxValue = g;
        myPlayer.GetComponentInChildren<Slider>().value = i;
    }

    public void StartGameButton()
    {
        if(SceneManager.GetActiveScene().name == "menuScene")
        {
            privateLobby.LoadChooseCharacterMenu();
        }

    }
}
