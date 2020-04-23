using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowEffect : MonoBehaviour{
    
    [SerializeField] [Range(0f, 2f)] float lerpTime;
    [SerializeField] Color[] myColors;
    public Image mySprite;
    int colorsIndex = 0;
    float t = 0f;
    int length;

    void Start(){
        mySprite = GetComponent<Image>();
        length = myColors.Length;
    }

    void Update(){
        mySprite.color = Color.Lerp(mySprite.color, myColors[colorsIndex], lerpTime * Time.deltaTime);
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if(t > 0.9f){
            t = 0;
            colorsIndex++;
            colorsIndex = (colorsIndex >= myColors.Length) ? 0 : colorsIndex;
        }
    }
}
