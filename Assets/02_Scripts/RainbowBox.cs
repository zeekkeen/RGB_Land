using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RainbowBox : MonoBehaviour, ITakeDamage{

     RipplePostProcessor camRipple;
    public float health = 1000f;
    public GameObject hitEffect, endEffect;
    [SerializeField] [Range(0f, 2f)] float lerpTime;
    [SerializeField] Color[] myColors;
    SpriteRenderer mySprite;
    int colorsIndex = 0;
    float t = 0f;
    int length;
    float amount;

    void Start(){
        camRipple = Camera.main.GetComponent<RipplePostProcessor>();
        mySprite = GetComponent<SpriteRenderer>();
        length = myColors.Length;
        amount = Random.Range(0.1f, 0.4f);
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

    public void TakeDamage(int damage){
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            health -= damage;
            if(health <= 0){
                health = 0;
                camRipple.RippleEffect();
                Instantiate(endEffect, transform.position, Quaternion.identity);
                ColorSphereManager.instance.RefreshUI(amount);
                Destroy(gameObject);
            }
    }
}
