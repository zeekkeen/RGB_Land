using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailMovement : MonoBehaviour{

    public float speed;
    public Transform objetive;
    public bool started = false;

    void Update(){
        if(started)
            transform.position = Vector2.MoveTowards(transform.position,objetive.position, speed * Time.deltaTime);
    }

    public void StartMovement(Transform obj, Gradient col){
        objetive = obj;
        ParticleSystem.MainModule  psMain = gameObject.GetComponent<ParticleSystem>().main;
        psMain.startColor = col.Evaluate(Random.Range(0f,1f));
        started = true;
    }
}
