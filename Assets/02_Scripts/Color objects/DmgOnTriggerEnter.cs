using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgOnTriggerEnter : MonoBehaviour{
    
    void OnTriggerEnter2D(Collider2D other) {
        ITakeDamage takeDamage = other.GetComponent<ITakeDamage>();
        if(takeDamage != null)
            takeDamage.TakeDamage(1);
    }
}
