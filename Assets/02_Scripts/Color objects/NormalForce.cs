using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalForce : MonoBehaviour{

    public float force;

    void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player" ){
			Debug.Log("entro");
			Vector3 vec = other.transform.position - transform.position;
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(vec.normalized * force);
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
            ITakeDamage takeDamage = other.GetComponent<ITakeDamage>();
            if(takeDamage != null)
                takeDamage.TakeDamage(1);
		}
	}
}
