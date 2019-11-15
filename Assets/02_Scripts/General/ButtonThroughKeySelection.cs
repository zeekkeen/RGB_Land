using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonThroughKeySelection : MonoBehaviour{

    PlayerControls inputAction;

    void Awake() {
        inputAction = new PlayerControls();
        inputAction.GamePlay.anykey.performed += ctx => ChangeFocus2();
    }

    void OnEnable() {
        inputAction.Enable();
    }

    void OnDisable() {
        inputAction.Disable();
    }

    public void ChangeFocus(){
        EventSystem.current.SetSelectedGameObject(this.gameObject);
    }

    public void ChangeFocus2(){
        if(EventSystem.current.currentSelectedGameObject == null){
            EventSystem.current.SetSelectedGameObject(this.gameObject);
        }
    }
}
