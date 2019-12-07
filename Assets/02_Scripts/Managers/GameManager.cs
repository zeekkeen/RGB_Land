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
            Destroy(this.gameObject);
        DontDestroyOnLoad(this);
        Cursor.lockState = UnityEngine.CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RestartValues(){
        foreach (Dialog_SO dialog in allDialogs){
            dialog.index = -1;
        }
        playerData.inPause = false;
        playerStats = Instantiate(playerInitialStats);
        playerData.lastPosition = initialPosition;
        playerData.lastLevel = "TribuInicial";
        // playerData.allMapPieces = playerData.initialAllMapPieces;
        ResetAchievements();
        playerData.allMapPieces = new List<MapPieceInfo>(playerData.initialAllMapPieces);
        playerData.playerPinPosition = new Vector3(-495,408,0);
    }

    public void SaveGame(Vector3 pos, string level){
        playerData.lastPosition = pos;
        playerData.lastLevel = level;
        playerData.allMapPieces = MapManager.instance.myAllMapPieces;
        if(MapManager.instance.playerPin != null)
            playerData.playerPinPosition = MapManager.instance.playerPin.transform.position;
        GameSaveManager.instance.SaveGameSlot();
    }

    public void LoadGame(){
        GameSaveManager.instance.LoadGameSlot();
        SceneManager.LoadScene(playerData.lastLevel);
    }

    public bool SearchAchievement(Achievement aux){
        foreach(Achievement achievement in playerData.achievements){
            if(achievement == aux)
                return true;
        }
        return false;
    }

    public void AddAchievement(Achievement aux){
        playerData.achievements.Add(aux);
        switch(aux){
            case Achievement.ColorControll:
                playerStats.stealAndGiveColor = true;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttack>().SpawnCrystal();
            break;
            case Achievement.RangeAttack1:
                playerStats.rangedAttack1 = true;
            break;
            case Achievement.RangeAttack2:
                playerStats.rangedAttack2 = true;
            break;
            case Achievement.RangeAttack3:
                playerStats.rangedAttack3 = true;
            break;

        }
    }

    public void ResetAchievements(){
        playerData.achievements = new List<Achievement>();
    }
}

public enum Achievement{
    NULL,
    SkullChief_Conversation_1,
    SkullChief_Conversation_2,
    ColorControll,
    RangeAttack1,
    RangeAttack2,
    RangeAttack3
}