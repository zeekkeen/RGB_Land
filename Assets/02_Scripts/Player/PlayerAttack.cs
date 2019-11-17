using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour{

    Rigidbody2D rb;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    Animator camAnim, playerAnim;
    public Animator attackAnim;
    public GameObject proyectile;
    public Direction attackDirection, dashDirection;
    [HideInInspector]
    public bool dashing = false, facingRight = true;
    public GameObject dashEffect;
    public CrystalController crystal;

    public enum Direction{
        top,
        down,
        side
    }

    PlayerControls inputAction;
    Vector2 movementInput;

    void Awake() {
        inputAction = new PlayerControls();
        inputAction.GamePlay.move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.GamePlay.move.canceled += ctx => movementInput = Vector2.zero;
        inputAction.GamePlay.MeleeAttack.performed += ctx => MeleeAttack();
        inputAction.GamePlay.Power.performed += ctx => Power();
        inputAction.GamePlay.ChangePower.performed += ctx => NextPower();
        // inputAction.GamePlay.Jump.canceled += ctx => JumpKeyUp();
    }

    void OnEnable() {
        inputAction.Enable();
    }

    void OnDisable() {
        inputAction.Disable();
    }

    void Start (){
        rb = GetComponent<Rigidbody2D>();
        camAnim = Camera.main.GetComponent<Animator>();
        playerAnim = GetComponentInChildren<Animator>();
        attackDirection = Direction.side;
        dashDirection = Direction.side;
        GameManager.instance.playerStats.activePower = 0;
        crystal = GameObject.FindGameObjectWithTag("Crystal").GetComponent<CrystalController>();
    }

    void Update(){
        // float moveInput = movementInput.x;
        // Debug.Log(movementInput.x + " : "+ movementInput.y);
        // if(movementInput.y == 0){
        //     attackDirection = Direction.side;
        //     playerAnim.SetInteger("Direction",0);
        // }
        if(movementInput.y < -0.8f && !GameManager.instance.playerStats.isGrounded){
            attackDirection = Direction.down;
            playerAnim.SetInteger("Direction",2);
        }
        else if(movementInput.y > 0.8f){
            attackDirection = Direction.top;
            playerAnim.SetInteger("Direction",1);
        }else{
            attackDirection = Direction.side;
            playerAnim.SetInteger("Direction",0);
        }

        if(!dashing){
            // if(movementInput.y == 0)
            //     dashDirection = Direction.side;
            if(movementInput.y < -0.8f)
                dashDirection = Direction.down;
            else if(movementInput.y > 0.8f)
                dashDirection = Direction.top;
            else
                dashDirection = Direction.side;
        }
        if(GameManager.instance.playerStats.timeBtwMeleeAttack > 0){
            GameManager.instance.playerStats.timeBtwMeleeAttack -= Time.deltaTime;
        }
        if(GameManager.instance.playerStats.timeBtwPowerUse <= 0){
            crystal.PowerActivated(false);
        
        }else
            GameManager.instance.playerStats.timeBtwPowerUse-=Time.deltaTime;
        
        if(movementInput.x > 0)
            facingRight = true;
        else if(movementInput.x < 0)
            facingRight = false;

        if(dashing){
            if(dashDirection == Direction.side)
                rb.velocity = ((facingRight) ? Vector2.right * GameManager.instance.playerStats.dashSpeed:Vector2.left * GameManager.instance.playerStats.dashSpeed);
            else if(dashDirection == Direction.top)
                rb.velocity = (Vector2.up * (GameManager.instance.playerStats.dashSpeed / 2));
            else if(dashDirection == Direction.down)
                rb.velocity = (Vector2.down * (GameManager.instance.playerStats.dashSpeed / 2));

			GameManager.instance.playerStats.dashTime -= Time.deltaTime;
			if(GameManager.instance.playerStats.dashTime <= 0){
				GameManager.instance.playerStats.dashTime = 0;
                //rb.velocity = Vector2.zero;
				dashing = false;
				Instantiate(dashEffect,new Vector3(transform.position.x + ((facingRight?-0.5f:0.5f)), transform.position.y + 1, transform.position.z), Quaternion.identity);
			}
		}
    }

    void NextPower(){
        GameManager.instance.playerStats.activePower ++;
        if(GameManager.instance.playerStats.activePower == ActivePower.none)
            GameManager.instance.playerStats.activePower = ActivePower.rangedAttack;
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        //Gizmos.DrawWireSphere(attackPos.position,attackRange);
        // Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
        switch(attackDirection){
                    case Direction.side:
                        Gizmos.DrawWireCube(attackPos.position,new Vector3(GameManager.instance.playerStats.attackRange.x,GameManager.instance.playerStats.attackRange.y,1));
                            break;
                    case Direction.top:
                        Gizmos.DrawWireCube(transform.position + new Vector3(0,3f,0),new Vector3(GameManager.instance.playerStats.attackRange.x,GameManager.instance.playerStats.attackRange.y,1));
                            break;
                    case Direction.down:
                        Gizmos.DrawWireCube(transform.position + new Vector3(0,-1.5f,0),new Vector3(GameManager.instance.playerStats.attackRange.x,GameManager.instance.playerStats.attackRange.y,1));
                            break;
                }
    }

    public void MeleeAttack(){
        if(GameManager.instance.playerStats.timeBtwMeleeAttack <= 0){
            Collider2D[] enemiesToDamage;
            switch(attackDirection){
                case Direction.side:
                    enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,GameManager.instance.playerStats.attackRange,0,whatIsEnemies);
                    //playerAnim.SetTrigger("attack");
                    attackAnim.SetTrigger("Active");
                    for(int i=0;i<enemiesToDamage.Length;i++){
                        // enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(GameManager.instance.playerStats.damage);
                        ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                        if(takeDamage != null)
                            takeDamage.TakeDamage(GameManager.instance.playerStats.damage);
                        //camAnim.SetTrigger("shake");
                    }
                        break;
                case Direction.top:
                    enemiesToDamage=Physics2D.OverlapBoxAll(transform.position + new Vector3(0,3f,0),GameManager.instance.playerStats.attackRange,0,whatIsEnemies);
                    playerAnim.SetTrigger("attack");
                    attackAnim.SetTrigger("Active");
                    for(int i=0;i<enemiesToDamage.Length;i++){
                        // enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(GameManager.instance.playerStats.damage);
                        ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                        if(takeDamage != null)
                            takeDamage.TakeDamage(GameManager.instance.playerStats.damage);
                        //camAnim.SetTrigger("shake");
                    }
                        break;
                case Direction.down:
                    enemiesToDamage = Physics2D.OverlapBoxAll(transform.position + new Vector3(0,-1.5f,0),GameManager.instance.playerStats.attackRange,0,whatIsEnemies);
                    playerAnim.SetTrigger("attack");
                    attackAnim.SetTrigger("Active");
                    for(int i=0;i<enemiesToDamage.Length;i++){
                        // enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(GameManager.instance.playerStats.damage);
                        ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                        if(takeDamage != null)
                            takeDamage.TakeDamage(GameManager.instance.playerStats.damage);
                        //camAnim.SetTrigger("shake");
                    }
                        break;
            }
            GameManager.instance.playerStats.timeBtwMeleeAttack = GameManager.instance.playerStats.startTimeBtwMeleeAttack;
            rb.velocity = Vector2.zero;
        }
    }

    public void Power(){
        if(GameManager.instance.playerStats.timeBtwPowerUse <= 0){
            switch (GameManager.instance.playerStats.activePower){
                        case ActivePower.rangedAttack:
                            GameObject instance;
                            switch(attackDirection){
                                case Direction.side:
                                    crystal.PowerActivated(true);
                                    instance = (GameObject) Instantiate(proyectile,attackPos.position,transform.rotation);
                                    //playerAnim.SetTrigger("attack");
                                    attackAnim.SetTrigger("Active");
                                        break;
                                case Direction.top:
                                    crystal.PowerActivated(true);
                                    instance = (GameObject) Instantiate(proyectile,transform.position + new Vector3(0,3f,0), Quaternion.Euler(0,0,90));
                                    playerAnim.SetTrigger("attack");
                                    attackAnim.SetTrigger("Active");
                                        break;
                                case Direction.down:
                                    crystal.PowerActivated(true);
                                    instance = (GameObject) Instantiate(proyectile,transform.position + new Vector3(0,-1.5f,0), Quaternion.Euler(0,0,-90));
                                    playerAnim.SetTrigger("attack");
                                    attackAnim.SetTrigger("Active");
                                        break;
                            }
                            rb.velocity = Vector2.zero;
                            break;
                        case ActivePower.dash:
                            GameManager.instance.playerStats.dashTime = GameManager.instance.playerStats.startDashTime;
                            Instantiate(dashEffect,new Vector3(transform.position.x + ((facingRight?-0.5f:0.5f)),transform.position.y + 1,transform.position.z),Quaternion.identity);
                            dashing = true;
                            break;
                        case ActivePower.colorControll:
                            crystal.PowerActivated(true);
                            crystal.ColorSearch();
                            break;
                    }
                    
                    GameManager.instance.playerStats.timeBtwPowerUse = GameManager.instance.playerStats.startTimeBtwPowerUse;
        }
    }
}
