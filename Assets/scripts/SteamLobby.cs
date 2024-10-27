using UnityEngine;
using Mirror;
using Steamworks;
using Mirror.BouncyCastle.Pqc.Crypto.Lms;

public class SteamLobby : MonoBehaviour
{
    [SerializeField] private GameObject[] MainMenu = null;
    private NetworkManager networkManager;
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;
    private const string HostAddressKey = "HostAddress";
    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();
        if(!SteamManager.Initialized) return;

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

    }
    //needs ability to host server
    public void HostLobby()
    {
        foreach(GameObject i in MainMenu) i.SetActive(false);
        // HostButton.SetActive(false);
        //set to friends only 
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly,networkManager.maxConnections);
    }
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK)
        {
            foreach(GameObject i in MainMenu) i.SetActive(true);
            // HostButton.SetActive(true);
            return;
        }
        networkManager.StartHost();
        SteamMatchmaking.SetLobbyData(new CSteamID(callback.m_ulSteamIDLobby),HostAddressKey,SteamUser.GetSteamID().ToString());
    }
    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }
    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        if(NetworkServer.active) return;
        
        string hostAddress = SteamMatchmaking.GetLobbyData(new CSteamID(callback.m_ulSteamIDLobby), HostAddressKey);
        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();

        foreach(GameObject i in MainMenu) i.SetActive(false);
        // HostButton.SetActive(false);

    }
}
