using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class Oclusion2DEnemy : MonoBehaviour{
    
    public GameObject content;
    public PandaBehaviour pBT;
    public Unit unit;
    void Start(){
        content.SetActive(false);
        pBT.enabled = false;
        unit.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "PlayerCollider"){
		    content.SetActive (true);
            unit.enabled = true;
            pBT.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "PlayerCollider"){
		    content.SetActive(false);
            pBT.enabled = false;
            unit.enabled = false;
            unit.rb.velocity = Vector2.zero;
        }
    }
}
