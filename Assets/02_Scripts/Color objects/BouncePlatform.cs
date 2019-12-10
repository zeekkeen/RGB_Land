using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BouncePlatform : ColorObject {

	public float force;

	public override void PaintedAction(){
        base.PaintedAction();
    }

    public override void UnPaintedAction(){
        base.UnPaintedAction();
    }

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player" && !painted){
			// Debug.Log("entro");
			Vector3 vec = other.transform.position - transform.position;
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(vec.normalized * force);
			// other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
		}
	}
}