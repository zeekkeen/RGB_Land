using UnityEngine;

[CreateAssetMenu(fileName = "NewStats", menuName = "ScriptableObjects/Stats/BaseStats")]
public class GeneralStats_SO : ScriptableObject {

    public int maxHealth = 100;
    public int currentHealth = 100;
    public int damage = 0;
}
