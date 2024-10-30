using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class LobbyPlayer : NetworkBehaviour
{
    public GameObject networkManager;
    private GameObject[] MenuSceneAssets;
    public bool IsLeader;
    public bool IsRedTeam;

    void Start()
    {
        
        
    }

    public void LobbyPlayerLeaveGame()
    {
        if(isServer && isLocalPlayer) networkManager.GetComponent<NetworkManagerSteam>().StopHost();
        else if(isLocalPlayer) networkManager.GetComponent<NetworkManagerSteam>().StopClient();

        if(SceneManager.GetActiveScene().name != "menuScene")SceneManager.LoadScene("menuScene");
        else 
        {
            MainMenu mainMenu = GameObject.FindGameObjectWithTag("MainMenu").GetComponent<MainMenu>();
            mainMenu.mainMenu.SetActive(true);
        }
        
        networkManager.GetComponent<NetworkManagerSteam>().ClearLobbyPlayers();
        
        
    }
}
