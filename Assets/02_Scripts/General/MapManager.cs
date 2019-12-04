using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour{
    
    public List<MapPieceInfo> myAllMapPieces;
    public GameObject playerPin;
    public List<GameObject> allMapGameObjectPieces;
    public static MapManager instance;

    void Awake() {
        if(instance == null)
            instance = this;
    }

    void Start(){
        myAllMapPieces = GameManager.instance.playerData.allMapPieces;
        StartMap();
        playerPin.transform.localPosition = GameManager.instance.playerData.playerPinPosition;
        // playerPin.transform.localPosition.Set(0,0,0);
    }

    void StartMap(){
        foreach (MapPieceInfo mapPiece in myAllMapPieces){
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
        for (int i = 0; i < myAllMapPieces.Count-1; i++){
            if(myAllMapPieces[i].mapID == mapPieceAux.mapID){
                if(myAllMapPieces[i].pieceID == mapPieceAux.pieceID){
                    // Debug.Log(myAllMapPieces[i].visible);
                    // myAllMapPieces[i].SetVisible();
                    myAllMapPieces[i] = mapPieceAux;
                    // Debug.Log(myAllMapPieces[i].visible);
                    break;
                }
            }
            
        }
    }

    // void DiscoveredPieceOfMap(MapPieceInfo mapPieceAux){
    //     foreach (MapPieceInfo mapPiece in myAllMapPieces){
    //         if(mapPiece.mapID == mapPieceAux.mapID){
    //             if(mapPiece.pieceID == mapPieceAux.pieceID){
    //                 Debug.Log(mapPiece.visible);
    //                 mapPiece.SetVisible(true);
    //                 Debug.Log(mapPiece.visible);
    //                 break;
    //             }
    //         }
    //     }
    // }
}

[System.Serializable]
public struct MapPieceInfo{
    public int pieceID;
    public bool visible;
    public string mapID;

    public void SetVisible(){
        visible = true;
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