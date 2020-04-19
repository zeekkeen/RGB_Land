using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlueManager : StateMachineBehaviour
{
    public IABlueBoss iABlue;
    public BlueBossManager manager;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject NPC=animator.gameObject;
        iABlue=NPC.GetComponent<IABlueBoss>();
        manager= iABlue.manager;
    }
}
