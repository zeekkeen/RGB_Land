using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oclusion2D : MonoBehaviour{

    public GameObject content;
    void Start(){
        content.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "PlayerCollider")
		    content.SetActive (true);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "PlayerCollider")
		    content.SetActive(false);
    }
}
