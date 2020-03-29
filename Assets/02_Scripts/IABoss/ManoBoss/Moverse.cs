using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverse : ManoManager
{
    public GameObject[] waypoints;
    int currentWP;

    void Awake()
    {
        
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       base.OnStateEnter(animator,stateInfo,layerIndex);
       waypoints = NPC.GetComponent<IAMano>().waypoints;
       currentWP = 0;
       NPC.GetComponent<IAMano>().WAtack();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if (waypoints.Length == 0)
        {
            return;
        }
        if (Vector2.Distance(waypoints[currentWP].transform.position, NPC.transform.position) < 3f)
        {
            currentWP++;
            if (currentWP >= waypoints.Length)
            {
                currentWP = 0;
            }
        }

        //var direction = waypoints[currentWP].transform.position - NPC.transform.position;
        //Vector2.MoveTowards(NPC.transform.position,waypoints[currentWP].transform.position,speed*10);
        NPC.GetComponent<Rigidbody2D>().velocity=((waypoints[currentWP].transform.position-NPC.transform.position)*(speed/7));
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
