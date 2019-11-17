using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour,ITriggerObject{
    
    public Dialog_SO NPCDialog;

    public void EnterActions(){
        if(NPCDialog.index != NPCDialog.sentences.Length - 1)
            DialogSystem.instance.OpenPopUp(NPCDialog);
    }

    public void ExitActions(){
        DialogSystem.instance.ClosePopUp();
        if(NPCDialog.index == NPCDialog.sentences.Length - 1){
            //NPC actions 
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player")
            EnterActions();
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player")
            ExitActions();
    }
}
