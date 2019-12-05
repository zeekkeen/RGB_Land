using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour{

    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject inGamePanel;
    public GameObject mapPanel;
    public bool mapOpened = false, pauseOpened = false;
    public ButtonThroughKeySelection[]  buttonsFocus;
    PlayerControls inputAction;
    public LinearProgressBar energyProgressBar;
    public static InGameUIManager instance;

    void Awake() {
        instance = this;
        inputAction = new PlayerControls();
        inputAction.GamePlay.Pause.performed += ctx => PausePanel();
        inputAction.GamePlay.Map.performed += ctx => MapPanel();
    }

    void Update() {
        if(energyProgressBar.current < energyProgressBar.maximum)
            energyProgressBar.current += (Time.deltaTime * GameManager.instance.playerStats.energyGain);
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
        Time.timeScale = 1;
    }

    public void MapPanel(){
        if(!pauseOpened){
            mapOpened = !mapOpened;
            inGamePanel.SetActive(true);
            pausePanel.SetActive(false);
            mapPanel.SetActive(mapOpened);
            gameOverPanel.SetActive(false);
            Time.timeScale = mapOpened ? 0 : 1;
        }
    }

    public void PausePanel(){
        if(!mapOpened){
            pauseOpened = !pauseOpened;
            inGamePanel.SetActive(!pauseOpened);
            pausePanel.SetActive(pauseOpened);
            gameOverPanel.SetActive(false);
            if(pauseOpened)
                buttonsFocus[0].ChangeFocus();
            mapPanel.SetActive(false);
            Time.timeScale = pauseOpened ? 0 : 1;
        }
    }

    public void GameOverPanel(){
        inGamePanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        buttonsFocus[1].ChangeFocus();
        mapPanel.SetActive(false);
        Time.timeScale = 0;
    }

    public void Reiniciar(){
        GameSaveManager.instance.LoadGameSlot();
        SceneManager.LoadScene(GameManager.instance.playerData.lastLevel);
    }

    public void MainMenu(){
        SceneManager.LoadScene("MainMenu");
    }
}
