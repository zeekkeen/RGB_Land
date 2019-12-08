using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData_SO : ScriptableObject {
    
    //attributes
    // public PlayerStats_SO playerStats;
    // public PlayerStats_SO playerInitialStats;
    public Vector3 lastPosition;
    public string lastLevel;
    public List<MapPieceInfo> initialAllMapPieces;
    public List<MapPieceInfo> allMapPieces;
    public Vector3 playerPinPosition;
    public List<Achievement> achievements = new List<Achievement>();
    public bool inPause;
}
