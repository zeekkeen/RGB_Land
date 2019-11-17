using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog")]
public class Dialog_SO : ScriptableObject {
    
    public string[] sentences;
    public int index = -1;
}
