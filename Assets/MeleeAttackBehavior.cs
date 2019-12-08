using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBehavior : StateMachineBehaviour{
    
    private float timer;
    public float minTime;
    public float maxTime;
    public LayerMask whatIsEnemies;
    public  Vector2 attackRange;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        timer = Random.Range(minTime, maxTime);
	}

	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (timer <= 0)
        {
            animator.SetTrigger("idle");
        }
        else {
            timer -= Time.deltaTime;
        }
	}

	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        Collider2D[] enemiesToDamage;
        enemiesToDamage = Physics2D.OverlapBoxAll(animator.transform.position, attackRange, 0, whatIsEnemies);
        for(int i=0;i<enemiesToDamage.Length;i++){
            ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
            if(takeDamage != null)
                takeDamage.TakeDamage(1);
        }
	}
}
