using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour{
    
    public Animator anim;
    public bool active = false;

    void Start(){
        anim = GetComponentInChildren<Animator>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!active && other.tag == "Player"){
            GameManager.instance.SaveGame(transform.position);
            anim.SetBool("Active",true);
            active = true;
        }
    }
}
