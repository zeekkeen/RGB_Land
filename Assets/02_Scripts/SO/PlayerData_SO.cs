using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData")]
public class PlayerData_SO : ScriptableObject {
    
    //attributes
    public PlayerStats_SO playerStats;
    public Transform lastPosition;

}
