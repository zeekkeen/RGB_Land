using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedRockProjectile : MonoBehaviour{
    
    public float lifeTime;
    [HideInInspector]
    public int damage;
    Animator animator;

    void Start(){
        StartCoroutine(DestroyThis());
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other) {
        animator.SetTrigger("changeState");
    }

    IEnumerator DestroyThis(){
        yield return new WaitForSeconds (lifeTime);
        animator.SetTrigger("changeState");
        yield return new WaitForSeconds (lifeTime / 2);
     Destroy(gameObject);
    }
}
