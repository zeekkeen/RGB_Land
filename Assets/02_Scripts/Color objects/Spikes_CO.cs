using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes_CO : ColorObject{

    public GameObject spike1, spike2;

    void Start() {
        Debug.Log("entro al start");
        spike1.GetComponent<Animator>().SetBool("painted",true);
        spike2.GetComponent<Animator>().SetBool("painted",false);
    }

    public override void PaintedAction(){
        base.PaintedAction();
        spike1.GetComponent<Animator>().SetBool("painted",true);
        spike2.GetComponent<Animator>().SetBool("painted",false);
    }

    public override void UnPaintedAction(){
        base.UnPaintedAction();
        spike1.GetComponent<Animator>().SetBool("painted",false);
        spike2.GetComponent<Animator>().SetBool("painted",true);
    }
}
