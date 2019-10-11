using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyStats", menuName = "ScriptableObjects/Stats/EnemyStats")]
public class EnemyStats_SO : GeneralStats_SO {

    public float moveSpeed;
    public float attackSpeed;
    public float dazedTime;
    public float StartDazedTime = 0.6f;
    public float distance = 5f;
    public TypeOfColor typeOfColor;
}

public enum TypeOfColor{
    red,
    green,
    blue
}
