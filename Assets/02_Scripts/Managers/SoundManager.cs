using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour{
    
    public List<Sound> soundAudioClipArray;
    static Dictionary<string,float> soundTimerDictionary;
    public string music;
    public static SoundManager instance;

    void Awake() {
        Initialize();
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
        // DontDestroyOnLoad(this);
        foreach (Sound s in soundAudioClipArray){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }
    void Start() {
        PlaySound(music);
    }
    void Initialize(){
        soundTimerDictionary = new Dictionary<string, float>();
        soundTimerDictionary["PlayerMove"] = 0f;
        soundTimerDictionary["UpBtn"] = 0f;
        soundTimerDictionary["DownBtn"] = 0f;
    } 

    public void PlaySound(string name){
        if(CanPlaySound(name)){
            Sound s = GetAudioSound(name);
            if(s == null)
                return;
            s.source.Play();
        }
    }

    // public void PlaySound(string name){
    //     if(CanPlaySound(name)){
    //         GameObject soundGameObject = new GameObject("Sound");
    //         AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
    //         audioSource.PlayOneShot(GetAudioClip(name));
    //     }
    // }

    Sound GetAudioSound(string name){
        foreach(Sound soundAudioClip in soundAudioClipArray){
            if(soundAudioClip.Name == name)
                return soundAudioClip;
        }
        return null;
    }

    AudioClip GetAudioClip(string name){
        foreach(Sound soundAudioClip in soundAudioClipArray){
            if(soundAudioClip.Name == name)
                return soundAudioClip.audioClip;
        }
        return null;
    }

    bool CanPlaySound(string name){
        switch(name){
            default:
                return true;
            case "PlayerMove":
                if(soundTimerDictionary.ContainsKey(name)){
                    float lastTimePlayed = soundTimerDictionary[name];
                    float playerMoveTimerMax = 0.05f;
                    if(lastTimePlayed + playerMoveTimerMax < Time.time){
                        soundTimerDictionary[name] = Time.time;
                        return true;
                    }else
                        return false;
                }else
                    return false;
            case "UpBtn":
                if(soundTimerDictionary.ContainsKey(name)){
                    float lastTimePlayed = soundTimerDictionary[name];
                    float playerMoveTimerMax = 0.05f;
                    if(lastTimePlayed + playerMoveTimerMax < Time.time){
                        soundTimerDictionary[name] = Time.time;
                        return true;
                    }else
                        return false;
                }else
                    return false;
            case "DownBtn":
                if(soundTimerDictionary.ContainsKey(name)){
                    float lastTimePlayed = soundTimerDictionary[name];
                    float playerMoveTimerMax = 0.05f;
                    if(lastTimePlayed + playerMoveTimerMax < Time.time){
                        soundTimerDictionary[name] = Time.time;
                        return true;
                    }else
                        return false;
                }else
                    return false;
        }
    }
}

[System.Serializable]
public class Sound{
    public string Name; 
    public AudioClip audioClip;
    [Range (0f, 1f)]
    public float volume;
    [Range (.1f, 3f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
}
