using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapPiece : MonoBehaviour{
    
    public MapPieceInfo mapPieceInfo;

    void OnEnable() {
        GetComponent<Animator>().SetInteger("State", mapPieceInfo.state);
    }
}