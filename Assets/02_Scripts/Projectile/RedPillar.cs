using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPillar : MonoBehaviour{
    
    public float lifeTime;
    [HideInInspector]
    public int damage;

    void Start(){
        StartCoroutine(DestroyThis());
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            other.GetComponent<ITakeDamage>().TakeDamage(damage);
            // Destroy(this.gameObject);
        }
    }

    IEnumerator DestroyThis(){
        yield return new WaitForSeconds (lifeTime);
     Destroy(gameObject);
    }
}
