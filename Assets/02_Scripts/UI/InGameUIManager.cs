using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIManager : MonoBehaviour{

    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject inGamePanel;
    public GameObject mapPanel;
    public bool mapOpened = false, pauseOpened = false;
    public ButtonThroughKeySelection[]  buttonsFocus;
    PlayerControls inputAction;
    public LinearProgressBar energyProgressBar;
    public Image circleImage;
    public static InGameUIManager instance;

    void Awake() {
        instance = this;
        inputAction = new PlayerControls();
        inputAction.GamePlay.Pause.performed += ctx => PausePanel();
        inputAction.GamePlay.Map.performed += ctx => MapPanel();
    }

    void Update() {
        if(energyProgressBar.current < energyProgressBar.maximum){
            energyProgressBar.current += (Time.deltaTime * GameManager.instance.playerStats.energyGain);
            energyProgressBar.GetCurrentFill();
        }
    }

    public void UpdateCircleColor(){
        switch(GameManager.instance.playerStats.activeRangePower){
            // case -1:
            //     circleImage.color = Color.white;
            // break;
            case 0:
                circleImage.color = Color.red;
            break;
            case 1:
                circleImage.color = Color.green;
            break;
            case 2:
                circleImage.color = Color.blue;
            break;
        }
    }

    public void UpdateCurrentFill(){
        energyProgressBar.GetCurrentFill();
    }
    void OnEnable() {
        inputAction.Enable();
    }

    void OnDisable() {
        inputAction.Disable();
    }

    void Start() {
        InGamePanel();
    }

    public void InGamePanel(){
        inGamePanel.SetActive(true);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        mapPanel.SetActive(false);
        // Time.timeScale = 1;
        GameManager.instance.playerData.inPause = false;
    }

    public void MapPanel(){
        if(!pauseOpened && !gameOverPanel.activeSelf){
            mapOpened = !mapOpened;
            inGamePanel.SetActive(true);
            pausePanel.SetActive(false);
            mapPanel.SetActive(mapOpened);
            gameOverPanel.SetActive(false);
            // Time.timeScale = mapOpened ? 0 : 1;
            GameManager.instance.playerData.inPause = mapOpened ? true : false;
        }
    }

    public void PausePanel(){
        if(!mapOpened && !gameOverPanel.activeSelf){
            pauseOpened = !pauseOpened;
            inGamePanel.SetActive(!pauseOpened);
            pausePanel.SetActive(pauseOpened);
            gameOverPanel.SetActive(false);
            if(pauseOpened)
                buttonsFocus[0].ChangeFocus();
            mapPanel.SetActive(false);
            // Time.timeScale = pauseOpened ? 0 : 1;
            GameManager.instance.playerData.inPause = pauseOpened ? true : false;
        }
    }

    public void GameOverPanel(){
        GameManager.instance.playerData.inPause = true;
        inGamePanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        buttonsFocus[1].ChangeFocus();
        mapPanel.SetActive(false);
        // Time.timeScale = 0;
    }

    public void Reiniciar(){
        GameSaveManager.instance.LoadGameSlot();
        // SceneManager.LoadScene(GameManager.instance.playerData.lastLevel);
        TransitionManager.instance.LoadSceneWithTransition(GameManager.instance.playerData.lastLevel);
    }

    public void MainMenu(){
        Time.timeScale = 1;
        TransitionManager.instance.LoadSceneWithTransition("MainMenu");
        // SceneManager.LoadScene("MainMenu");
    }
}
