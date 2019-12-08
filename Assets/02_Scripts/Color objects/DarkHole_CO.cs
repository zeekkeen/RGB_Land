using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DarkHole_CO : ColorObject {

	public float force;

	public override void PaintedAction(){
        base.PaintedAction();
    }

    public override void UnPaintedAction(){
        base.UnPaintedAction();
    }

	private void OnTriggerStay2D(Collider2D other) {
		if(other.gameObject.tag == "Player" && !painted){
			
			Vector3 vec = other.transform.position - transform.position;
			float distance = Vector3.Distance(other.transform.position, transform.position);
			other.gameObject.GetComponent<Rigidbody2D>().AddForce(vec.normalized * (force / distance) * (-1));
			// other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
		}
	}
}