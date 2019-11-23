using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour{
    
    // public List<MapPieceInfo> allMapPieces;
    public GameObject playerPin;
    public List<GameObject> allMapGameObjectPieces;
    public static MapManager instance;

    void Awake() {
        if(instance == null)
            instance = this;
    }

    void Start(){
        StartMap();
    }

    void StartMap(){
        foreach (MapPieceInfo mapPiece in GameManager.instance.playerData.allMapPieces){
            searchID( mapPiece);
        }
    }

    public void searchID(MapPieceInfo mapPieceAux){
        foreach (GameObject pieceGO in allMapGameObjectPieces){
            if(pieceGO.GetComponent<MapPiece>().mapPieceInfo.mapID == mapPieceAux.mapID){
                if(pieceGO.GetComponent<MapPiece>().mapPieceInfo.pieceID == mapPieceAux.pieceID){
                    if(mapPieceAux.visible)
                        pieceGO.SetActive(false);
                        // pieceGO.GetComponent<Image>().color = new Color(0,0,0,0);
                    break;
                }
            }
        }
    }

    public void PlayerRoom(MapPieceInfo mapPieceAux){
        foreach (GameObject pieceGO in allMapGameObjectPieces){
            if(pieceGO.GetComponent<MapPiece>().mapPieceInfo.mapID == mapPieceAux.mapID){
                if(pieceGO.GetComponent<MapPiece>().mapPieceInfo.pieceID == mapPieceAux.pieceID){
                    playerPin.transform.position = pieceGO.transform.position;
                    pieceGO.SetActive(false);
                    DiscoveredPieceOfMap(mapPieceAux);
                    StartMap();
                    break;
                }
            }
        }
    }

    void DiscoveredPieceOfMap(MapPieceInfo mapPieceAux){
        foreach (MapPieceInfo mapPiece in GameManager.instance.playerData.allMapPieces){
            if(mapPiece.mapID == mapPieceAux.mapID){
                if(mapPiece.pieceID == mapPieceAux.pieceID){
                    mapPiece.SetVisible(true);
                    break;
                }
            }
        }
    }
}

[System.Serializable]
public struct MapPieceInfo{
    public int pieceID;
    public bool visible;
    public string mapID;

    public void SetVisible(bool aux){
        visible = aux;
    }

    // public MapPieceInfo(string mapID,int pieceID, bool visible){
    //     this.pieceID = pieceID;
    //     this.visible = visible;
    //     this.mapID = mapID;
    // }
}

// [SerializeField]
// public struct Map{

//     public List<MapPieceInfo> mapPieces;

//     public Map(List<MapPieceInfo> mapPieces){
//         this.mapPieces = mapPieces;
//     }
// }