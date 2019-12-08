using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpBehavior : StateMachineBehaviour{

    private float timer;
    public float minTime;
    public float maxTime;

    private Transform playerPos;
    public float speed;
    private int rand;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
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
                animator.SetTrigger("idle");
            }
        }
        else {
            timer -= Time.deltaTime;
        }
        if(animator.transform.position.x < playerPos.position.x)
            animator.transform.localScale = new Vector2(1,1);
        else
            animator.transform.localScale = new Vector2(-1,1);
        Vector2 target = new Vector2(playerPos.position.x, animator.transform.position.y);
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, target, speed * Time.deltaTime);
        animator.gameObject.GetComponentInParent<Rigidbody2D>().AddForce(animator.transform.localScale.x > 0 ? Vector2.right * speed: Vector2.left * speed);
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	
	}
}
