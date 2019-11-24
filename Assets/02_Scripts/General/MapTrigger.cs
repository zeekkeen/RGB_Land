using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTrigger : MonoBehaviour{
    
    public MapPieceInfo mapPieceInfo;
    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            mapPieceInfo.SetVisible();
            if(MapManager.instance != null)
                MapManager.instance.PlayerRoom(mapPieceInfo);
        }
    }
}
