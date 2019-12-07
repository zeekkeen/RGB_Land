using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour{
    
    public Animator dialogPanel;
    public Text textDisplay;
    public Dialog_SO dialog;
    public float typingSpeed = 0.02f;
    PlayerControls inputAction;
    public static DialogSystem instance;
    ITriggerObject trigger;

    void Awake() {
        instance = this;
        inputAction = new PlayerControls();
        inputAction.GamePlay.Dialog.performed += ctx => NextSentence();
    }

    void OnEnable() {
        inputAction.Enable();
    }

    void OnDisable() {
        inputAction.Disable();
    }

    IEnumerator Type(){
        if(dialog == null)
            yield break;
        else{
            foreach(char letter in dialog.sentences[dialog.index].ToCharArray()){
                if(dialog == null)
                    yield break;
                if(textDisplay.text != dialog.sentences[dialog.index])
                    textDisplay.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }

    public void NextSentence(){
        if(dialog != null && trigger != null){
            if(((textDisplay.text == dialog.sentences[(dialog.index == -1) ? dialog.index + 1 : dialog.index])) || dialog.index == -1){
                if(dialog.index < dialog.sentences.Count - 1){
                    dialog.index++;
                    textDisplay.text = "";
                    StartCoroutine(Type());
                }else{
                    textDisplay.text = "";
                    dialogPanel.SetBool("active",false);
                    trigger.ExitActions();

                }
            }else{
                StopAllCoroutines();
                textDisplay.text = "";
                textDisplay.text = dialog.sentences[dialog.index];
            }
        }
    }
    public void OpenPopUp(Dialog_SO aux, ITriggerObject aux2){
        if(aux != null && aux2 != null){
            if(aux.index != aux.sentences.Count - 1){
                aux.index = -1;
                dialog = aux;
                trigger = aux2;
                dialogPanel.SetBool("active",true);
                NextSentence();
            }
        }
    }

    public void ClosePopUp(){
        if(dialog != null){
            dialogPanel.SetBool("active",false);
            dialog = null;
        }
    }
}
