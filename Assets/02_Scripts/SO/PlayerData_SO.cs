using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData_SO : ScriptableObject {
    
    //attributes
    public PlayerStats_SO playerStats;
    public PlayerStats_SO playerInitialStats;
    public Transform lastPosition;
    public Transform initialPosition;

}
