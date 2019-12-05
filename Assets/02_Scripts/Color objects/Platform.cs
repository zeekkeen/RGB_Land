using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platform : ColorObject {

	public Transform target;
	public float speed;
	Vector3 start, end;

	void Start() {
		if(target != null){
			target.parent = null;
			start = transform.position;
			end = target.position;
		}
	}
	void FixedUpdate() {
		if(painted){
			if(target != null){
				transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
			}
			if(transform.position == target.position){
				target.position = (target.position == start) ? end : start;
			}
		}
	}

	public override void PaintedAction(){
		base.PaintedAction();
    }

    public override void UnPaintedAction(){
		base.UnPaintedAction();
    }
}