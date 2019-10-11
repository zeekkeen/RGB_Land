using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "ScriptableObjects/Stats/PlayerStats")]
public class PlayerStats_SO : GeneralStats_SO{
    
    //movement
    public float moveSpeed;
    public float attackSpeed;
    //check if is grounded
    public bool isGrounded;
    public float checkRadius;
    //jump
    public float jumpForce;
    public float jumpTime;
    public float jumpTimeCounter;
    public bool isJumping;
    public bool doubleJump;
    //player habilities unlocked
    public bool dash;
    public bool stealAndGiveColor;
    public bool rangedAttack;
    //player attack
    public float timeBtwMeleeAttack,timeBtwPowerUse;
    public float startTimeBtwMeleeAttack = 0.3f,startTimeBtwPowerUse = 0.3f;
    public Vector2 attackRange;
    public ActivePower activePower;
    public float dashTime;
    public float dashSpeed, startDashTime;

}
public enum ActivePower{
    rangedAttack = 0,
    dash = 1,
    colorControll = 2,
    none = 3
}
