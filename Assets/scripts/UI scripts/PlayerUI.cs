using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Mirror;
using UnityEngine.PlayerLoop;

public class PlayerUI : MonoBehaviour
{
    public bool GamePaused;
    public bool playerInScene;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject[] playerUI;
    [SerializeField] private GameObject[] optionsMenus;
    [SerializeField] private NetworkManager NetworkManager;


    public int currentBulletCount;
    public int maxBulletCount;
    [SerializeField] private TextMeshProUGUI currentBulletCountText;
    [SerializeField] private TextMeshProUGUI maxBulletCountText;
    

    public float PlayerSensitivity;

    private void Start()
    {
        if(PlayerPrefs.GetInt("FullScreenChoice") == 0) Screen.fullScreen = true;
        if(PlayerPrefs.GetInt("FullScreenChoice") == 1) Screen.fullScreen = false;

        int resolutionIndex = PlayerPrefs.GetInt("ScreenResolutionSaver");
        if(resolutionIndex == 0) Screen.SetResolution(1920, 1080, Screen.fullScreen);
        if(resolutionIndex == 1) Screen.SetResolution(2560, 1440, Screen.fullScreen);
        if(resolutionIndex == 2) Screen.SetResolution(1366, 768, Screen.fullScreen);
        if(resolutionIndex == 3) Screen.SetResolution(1440, 900, Screen.fullScreen);
        if(resolutionIndex == 4) Screen.SetResolution(1280, 720, Screen.fullScreen);
        if(resolutionIndex == 5) Screen.SetResolution(1280, 1024, Screen.fullScreen);        
    }
    private void Update()
    {
        if(playerInScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) 
            {
                GamePaused = !GamePaused;
                DisableCursor();
            }

            BulletAmountVisuals();
            
        }
        else if(GamePaused)
        {
            GamePaused = false;
            DisableCursor();
        }
    }
    private void DisableCursor()
    {
        if(GamePaused == true) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            foreach(GameObject i in playerUI) i.SetActive(false);
            pauseMenu.SetActive(true);
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            foreach(GameObject i in playerUI) i.SetActive(true);
            pauseMenu.SetActive(false);
            foreach(GameObject i in optionsMenus) i.SetActive(false);
        }
    }

    public void Resume()
    {
        GamePaused = false;
        DisableCursor();
    }
    public void Options()
    {
        foreach(GameObject i in optionsMenus) if(i.name == "OptionsMenu") i.SetActive(true); 
        pauseMenu.SetActive(false);
    }
    public void LeaveGame()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("LobbyPlayer");
        foreach(GameObject i in g) i.GetComponent<LobbyPlayer>().LobbyPlayerLeaveGame();
    }



    //Options Menus
    public void Back()
    {
        foreach(GameObject i in optionsMenus) i.SetActive(false); 
        pauseMenu.SetActive(true);
    }

    public void ControlsMenu()
    {
        foreach(GameObject i in optionsMenus)
        {
            if(i.name == "ControlsMenu") i.SetActive(true);
            else i.SetActive(false);
        }
        pauseMenu.SetActive(false);
    }

    public void VideoMenu()
    {
        foreach(GameObject i in optionsMenus)
        {
            if(i.name == "VideoMenu") i.SetActive(true);
            else i.SetActive(false);
        }
        pauseMenu.SetActive(false);
    }

    public void AudioMenu()
    {
        foreach(GameObject i in optionsMenus)
        {
            if(i.name == "AudioMenu") i.SetActive(true);
            else i.SetActive(false);
        }
        pauseMenu.SetActive(false);
    }




    public void OnPlayerStart()
    {
        pauseMenu.SetActive(false);
        playerInScene = true;
        GamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        foreach(GameObject i in playerUI) i.SetActive(true);
        foreach(GameObject i in optionsMenus) i.SetActive(false); 
        QualitySettings.SetQualityLevel(PlayerPrefs.GetInt("QualityFactor"));
    }

    public void BulletAmountVisuals()
    {
        currentBulletCountText.text = currentBulletCount.ToString();
        maxBulletCountText.text = maxBulletCount.ToString();
    }

}
