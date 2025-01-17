using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Mirror;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using Unity.VisualScripting;

public class PlayerUI : MonoBehaviour
{
    public bool GamePaused;
    public bool playerInScene;
    [SerializeField] private GameObject pauseMenu;
    public GameObject[] playerUI;
    [SerializeField] private GameObject[] optionsMenus;
    [SerializeField] private TextMeshProUGUI currentBulletCountText;
    [SerializeField] private TextMeshProUGUI maxBulletCountText;

    [SerializeField] private TextMeshProUGUI maxHealth;
    [SerializeField] private TextMeshProUGUI currentHealth;
    [SerializeField] private Slider healthSlider;
    [HideInInspector] public float PlayerSensitivity;



    //E ability stuff
    [HideInInspector] public float eAbilityCoolDown = 0;
    [SerializeField] private Image eAbilityCooldownPanel;
    [SerializeField] private TMP_Text eAbilityText;
    private float CurrentEFillAmount;
    private int currentETextAmount;
    
    //Q ability stuff
    [HideInInspector] public float qAbilityCoolDown = 0;
    [SerializeField] private Image qAbilityCooldownPanel;
    [SerializeField] private TMP_Text qAbilityText;
    private float CurrentQFillAmount;
    private int currentQTextAmount;  

    //r ability stuff
    [HideInInspector] public float rAbilityCoolDown = 0;
    [SerializeField] private Image rAbilityCooldownPanel;
    [SerializeField] private TMP_Text rAbilityText;
    private float CurrentRFillAmount;
    private int currentRTextAmount;    

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

        if(SceneManager.GetActiveScene().name != "menuScene") rAbilityCooldownPanel.fillAmount = 0.0f;
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
        }
        else if(GamePaused)
        {
            GamePaused = false;
            DisableCursor();
        }
        if(rAbilityCoolDown != 0) RAbilityCoolDown();
        if(eAbilityCoolDown != 0) EAbilityCoolDown();
        if(qAbilityCoolDown != 0) QAbilityCoolDown();
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

    public void BulletAmountVisuals(int i, int g)
    {
        currentBulletCountText.text = i.ToString();
        maxBulletCountText.text = g.ToString();
    }

    public void PlayerHealthVisuals(float i, float g)
    {
        currentHealth.text = i.ToString();
        maxHealth.text = g.ToString();

        healthSlider.maxValue = g;
        healthSlider.value = i;

    }

    private void RAbilityCoolDown()
    {
        if(CurrentRFillAmount == 0) 
        {
            float g = Mathf.Round(rAbilityCoolDown);
            currentRTextAmount = (int) g;
            RAbilityCoolDownText(); 
        }
        
        rAbilityCoolDown -= Time.deltaTime;
        if(rAbilityCoolDown > 0.0f)
        {
            if(CurrentRFillAmount <= (1 / rAbilityCoolDown) -Time.deltaTime) 
            {
                rAbilityCooldownPanel.fillAmount = (1 / rAbilityCoolDown) -Time.deltaTime ;
                CurrentRFillAmount = (1 / rAbilityCoolDown) -Time.deltaTime;
                rAbilityText.gameObject.SetActive(true);
            }
        }
        else
        {
            rAbilityCooldownPanel.fillAmount = 0.0f;
            rAbilityCoolDown = 0;
            CurrentRFillAmount = 0;
            rAbilityText.gameObject.SetActive(false);
        }
    }
    private void RAbilityCoolDownText()
    {
        if(currentRTextAmount == 0) CancelInvoke("RAbilityCoolDownText");
        else
        {
            Invoke("RAbilityCoolDownText", .9f);
            currentRTextAmount--;
            rAbilityText.text = currentRTextAmount.ToString();
        }
    }

    private void QAbilityCoolDown()
    {
        if(CurrentQFillAmount == 0) 
        {
            float g = Mathf.Round(qAbilityCoolDown);
            currentQTextAmount = (int) g;
            QAbilityCoolDownText(); 
        }
        
        qAbilityCoolDown -= Time.deltaTime;
        if(qAbilityCoolDown > 0.0f)
        {
            if(CurrentQFillAmount <= (1 / qAbilityCoolDown) -Time.deltaTime) 
            {
                qAbilityCooldownPanel.fillAmount = (1 / qAbilityCoolDown) -Time.deltaTime ;
                CurrentQFillAmount = (1 / qAbilityCoolDown) -Time.deltaTime;
                qAbilityText.gameObject.SetActive(true);
            }
        }
        else
        {
            qAbilityCooldownPanel.fillAmount = 0.0f;
            qAbilityCoolDown = 0;
            CurrentQFillAmount = 0;
            qAbilityText.gameObject.SetActive(false);
        }
    }
    private void QAbilityCoolDownText()
    {
        if(currentQTextAmount == 0) CancelInvoke("QAbilityCoolDownText");
        else
        {
            Invoke("QAbilityCoolDownText", .9f); 
            currentQTextAmount--;
            qAbilityText.text = currentQTextAmount.ToString();
        }
    }

    private void EAbilityCoolDown()
    {
        if(CurrentEFillAmount == 0) 
        {
            float g = Mathf.Round(eAbilityCoolDown);
            currentETextAmount = (int) g;
            EAbilityCoolDownText(); 
        }
        
        eAbilityCoolDown -= Time.deltaTime;
        if(eAbilityCoolDown > 0.0f)
        {
            if(CurrentEFillAmount <= (1 / eAbilityCoolDown) -Time.deltaTime) 
            {
                eAbilityCooldownPanel.fillAmount = (1 / eAbilityCoolDown) -Time.deltaTime ;
                CurrentRFillAmount = (1 / eAbilityCoolDown) -Time.deltaTime;
                eAbilityText.gameObject.SetActive(true);
            }
        }
        else
        {
            eAbilityCooldownPanel.fillAmount = 0.0f;
            eAbilityCoolDown = 0;
            CurrentEFillAmount = 0;
            eAbilityText.gameObject.SetActive(false);
        }
    }
    private void EAbilityCoolDownText()
    {
        if(currentETextAmount == 0) CancelInvoke("EAbilityCoolDownText");
        else
        {
            Invoke("EAbilityCoolDownText", .9f); 
            currentETextAmount--;
            eAbilityText.text = currentETextAmount.ToString();
        }
    }

}
