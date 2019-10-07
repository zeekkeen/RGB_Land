using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalController : MonoBehaviour{
    // public Vector3 dir;
    public float moveSpeed = 5f, rotationTimer = 0, rotationTime = 2.5f;
    bool movingRight = false;
    SpriteRenderer gemRenderer;
    public GameObject startPosition;
    public Vector2 attackRange = new Vector2(7,7);
    public LayerMask whatIsColorObject;
    void Start(){
        gemRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Update(){
        //RotateAround();
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

    public void ColorSearch(){
        Collider2D[] colorObjects;
        colorObjects = Physics2D.OverlapBoxAll(startPosition.transform.position,attackRange,0,whatIsColorObject);
        if(colorObjects != null)
            for(int i=0;i<colorObjects.Length;i++){
                colorObjects[i].GetComponent<ColorObject>().SwitchColor();
            }
            
    }

    void OnDrawGizmosSelected(){
        Gizmos.color=Color.yellow;
        //Gizmos.DrawWireSphere(attackPos.position,attackRange);
        // Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
        Gizmos.DrawWireCube(transform.position,new Vector3(attackRange.x,attackRange.y,1));
    }
}