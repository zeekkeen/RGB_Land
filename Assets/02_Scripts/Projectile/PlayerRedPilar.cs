using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRedPilar : MonoBehaviour{

    public float damageMultiplier;
    
    void OnTriggerEnter2D(Collider2D other) {
        ITakeDamage takeDamage = other.gameObject.GetComponent<ITakeDamage>();
		if(takeDamage != null)
			takeDamage.TakeDamage((int)(GameManager.instance.playerStats.damage * damageMultiplier));
    }
}
