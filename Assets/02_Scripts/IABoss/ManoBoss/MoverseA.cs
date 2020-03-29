using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverseA : ManoManager
{

    public float speedD;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator,stateInfo,layerIndex);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (Vector2.Distance(iaMano.dPosition.position, NPC.transform.position) < 1.5f)
        {
            NPC.GetComponent<Rigidbody2D>().velocity=Vector2.zero;
            NPC.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
            animator.SetTrigger("Attack");
            //iaMano.WToDo(2,"Attack");
        }
       NPC.GetComponent<Rigidbody2D>().velocity=((iaMano.dPosition.position-NPC.transform.position)*speed);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    
    }


    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
