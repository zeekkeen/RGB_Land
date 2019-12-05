using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSphereManager : MonoBehaviour{
    
    public GameObject colorSpherePrefab;
    public GameObject colorSphereContainer;
    public static ColorSphereManager instance;

    void Start(){
        instance = this;
        RefreshUI();
    }

    public void RefreshUI(){
        foreach (Transform child in colorSphereContainer.transform) {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < GameManager.instance.playerStats.colorSphereCount; i++){
            GameObject sphere = (GameObject)Instantiate(colorSpherePrefab,colorSphereContainer.transform.position,Quaternion.identity);
            sphere.transform.parent = colorSphereContainer.transform;
        }
    }
}
