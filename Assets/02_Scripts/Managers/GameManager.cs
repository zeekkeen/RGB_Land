using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public PlayerData_SO playerData;
    public PlayerStats_SO playerStats;
    public PlayerStats_SO playerInitialStats;
    public Vector3 initialPosition;
    public static GameManager instance;
    public Dialog_SO[] allDialogs;

    void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)    
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    public void RestartValues(){
        foreach (Dialog_SO dialog in allDialogs){
            dialog.index = -1;
        }
        playerStats = Instantiate(playerInitialStats);
        playerData.lastPosition = initialPosition;
        playerData.lastLevel = "TribuInicial";
        playerData.allMapPieces = playerData.initialAllMapPieces;
    }

    public void SaveGame(Vector3 pos, string level){
        playerData.lastPosition = pos;
        playerData.lastLevel = level;
        GameSaveManager.instance.SaveGameSlot();
    }

    public void LoadGame(){
        GameSaveManager.instance.LoadGameSlot();
        SceneManager.LoadScene(playerData.lastLevel);
    }
}
