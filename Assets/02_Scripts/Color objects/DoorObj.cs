using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorObj : ColorObject{

    public Animator anim;

    public override void PaintedAction(){
        anim.SetBool("painted",true);
        GetComponent<BoxCollider2D>().isTrigger = true;
    }

    public override void UnPaintedAction(){
        anim.SetBool("painted",false);
        GetComponent<BoxCollider2D>().isTrigger = false;
    }
}
