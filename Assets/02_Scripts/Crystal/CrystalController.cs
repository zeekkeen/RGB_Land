using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour{
    public Vector3 dir;
    public float moveSpeed = 5f, rotationTimer = 0, rotationTime = 2.5f;
    bool movingRight = false;
    SpriteRenderer gemRenderer;
    void Start(){
        gemRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update(){
        RotateAround();
    }

    void RotateAround(){
        rotationTimer += Time.deltaTime;
        if(movingRight){
            gemRenderer.sortingOrder = 10;
            transform.Translate(moveSpeed * Time.deltaTime,0,0,Space.Self);
        }else {
            gemRenderer.sortingOrder = 0;
            transform.Translate(-moveSpeed * Time.deltaTime,0,0,Space.Self);
        }
        if(rotationTimer >= rotationTime){
            movingRight = !movingRight;
            rotationTimer = 0;
        }
        //transform.RotateAround(transform.parent.position,dir,rotationSpeed*Time.deltaTime);
    }

}