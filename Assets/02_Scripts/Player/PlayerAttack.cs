using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour{

    Rigidbody2D rb;
    float timeBtwMeleeAttack,timeBtwPowerUse;
    public float startTimeBtwMeleeAttack=0.3f,startTimeBtwPowerUse=0.3f;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    Animator camAnim,playerAnim;
    // public float attackRange;
    public Vector2 attackRange;
    public int damage;
    public GameObject proyectile;
    public Direction attackDirection, dashDirection;
    public ActivePower activePower;
    float dashTime;
    bool dash = false, facingRight = true;
    public float dashSpeed, startDashTime;
    public GameObject dashEffect;
    public CrystalController crystal;

    public enum Direction{
        top,
        down,
        side
    }

    public enum ActivePower{
        rangedAttack,
        dash,
        colorControll,
        none
    }

    void Start (){
        rb=GetComponent<Rigidbody2D>();
        camAnim=Camera.main.GetComponent<Animator>();
        playerAnim=GetComponentInChildren<Animator>();
        attackDirection = Direction.side;
        dashDirection = Direction.side;
        activePower = 0;
        crystal = GameObject.FindGameObjectWithTag("Crystal").GetComponent<CrystalController>();
    }

    void Update(){
        float moveInput = Input.GetAxisRaw("Vertical");
        if(moveInput == 0)
            attackDirection = Direction.side;
        if(moveInput < 0)
            attackDirection = Direction.down;
        else if(moveInput > 0)
            attackDirection = Direction.top;

        if(!dash){
            if(moveInput == 0)
                dashDirection = Direction.side;
            if(moveInput < 0)
                dashDirection = Direction.down;
            else if(moveInput > 0)
                dashDirection = Direction.top;
        }

        if(timeBtwMeleeAttack <= 0){
            if(Input.GetKeyDown(KeyCode.X)){
                Collider2D[] enemiesToDamage;
                switch(attackDirection){
                    case Direction.side:
                        enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            enemiesToDamage[i].GetComponent<Enemy1>().TakeDamage(damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                    case Direction.top:
                        enemiesToDamage=Physics2D.OverlapBoxAll(transform.position + new Vector3(0,3f,0),attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            enemiesToDamage[i].GetComponent<Enemy1>().TakeDamage(damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                    case Direction.down:
                        enemiesToDamage=Physics2D.OverlapBoxAll(transform.position + new Vector3(0,-1.5f,0),attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            enemiesToDamage[i].GetComponent<Enemy1>().TakeDamage(damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                }
                timeBtwMeleeAttack = startTimeBtwMeleeAttack;
                rb.velocity = Vector2.zero;
            }
        }else
        {
            timeBtwMeleeAttack-=Time.deltaTime;
        }
        if(timeBtwPowerUse <= 0)
        {
            if(Input.GetKeyDown(KeyCode.C)){
                switch (activePower){
                    case ActivePower.rangedAttack:
                        GameObject instance;
                        switch(attackDirection){
                            case Direction.side:
                                instance = (GameObject) Instantiate(proyectile,attackPos.position,transform.rotation);
                                playerAnim.SetTrigger("attack");
                                    break;
                            case Direction.top:
                                instance = (GameObject) Instantiate(proyectile,transform.position + new Vector3(0,3f,0),Quaternion.Euler(0,0,90));
                                playerAnim.SetTrigger("attack");
                                    break;
                            case Direction.down:
                                instance = (GameObject) Instantiate(proyectile,transform.position + new Vector3(0,-1.5f,0),Quaternion.Euler(0,0,-90));
                                playerAnim.SetTrigger("attack");
                                    break;
                        }
                        rb.velocity = Vector2.zero;
                        break;
                    case ActivePower.dash:
                        dashTime = startDashTime;
                        dash = true;
                        break;
                    case ActivePower.colorControll:
                        crystal.ColorSearch();
                        break;
                }
                
                timeBtwPowerUse = startTimeBtwPowerUse;
            }
        }else
        {
            timeBtwPowerUse-=Time.deltaTime;
        }
        
        if(Input.GetAxisRaw("Horizontal") > 0)
            facingRight = true;
        else if(Input.GetAxisRaw("Horizontal") < 0)
            facingRight = false;

        if(dash){
            if(dashDirection == Direction.side)
                rb.velocity = ((facingRight)?Vector2.right*dashSpeed:Vector2.left*dashSpeed);
            else if(dashDirection == Direction.top)
                rb.velocity = (Vector2.up * dashSpeed);
            else if(dashDirection == Direction.down)
                rb.velocity = (Vector2.down * dashSpeed);

			dashTime -= Time.deltaTime;
			if(dashTime <= 0){
				dashTime = 0;
                //rb.velocity = Vector2.zero;
				dash = false;
				Instantiate(dashEffect,new Vector3(transform.position.x + ((facingRight?-0.5f:0.5f)),transform.position.y + 1,transform.position.z),Quaternion.identity);
			}
		}
        if(Input.GetKeyDown(KeyCode.Z))
            NextPower();
    }

    void NextPower(){
        activePower ++;
        if(activePower == ActivePower.none)
            activePower = ActivePower.rangedAttack;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color=Color.green;
        //Gizmos.DrawWireSphere(attackPos.position,attackRange);
        // Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
        switch(attackDirection){
                    case Direction.side:
                        Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
                            break;
                    case Direction.top:
                        Gizmos.DrawWireCube(transform.position + new Vector3(0,3f,0),new Vector3(attackRange.x,attackRange.y,1));
                            break;
                    case Direction.down:
                        Gizmos.DrawWireCube(transform.position + new Vector3(0,-1.5f,0),new Vector3(attackRange.x,attackRange.y,1));
                            break;
                }
    }
}
