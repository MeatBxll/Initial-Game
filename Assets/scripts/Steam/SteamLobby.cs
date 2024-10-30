using UnityEngine;
using Mirror;
using Steamworks;
using Mirror.BouncyCastle.Pqc.Crypto.Lms;

public class SteamLobby : MonoBehaviour
{
    [SerializeField] private GameObject[] MainMenuElements = null;
    [SerializeField] private GameObject PlayMenu = null;
    [SerializeField] private GameObject PrivateLobby = null;

    private NetworkManager networkManager;
    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;
    private const string HostAddressKey = "HostAddress";
    private void Start()
    {
        networkManager = GetComponent<NetworkManagerSteam>();
        if(!SteamManager.Initialized) return;
        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);

    }
    //needs ability to host server
    public void HostLobby(bool i)
    {

        PlayMenu.SetActive(!i);
        // PrivateLobby.SetActive(i);
        if(i) SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypeFriendsOnly, networkManager.maxConnections); //set to friends only 
        else networkManager.StopHost();
    }
    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        if(callback.m_eResult != EResult.k_EResultOK)
        {
            PlayMenu.SetActive(true);
            // PrivateLobby.SetActive(false);
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

        foreach(GameObject i in MainMenuElements) i.SetActive(false);
        PrivateLobby.SetActive(true);
    }
}
