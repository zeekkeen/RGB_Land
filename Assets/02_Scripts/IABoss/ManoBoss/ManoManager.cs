using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManoManager : StateMachineBehaviour
{
  public GameObject NPC;
  public IAMano iaMano;
  public GameObject player;
    //public List<Transform> points;
    
    public float speed = 5.0f;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        NPC = animator.gameObject;
        iaMano=NPC.GetComponent<IAMano>();
        player = NPC.GetComponent<IAMano>().GetPlayer();
        //Casa = GameObject.FindGameObjectWithTag("base");
    }
}
