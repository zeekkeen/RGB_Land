using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : MonoBehaviour{
    
    public static GameSaveManager instance;
    public int gameSlot = 0;

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

    public void SaveGameSlot(){
        if(!IsSaveFile())
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/character_data/slot_" + gameSlot + ".txt");
        var json = JsonUtility.ToJson(GameManager.instance.playerData);
        bf.Serialize(file,json);
        file.Close();
        ///
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_stats"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_stats");
        BinaryFormatter bf2 = new BinaryFormatter();
        FileStream file2 = File.Create(Application.persistentDataPath + "/game_save/character_stats/slot_"+gameSlot+".txt");
        var json2 = JsonUtility.ToJson(GameManager.instance.playerStats);
        bf2.Serialize(file2,json2);
        file2.Close();
    }

    public void LoadGameSlot(){
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/character_data/slot_" + gameSlot + ".txt")){
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/character_data/slot_" + gameSlot + ".txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string) bf.Deserialize(file), GameManager.instance.playerData);
            file.Close();
        }
        ///
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_stats"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_stats");
        BinaryFormatter bf2 = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/character_stats/slot_"+gameSlot+".txt")){
            FileStream file2 = File.Open(Application.persistentDataPath + "/game_save/character_stats/slot_" + gameSlot + ".txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string) bf2.Deserialize(file2), GameManager.instance.playerStats);
            file2.Close();
        }
    }

    public void SaveGameSlot(int slot){
        if(!IsSaveFile())
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/character_data/slot_"+slot+".txt");
        var json = JsonUtility.ToJson(GameManager.instance.playerData);
        bf.Serialize(file,json);
        file.Close();
        ///
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_stats"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_stats");
        BinaryFormatter bf2 = new BinaryFormatter();
        FileStream file2 = File.Create(Application.persistentDataPath + "/game_save/character_stats/slot_"+slot+".txt");
        var json2 = JsonUtility.ToJson(GameManager.instance.playerStats);
        bf2.Serialize(file2,json2);
        file2.Close();
    }

    public void LoadGameSlot(int slot){
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_data"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_data");
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/character_data/slot_"+slot+".txt")){
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/character_data/slot_" + slot + ".txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string) bf.Deserialize(file), GameManager.instance.playerData);
            file.Close();
        }
        ///
        if(!Directory.Exists(Application.persistentDataPath + "/game_save/character_stats"))
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/character_stats");
        BinaryFormatter bf2 = new BinaryFormatter();
        if(File.Exists(Application.persistentDataPath + "/game_save/character_stats/slot_"+slot+".txt")){
            FileStream file2 = File.Open(Application.persistentDataPath + "/game_save/character_stats/slot_" + slot + ".txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string) bf2.Deserialize(file2), GameManager.instance.playerStats);
            file2.Close();
        }
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
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/character_data/character_save.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string) bf.Deserialize(file), GameManager.instance.playerData);
            file.Close();
        }
    }
}
