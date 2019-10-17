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

    }

    public void NewGame(){
        mainPanel.SetActive(false);
        newGamePanel.SetActive(true);
        loadGamePanel.SetActive(false);
        optionsPanel.SetActive(false);
        exitPanel.SetActive(false);
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

    public void SaveSlot( int n){
        
    }

    public void LoadSlot( int n){
        SceneManager.LoadScene("TribuInicial");
    }

    public void ChangeIdiom( int n){
        
    }
}
