using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour,ITriggerObject{

    public List <Dialog_SO> NewNPCDialog;
    public int index;
    int GetIndex(){
        for (int i = NewNPCDialog.Count - 1; i >= 0; i--){
            if(NewNPCDialog[i].achievementRequired == Achievement.NULL || GameManager.instance.SearchAchievement(NewNPCDialog[i].achievementRequired)){
                return i;
            }
        }
        return -1;
    }

    public void EnterActions(){
        index = GetIndex();
        if(index != -1){
            // NewNPCDialog[index].sentences = DialogSystem.GetDialog(NewNPCDialog[index].dialogCode);
            NewNPCDialog[index].completed = false;
            // if(NewNPCDialog[index].index != NewNPCDialog[index].sentences.Count - 1)
            DialogSystem.instance.OpenPopUp(NewNPCDialog[index],this);
        }
    }

    public void ExitActions(){
        if(index != -1){
            // QuestsManager.instance.VerifyObjetive(GoalType.conversation,0);
            DialogSystem.instance.ClosePopUp();
            if(NewNPCDialog[index].index == NewNPCDialog[index].sentences.Count - 1){
                if(NewNPCDialog[index].completed && NewNPCDialog[index].achievement != Achievement.NULL){
                    GameManager.instance.AddAchievement(NewNPCDialog[index].achievement);
                    // QuestsManager.instance.VerifyAchievement(NewNPCDialog[index].achievement);
                }
                // if(NewNPCDialog[index].completed)
                    // if(NewNPCDialog[index].myQuest.questID != -1)
                    //     QuestsManager.instance.AddQuest(NewNPCDialog[index].myQuest);
                    // GameSaveManager.instance.SaveGame();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Flip(other.transform);
            EnterActions();
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Flip(other.transform);
            ExitActions();
        }
    }

    void Flip(Transform other){
            if(other.transform.rotation.y > 0)
                transform.rotation = Quaternion.Euler(0,0,0);
            else
                transform.rotation = Quaternion.Euler(0,180,0);
    }
}
