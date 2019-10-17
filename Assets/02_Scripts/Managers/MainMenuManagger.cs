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

    void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)    
            Destroy(this);
        // DontDestroyOnLoad(this);
    }

    void Start(){
        Back();
        if(GameSaveManager.instance.IsSaveFile())
            continueButton.SetActive(true);
        else
            continueButton.SetActive(false);
        if (PlayerPrefs.GetInt("gameSlot", -1) != -1) //el -1 es el valor por defecto que nos da si no encontra nada
            GameSaveManager.instance.gameSlot = PlayerPrefs.GetInt("gameSlot");
        // if (PlayerPrefs.GetInt("sound", -1) != -1) 
        //     GameSaveManager.instance.gameSlot = PlayerPrefs.GetInt("sound");
        // if (PlayerPrefs.GetInt("effects", -1) != -1) 
        //     GameSaveManager.instance.gameSlot = PlayerPrefs.GetInt("effects");
    }

    // void Update(){
        
    // }

    public void Back(){
        mainPanel.SetActive(true);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void Continue(){
        GameSaveManager.instance.LoadGameSlot();
    }

    public void NewGame(){
        mainPanel.SetActive(false);
        newGamePanel.SetActive(true);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void NewGameSlot(int n){
        GameManager.instance.RestartValues();
        GameSaveManager.instance.LoadGameSlot(n);
        PlayerPrefs.SetInt("gameSlot", n);
        SceneManager.LoadScene("TribuInicial");
    }

    public void LoadGame(){
        mainPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(true);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
    }

    public void Options(){
        mainPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(true);
        exitPanel.SetActive(false);
    }

    public void Exit(){
        mainPanel.SetActive(false);
        newGamePanel.SetActive(false);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    public void ExitCompleted( bool r){
        if(r)
            Application.Quit();
        else
            Back();
    }

    public void LoadSlot( int n){
        GameSaveManager.instance.LoadGameSlot(n);
        PlayerPrefs.SetInt("gameSlot", n);
        SceneManager.LoadScene("TribuInicial");
    }

    public void ChangeIdiom( int n){
        
    }
}
