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


    public int currentBulletCount;
    public int maxBulletCount;
    [SerializeField] private TextMeshProUGUI currentBulletCountText;
    [SerializeField] private TextMeshProUGUI maxBulletCountText;

    private void Update()
    {
        if(playerInScene)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) OpenMenu();
            BulletAmountVisuals();
            
        }
        else if(GamePaused)
        {
            DisableCursor();
            GamePaused = false;
        }
    }


    public void OpenMenu()
    {
        GamePaused = !GamePaused;
        DisableCursor();
        if(GamePaused == true) pauseMenu.SetActive(true);
        else pauseMenu.SetActive(false);

    }

    private void DisableCursor()
    {
        if(GamePaused == true) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            foreach(GameObject i in playerUI) i.SetActive(false);
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            foreach(GameObject i in playerUI) i.SetActive(true);
        }
    }

    public void Resume()
    {
        OpenMenu();
    }
    public void Options()
    {

    }
    public void LeaveGame()
    {
        Application.Quit();
    }

    public void OnPlayerStart()
    {
        pauseMenu.SetActive(false);
        playerInScene = true;
        GamePaused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //activates all of the player ui
        foreach(GameObject i in playerUI) i.SetActive(true);
    }

    public void BulletAmountVisuals()
    {
        currentBulletCountText.text = currentBulletCount.ToString();
        maxBulletCountText.text = maxBulletCount.ToString();
    }

}
