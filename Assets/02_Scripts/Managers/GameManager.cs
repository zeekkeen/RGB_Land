using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{

    public PlayerData_SO playerData;
    public static GameManager instance;

    void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)    
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    public void RestartValues(){
        playerData.playerStats = Instantiate(playerData.playerInitialStats);
        playerData.lastPosition = playerData.initialPosition;
    }

    public void SaveGame(Vector3 pos){
        playerData.lastPosition = pos;
        GameSaveManager.instance.SaveGameSlot();
    }

    public void LoadGame(){
        GameSaveManager.instance.LoadGameSlot();
        SceneManager.LoadScene("TribuInicial");
    }
}
