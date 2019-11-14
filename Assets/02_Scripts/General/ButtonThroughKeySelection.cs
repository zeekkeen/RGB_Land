using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonThroughKeySelection : MonoBehaviour{

    public bool active = true;

    public void Update(){
        if (Input.anyKeyDown && !active){
            ChangeFocus();
            Debug.Log("any key");
        }
        if (Input.GetMouseButton(0) && active){
            active = false;
            Debug.Log("mouse");
        }
    }

    public void ChangeFocus(){
        EventSystem.current.SetSelectedGameObject(this.gameObject);
        active = true;
    }
}
