using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSlash : MonoBehaviour{
    
    public PlayerAttack player;

    private void OnTriggerEnter2D(Collider2D other) {
                Debug.Log("entroooo trigger " + other.gameObject.name);
        if(player.slashing){
            ITakeDamage takeDamage = other.gameObject.GetComponent<ITakeDamage>();
            if(takeDamage != null){
                takeDamage.TakeDamage((int)(GameManager.instance.playerStats.damage));
            }
        }
	}
}
