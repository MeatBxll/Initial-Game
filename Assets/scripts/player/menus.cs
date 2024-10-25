using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;

public class menus : NetworkBehaviour 
{
    public static bool GamePaused;
    private GameObject PauseMenu;
    void Start()
    {
        if(isLocalPlayer)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Transform holderPauseMenu = GameObject.FindGameObjectWithTag("PauseMenu").GetComponentInChildren<Transform>(true);
            PauseMenu = holderPauseMenu.gameObject;
            Debug.Log(PauseMenu.name);
        }
    }

    void Update()
    {
        if (!isLocalPlayer) return;
        if(Input.GetKeyDown(KeyCode.Escape)) OpenMenu();

    }


    public void OpenMenu()
    {
        GamePaused = !GamePaused;
        DisableCursor();
        if(GamePaused == true) PauseMenu.SetActive(true);
        else PauseMenu.SetActive(false);
    }

    private void DisableCursor()
    {
        if(GamePaused == true) 
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else 
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
