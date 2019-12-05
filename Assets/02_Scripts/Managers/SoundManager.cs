using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager: MonoBehaviour{
    
    public List<SoundAudioClip> soundAudioClipArray;
    static Dictionary<Sound,float> soundTimerDictionary;
    public Sound music;
    public static SoundManager instance;

    void Awake() {
        Initialize();
        if(instance == null)
            instance = this;
        else
            Destroy(this);
        // DontDestroyOnLoad(this);
    }
    void Start() {
        PlaySound(music);
    }
    void Initialize(){
        soundTimerDictionary = new Dictionary<Sound, float>();
        soundTimerDictionary[Sound.PlayerMove] = 0f;
    } 

    public void PlaySound(Sound sound){
        if(CanPlaySound(sound)){
            GameObject soundGameObject = new GameObject("Sound");
            AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
            audioSource.PlayOneShot(GetAudioClip(sound));
        }
    }

    AudioClip GetAudioClip(Sound sound){
        foreach(SoundAudioClip soundAudioClip in soundAudioClipArray){
            if(soundAudioClip.sound == sound)
                return soundAudioClip.audioClip;
        }
        return null;
    }

    bool CanPlaySound(Sound sound){
        switch(sound){
            default:
                return true;
            case Sound.PlayerMove:
                if(soundTimerDictionary.ContainsKey(sound)){
                    float lastTimePlayed = soundTimerDictionary[sound];
                    float playerMoveTimerMax = 0.05f;
                    if(lastTimePlayed + playerMoveTimerMax < Time.time){
                        soundTimerDictionary[sound] = Time.time;
                        return true;
                    }else
                        return false;
                }else
                    return false;
        }
    }
}
public enum Sound{
    MainMenuMusic,
    TribuInicialMusic,
    TribuRedMusic,
    TribuGreenMusic,
    TribuBlueMusic,
    PlayerAttack,
    PlayerHit,
    PlayerMove,
    PlayerDash,
    PlayerColorControll,
    PlayerJump,
    EnemyCornerGrayHit,
    EnemyCornerGrayAttack,
    EnemyCornerGrayDead,
    EnemyCornerRedHit,
    EnemyCornerRedAttack,
    EnemyCornerRedDead,
    EnemyCornerGreenHit,
    EnemyCornerGreenAttack,
    EnemyCornerGreenDead,
    EnemyCornerBlueHit,
    EnemyCornerBlueAttack,
    EnemyCornerBlueDead,
    BtnAcceptUI,
    BtnUpUI,
    BtnDownUI
}

[System.Serializable]
public struct SoundAudioClip{
    public Sound sound; 
    public AudioClip audioClip;
}
