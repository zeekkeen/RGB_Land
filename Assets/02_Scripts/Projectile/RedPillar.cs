﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedPillar : MonoBehaviour{
    
    public int damage;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player")
        other.GetComponent<ITakeDamage>().TakeDamage(damage);
    }
}
