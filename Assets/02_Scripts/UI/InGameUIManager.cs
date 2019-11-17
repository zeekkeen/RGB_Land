using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InGameUIManager : MonoBehaviour{

    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject inGamePanel;
    public ButtonThroughKeySelection[]  buttonsFocus;
    PlayerControls inputAction;
    public static InGameUIManager instance;

    void Awake() {
        instance = this;
        inputAction = new PlayerControls();
        inputAction.GamePlay.Pause.performed += ctx => PausePanel();
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
        Time.timeScale = 1;
    }

    public void PausePanel(){
        inGamePanel.SetActive(false);
        pausePanel.SetActive(true);
        gameOverPanel.SetActive(false);
        buttonsFocus[0].ChangeFocus();
        Time.timeScale = 0;
    }

    public void GameOverPanel(){
        inGamePanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(true);
        buttonsFocus[1].ChangeFocus();
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
