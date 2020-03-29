using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacleadaBehavior : StateMachineBehaviour{
    
    private float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos;
    public float speed;
    private int rand;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.GetComponentInParent<Rigidbody2D>().AddForce(animator.transform.localScale.x *Vector2.right* speed,ForceMode2D.Impulse);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
