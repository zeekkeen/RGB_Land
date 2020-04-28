using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour{
    
    public Animator anim;
    public bool active = false;
    public string level;
    public Animator infoObj;

    void OnTriggerEnter2D(Collider2D other) {
        if(!active && other.gameObject.tag == "Player"){
            // MapManager.instance.myAllMapPieces = GameManager.instance.playerData.allMapPieces;
            // GameManager.instance.SaveGame(transform.position, level);
            GameManager.instance.nearCheckPoint = this;
            infoObj.SetBool("Active", true);
            active = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if(active && other.gameObject.tag == "Player"){
            GameManager.instance.nearCheckPoint = null;
            infoObj.SetBool("Active", false);
            active = false;
        }
    }

    public void ActivateAnim(bool a){
        anim.SetBool("Active", a);
    }

    public void SaveGame(){
        GameManager.instance.SaveGame(transform.position, level);
    }
}