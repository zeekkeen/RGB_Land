using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorSphereManager : MonoBehaviour{
    
    public List<Image> colorSpheres;
    public static ColorSphereManager instance;

    void Start(){
        instance = this;
        RefreshUI(0);
    }

    public void RefreshUI(float value){
        GameManager.instance.playerStats.colorSphereCount += (value);
        if(GameManager.instance.playerStats.colorSphereCount > 4){
            colorSpheres[0].fillAmount = 1;
            colorSpheres[1].fillAmount = 1;
            colorSpheres[2].fillAmount = 1;
            colorSpheres[3].fillAmount = 1;
            colorSpheres[4].fillAmount = GameManager.instance.playerStats.colorSphereCount - 4;
        }else if(GameManager.instance.playerStats.colorSphereCount > 3){
            colorSpheres[0].fillAmount = 1;
            colorSpheres[1].fillAmount = 1;
            colorSpheres[2].fillAmount = 1;
            colorSpheres[3].fillAmount = GameManager.instance.playerStats.colorSphereCount - 3;
            colorSpheres[4].fillAmount = 0;
        }else if(GameManager.instance.playerStats.colorSphereCount > 2){
            colorSpheres[0].fillAmount = 1;
            colorSpheres[1].fillAmount = 1;
            colorSpheres[2].fillAmount = GameManager.instance.playerStats.colorSphereCount - 2;
            colorSpheres[3].fillAmount = 0;
            colorSpheres[4].fillAmount = 0;
        }else if(GameManager.instance.playerStats.colorSphereCount > 1){
            colorSpheres[0].fillAmount = 1;
            colorSpheres[1].fillAmount = GameManager.instance.playerStats.colorSphereCount - 1;
            colorSpheres[2].fillAmount = 0;
            colorSpheres[3].fillAmount = 0;
            colorSpheres[4].fillAmount = 0;
        }else{
            colorSpheres[0].fillAmount = GameManager.instance.playerStats.colorSphereCount;
            colorSpheres[1].fillAmount = 0;
            colorSpheres[2].fillAmount = 0;
            colorSpheres[3].fillAmount = 0;
            colorSpheres[4].fillAmount = 0;
        }
    }
}