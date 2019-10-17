using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : MonoBehaviour{
    
    public static GameSaveManager instance;
    public int gameSlot;

    void Awake() {
        if(instance == null)
            instance = this;
        else if(instance != this)    
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    public bool IsSaveFile(){
        return Directory.Exists(Application.persistentDataPath + "game_save");
    }

    public void SaveGame(){
        if(!IsSaveFile())
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/character_data/character_save.txt");
        var json = JsonUtility.ToJson(GameManager.instance.playerData);
        bf.Serialize(file,json);
        file.Close();
    }

    public void LoadGame(){
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/character_data/character_save.txt")){
            FileStream file = File.Create(Application.persistentDataPath + "/game_save/character_data/character_save.txt");
            JsonUtility.FromJsonOverwrite((string) bf.Deserialize(file), GameManager.instance.playerData);
            file.Close();
        }
    }
}
