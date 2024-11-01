using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using System.Linq;
using Telepathy;
using Steamworks;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class NetworkManagerSteam : NetworkManager
{
    public List<GameObject> LobbyPlayers = new List<GameObject>();

    public bool UpdateToPlayerlistRequired;

    public String SelectedMap = "Map1";

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        if(numPlayers >= maxConnections)
        {
            conn.Disconnect();
            return;
        }

        if(SceneManager.GetActiveScene().name != "menuScene")
        {
            conn.Disconnect();
            return;
        }
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        if(SceneManager.GetActiveScene().name == "menuScene")
        {
            bool isLeader = LobbyPlayers.Count == 0;

            GameObject roomPlayerInstace = Instantiate(playerPrefab);
            roomPlayerInstace.name = SteamFriends.GetPersonaName();

            roomPlayerInstace.GetComponent<LobbyPlayer>().IsLeader = isLeader;
            roomPlayerInstace.GetComponent<LobbyPlayer>().networkManager = gameObject.GetComponent<NetworkManagerSteam>();
            
            roomPlayerInstace.GetComponent<LobbyPlayer>().conn = conn;

            LobbyPlayers.Add(roomPlayerInstace);

            NetworkServer.AddPlayerForConnection(conn, roomPlayerInstace);

        }
    }

    public void LoadMap()
    {
        SceneManager.LoadScene(SelectedMap);
    }
    public void MapLoaded()
    {
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("LobbyPlayer")) g.GetComponent<LobbyPlayer>().GameLoaded();
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        if(conn.identity != null)
        {
            var player = conn.identity.gameObject;
            LobbyPlayers.Remove(player);
        }

        base.OnServerDisconnect(conn);
    }

    public void ClearLobbyPlayers()
    {
        LobbyPlayers.Clear();
    }

}
