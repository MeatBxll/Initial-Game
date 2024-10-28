using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public bool GamePaused;
    public bool playerInScene;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject[] playerUI;
    [SerializeField] private GameObject[] optionsMenus;


    public int currentBulletCount;
    public int maxBulletCount;
    [SerializeField] private TextMeshProUGUI currentBulletCountText;
    [SerializeField] private TextMeshProUGUI maxBulletCountText;

    public float PlayerSensitivity;

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
            DisableCursor();
            GamePaused = false;
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
        Application.Quit();
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

    }

    public void BulletAmountVisuals()
    {
        currentBulletCountText.text = currentBulletCount.ToString();
        maxBulletCountText.text = maxBulletCount.ToString();
    }

}
