using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehavior : StateMachineBehaviour{
    
    private float timer;
    public float minTime;
    public float maxTime;
    private int rand;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator.gameObject.GetComponentInParent<Rigidbody2D>().velocity = Vector2.zero;
        timer = Random.Range(minTime, maxTime);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (timer <= 0)
        {
            rand = Random.Range(0, 2);
            if (rand == 0)
            {
                animator.SetTrigger("caminar");
            }
            else {
                animator.SetTrigger("jump");
            }
        }
        else {
            timer -= Time.deltaTime;
        }
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
