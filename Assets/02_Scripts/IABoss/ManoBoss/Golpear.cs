using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golpear : ManoManager
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
         base.OnStateEnter(animator, stateInfo, layerIndex);
         NPC.GetComponent<Rigidbody2D>().bodyType=RigidbodyType2D.Dynamic;
         NPC.GetComponent<Rigidbody2D>().AddForce((player.transform.position-NPC.transform.position)*speed,ForceMode2D.Impulse);
         iaMano.puedeAtacar=true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        iaMano.puedeAtacar=false;
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    // override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    // {
      
    // }

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
