using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public GameObject[] players;
    [Command]
    public void CmdLeaveGame()
    {
        if(isLocalPlayer && isClient) NetworkManager.singleton.StopClient();
        if(isServer) NetworkManager.singleton.StopHost();

        players = GameObject.FindGameObjectsWithTag("Player");
        foreach(GameObject i in players) Destroy(i,0f);
        Debug.Log("runnong");
    }

}
