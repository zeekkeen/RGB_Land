using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuManagger : MonoBehaviour{
    
    public GameObject mainPanel;
    public GameObject continueButton;
    public GameObject newGamePanel;
    public GameObject loadGamePanel;
    public GameObject optionsPanel;
    public GameObject exitPanel;
    public static MainMenuManagger instance;
    public ButtonThroughKeySelection[]  buttonsFocus;
    PlayerControls inputAction;
    Vector2 movementInput;
    

    void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)    
            Destroy(this);
        // DontDestroyOnLoad(this);
        inputAction = new PlayerControls();
        inputAction.GamePlay.move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.GamePlay.move.canceled += ctx => movementInput = Vector2.zero;
        inputAction.GamePlay.RangedAttack.performed += ctx => BackBtnPressed();
    }

    void OnEnable() {
        inputAction.Enable();
    }

    void OnDisable() {
        inputAction.Disable();
    }

    void Start(){
        mainPanel.SetActive(true);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
        buttonsFocus[0].ChangeFocus();
        if (PlayerPrefs.GetInt("gameSlot", -1) != -1){ //el -1 es el valor por defecto que nos da si no encontra nada
            GameSaveManager.instance.gameSlot = PlayerPrefs.GetInt("gameSlot");
            continueButton.SetActive(true);
        }else{
            continueButton.SetActive(false);
            GameSaveManager.instance.gameSlot = 0;
        }
    }

    void Update() {
        if(movementInput.y < 0f)
            SoundManager.instance.PlaySound("DownBtn");
        else if(movementInput.y > 0)
            SoundManager.instance.PlaySound("UpBtn");
    }

    void BackBtnPressed(){
        if(!mainPanel.activeSelf)
            Back();
        else
            Exit();
    }

    public void Back(){
        SoundManager.instance.PlaySound("AcceptBtn");
        mainPanel.SetActive(true);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
        buttonsFocus[0].ChangeFocus();
    }

    public void Continue(){
        SoundManager.instance.PlaySound("AcceptBtn");
        GameSaveManager.instance.LoadGameSlot();
        TransitionManager.instance.LoadSceneWithTransition(GameManager.instance.playerData.lastLevel);
    }

    public void NewGame(){
        SoundManager.instance.PlaySound("AcceptBtn");
        mainPanel.SetActive(false);
        newGamePanel.SetActive(true);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
        buttonsFocus[1].ChangeFocus();
    }

    public void NewGameSlot(int n){
        SoundManager.instance.PlaySound("AcceptBtn");
        GameManager.instance.RestartValues();
        GameSaveManager.instance.SaveGameSlot(n);
        PlayerPrefs.SetInt("gameSlot", n);
        TransitionManager.instance.LoadSceneWithTransition(GameManager.instance.playerData.lastLevel);
    }

    public void LoadGame(){
        SoundManager.instance.PlaySound("AcceptBtn");
        mainPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(true);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
        buttonsFocus[2].ChangeFocus();
    }

    public void Options(){
        SoundManager.instance.PlaySound("AcceptBtn");
        mainPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(true);
        exitPanel.SetActive(false);
        buttonsFocus[3].ChangeFocus();
    }

    public void Exit(){
        SoundManager.instance.PlaySound("AcceptBtn");
        mainPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(true);
        buttonsFocus[4].ChangeFocus();
    }

    public void ExitCompleted( bool r){
        SoundManager.instance.PlaySound("AcceptBtn");
        if(r)
            Application.Quit();
        else
            Back();
    }

    public void LoadSlot( int n){
        GameSaveManager.instance.LoadGameSlot(n);
        PlayerPrefs.SetInt("gameSlot", n);
        TransitionManager.instance.LoadSceneWithTransition(GameManager.instance.playerData.lastLevel);
    }

    public void ChangeIdiom( int n){
        
    }
}