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
    public string acceptSound;

    void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)    
            Destroy(this);
        // DontDestroyOnLoad(this);
    }

    void Start(){
        Back();
        // if(GameSaveManager.instance.IsSaveFile())
        //     continueButton.SetActive(true);
        // else
        //     continueButton.SetActive(false);
        if (PlayerPrefs.GetInt("gameSlot", -1) != -1){ //el -1 es el valor por defecto que nos da si no encontra nada
            GameSaveManager.instance.gameSlot = PlayerPrefs.GetInt("gameSlot");
            continueButton.SetActive(true);
        }else{
            continueButton.SetActive(false);
            GameSaveManager.instance.gameSlot = 0;
        }
        // if (PlayerPrefs.GetInt("sound", -1) != -1) 
        //     GameSaveManager.instance.gameSlot = PlayerPrefs.GetInt("sound");
        // if (PlayerPrefs.GetInt("effects", -1) != -1) 
        //     GameSaveManager.instance.gameSlot = PlayerPrefs.GetInt("effects");
    }

    // void Update(){
        
    // }

    public void Back(){
        SoundManager.instance.PlaySound(acceptSound);
        mainPanel.SetActive(true);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
        buttonsFocus[0].ChangeFocus();
    }

    public void Continue(){
        SoundManager.instance.PlaySound(acceptSound);
        GameSaveManager.instance.LoadGameSlot();
        SceneManager.LoadScene(GameManager.instance.playerData.lastLevel);
    }

    public void NewGame(){
        SoundManager.instance.PlaySound(acceptSound);
        mainPanel.SetActive(false);
        newGamePanel.SetActive(true);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
        buttonsFocus[1].ChangeFocus();
    }

    public void NewGameSlot(int n){
        SoundManager.instance.PlaySound(acceptSound);
        GameManager.instance.RestartValues();
        GameSaveManager.instance.SaveGameSlot(n);
        PlayerPrefs.SetInt("gameSlot", n);
        SceneManager.LoadScene(GameManager.instance.playerData.lastLevel);
    }

    public void LoadGame(){
        SoundManager.instance.PlaySound(acceptSound);
        mainPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(true);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
        buttonsFocus[2].ChangeFocus();
    }

    public void Options(){
        SoundManager.instance.PlaySound(acceptSound);
        mainPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(true);
        exitPanel.SetActive(false);
        buttonsFocus[3].ChangeFocus();
    }

    public void Exit(){
        SoundManager.instance.PlaySound(acceptSound);
        mainPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(true);
        buttonsFocus[4].ChangeFocus();
    }

    public void ExitCompleted( bool r){
        SoundManager.instance.PlaySound(acceptSound);
        if(r)
            Application.Quit();
        else
            Back();
    }

    public void LoadSlot( int n){
        GameSaveManager.instance.LoadGameSlot(n);
        PlayerPrefs.SetInt("gameSlot", n);
        SceneManager.LoadScene(GameManager.instance.playerData.lastLevel);
    }

    public void ChangeIdiom( int n){
        
    }
}
