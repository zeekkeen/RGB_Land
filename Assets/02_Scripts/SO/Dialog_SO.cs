using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialog", menuName = "ScriptableObjects/Dialog")]
public class Dialog_SO : ScriptableObject {
    
    public List<string> sentences;
    public int index = -1;
    // public Quest myQuest;
    public bool completed = false;
    public Achievement achievementRequired;
    public Achievement achievement;
}
