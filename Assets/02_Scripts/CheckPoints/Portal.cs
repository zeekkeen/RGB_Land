using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour{
    
    public string destiny;
    public Vector3 newLastPosition;
    public Achievement achievementRequired;

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            if(achievementRequired == Achievement.NULL || GameManager.instance.SearchAchievement(achievementRequired)){
                GameManager.instance.SaveGame(newLastPosition, destiny);
                TransitionManager.instance.LoadSceneWithTransition(destiny);
            }
        }
    }
}