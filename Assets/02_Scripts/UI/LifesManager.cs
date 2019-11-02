using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifesManager : MonoBehaviour{
    
    public GameObject lifePrefab;
    public GameObject lifeContainer;
    public static LifesManager instance;

    void Start(){
        instance = this;
        RefreshUI();
    }

    public void RefreshUI(){
        foreach (Transform child in lifeContainer.transform) {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < GameManager.instance.playerStats.currentHealth; i++){
            GameObject life = (GameObject)Instantiate(lifePrefab,lifeContainer.transform.position,Quaternion.identity);
            life.transform.parent = lifeContainer.transform;
        }
    }
}
