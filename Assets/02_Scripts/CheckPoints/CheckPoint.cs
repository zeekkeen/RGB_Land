using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour{
    
    public Animator anim;
    public bool active = false;
    public string level;

    void Start(){
        anim = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!active && other.gameObject.tag == "Player"){
            // MapManager.instance.myAllMapPieces = GameManager.instance.playerData.allMapPieces;
            GameManager.instance.SaveGame(transform.position, level);
            anim.SetBool("Active",true);
            active = true;
        }
    }
}