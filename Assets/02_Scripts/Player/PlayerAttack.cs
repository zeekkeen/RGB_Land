using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour{

    Rigidbody2D rb;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    Animator camAnim,playerAnim;
    public GameObject proyectile;
    public Direction attackDirection, dashDirection;
    bool dashing = false, facingRight = true;
    public GameObject dashEffect;
    public CrystalController crystal;

    public enum Direction{
        top,
        down,
        side
    }

    void Start (){
        rb=GetComponent<Rigidbody2D>();
        camAnim=Camera.main.GetComponent<Animator>();
        playerAnim=GetComponentInChildren<Animator>();
        attackDirection = Direction.side;
        dashDirection = Direction.side;
        GameManager.instance.playerData.playerStats.activePower = 0;
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

        if(!dashing){
            if(moveInput == 0)
                dashDirection = Direction.side;
            if(moveInput < 0)
                dashDirection = Direction.down;
            else if(moveInput > 0)
                dashDirection = Direction.top;
        }

        if(GameManager.instance.playerData.playerStats.timeBtwMeleeAttack <= 0){
            if(Input.GetKeyDown(KeyCode.X)){
                Collider2D[] enemiesToDamage;
                switch(attackDirection){
                    case Direction.side:
                        enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,GameManager.instance.playerData.playerStats.attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            // enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(GameManager.instance.playerData.playerStats.damage);
                            ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                            if(takeDamage != null)
                                takeDamage.TakeDamage(GameManager.instance.playerData.playerStats.damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                    case Direction.top:
                        enemiesToDamage=Physics2D.OverlapBoxAll(transform.position + new Vector3(0,3f,0),GameManager.instance.playerData.playerStats.attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            // enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(GameManager.instance.playerData.playerStats.damage);
                            ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                            if(takeDamage != null)
                                takeDamage.TakeDamage(GameManager.instance.playerData.playerStats.damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                    case Direction.down:
                        enemiesToDamage=Physics2D.OverlapBoxAll(transform.position + new Vector3(0,-1.5f,0),GameManager.instance.playerData.playerStats.attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            // enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(GameManager.instance.playerData.playerStats.damage);
                            ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                            if(takeDamage != null)
                                takeDamage.TakeDamage(GameManager.instance.playerData.playerStats.damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                }
                GameManager.instance.playerData.playerStats.timeBtwMeleeAttack = GameManager.instance.playerData.playerStats.startTimeBtwMeleeAttack;
                rb.velocity = Vector2.zero;
            }
        }else
        {
            GameManager.instance.playerData.playerStats.timeBtwMeleeAttack -= Time.deltaTime;
        }
        if(GameManager.instance.playerData.playerStats.timeBtwPowerUse <= 0)
        {
            if(Input.GetKeyDown(KeyCode.C)){
                switch (GameManager.instance.playerData.playerStats.activePower){
                    case ActivePower.rangedAttack:
                        GameObject instance;
                        switch(attackDirection){
                            case Direction.side:
                                instance = (GameObject) Instantiate(proyectile,attackPos.position,transform.rotation);
                                playerAnim.SetTrigger("attack");
                                    break;
                            case Direction.top:
                                instance = (GameObject) Instantiate(proyectile,transform.position + new Vector3(0,3f,0), Quaternion.Euler(0,0,90));
                                playerAnim.SetTrigger("attack");
                                    break;
                            case Direction.down:
                                instance = (GameObject) Instantiate(proyectile,transform.position + new Vector3(0,-1.5f,0), Quaternion.Euler(0,0,-90));
                                playerAnim.SetTrigger("attack");
                                    break;
                        }
                        rb.velocity = Vector2.zero;
                        break;
                    case ActivePower.dash:
                        GameManager.instance.playerData.playerStats.dashTime = GameManager.instance.playerData.playerStats.startDashTime;
                        dashing = true;
                        break;
                    case ActivePower.colorControll:
                        crystal.ColorSearch();
                        break;
                }
                
                GameManager.instance.playerData.playerStats.timeBtwPowerUse = GameManager.instance.playerData.playerStats.startTimeBtwPowerUse;
            }
        }else
        {
            GameManager.instance.playerData.playerStats.timeBtwPowerUse-=Time.deltaTime;
        }
        
        if(Input.GetAxisRaw("Horizontal") > 0)
            facingRight = true;
        else if(Input.GetAxisRaw("Horizontal") < 0)
            facingRight = false;

        if(dashing){
            if(dashDirection == Direction.side)
                rb.velocity = ((facingRight)?Vector2.right * GameManager.instance.playerData.playerStats.dashSpeed:Vector2.left * GameManager.instance.playerData.playerStats.dashSpeed);
            else if(dashDirection == Direction.top)
                rb.velocity = (Vector2.up * GameManager.instance.playerData.playerStats.dashSpeed);
            else if(dashDirection == Direction.down)
                rb.velocity = (Vector2.down * GameManager.instance.playerData.playerStats.dashSpeed);

			GameManager.instance.playerData.playerStats.dashTime -= Time.deltaTime;
			if(GameManager.instance.playerData.playerStats.dashTime <= 0){
				GameManager.instance.playerData.playerStats.dashTime = 0;
                //rb.velocity = Vector2.zero;
				dashing = false;
				Instantiate(dashEffect,new Vector3(transform.position.x + ((facingRight?-0.5f:0.5f)),transform.position.y + 1,transform.position.z),Quaternion.identity);
			}
		}
        if(Input.GetKeyDown(KeyCode.Z))
            NextPower();
    }

    void NextPower(){
        GameManager.instance.playerData.playerStats.activePower ++;
        if(GameManager.instance.playerData.playerStats.activePower == ActivePower.none)
            GameManager.instance.playerData.playerStats.activePower = ActivePower.rangedAttack;
    }

    void OnDrawGizmosSelected(){
        Gizmos.color=Color.green;
        //Gizmos.DrawWireSphere(attackPos.position,attackRange);
        // Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
        switch(attackDirection){
                    case Direction.side:
                        Gizmos.DrawWireCube(attackPos.position,new Vector3(GameManager.instance.playerData.playerStats.attackRange.x,GameManager.instance.playerData.playerStats.attackRange.y,1));
                            break;
                    case Direction.top:
                        Gizmos.DrawWireCube(transform.position + new Vector3(0,3f,0),new Vector3(GameManager.instance.playerData.playerStats.attackRange.x,GameManager.instance.playerData.playerStats.attackRange.y,1));
                            break;
                    case Direction.down:
                        Gizmos.DrawWireCube(transform.position + new Vector3(0,-1.5f,0),new Vector3(GameManager.instance.playerData.playerStats.attackRange.x,GameManager.instance.playerData.playerStats.attackRange.y,1));
                            break;
                }
    }
}
