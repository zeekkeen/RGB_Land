using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour{

    Rigidbody2D rb;
    public Transform attackPos;
    public Transform attackPos2;
    public LayerMask whatIsEnemies;
    Animator camAnim, playerAnim;
    public Animator attackAnim;
    public List<GameObject> proyectiles;
    public Direction attackDirection, dashDirection;
    [HideInInspector]
    public bool dashing = false, facingRight = true;
    public GameObject dashEffect;
    public GameObject crystalPrefab;
    public CrystalController crystal;
    int attackCombo = 0, nextAttackCombo = -1;
    float timeBtwMeleeAttack2;
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
        inputAction.GamePlay.RangedAttack.performed += ctx => RangedAttack();
        inputAction.GamePlay.ColorControl.performed += ctx => ColorControl();
        inputAction.GamePlay.NextRangedPower.performed += ctx => NextPower();
        inputAction.GamePlay.PreviousRangedPower.performed += ctx => PreviousPower();
        inputAction.GamePlay.Dash.started += ctx => Dash();
    }
    public void SpawnCrystal(){
        if(GameManager.instance.playerStats.stealAndGiveColor){
            GameObject aux = Instantiate (crystalPrefab, attackPos.transform.position, Quaternion.identity);
            crystal = aux.GetComponent<CrystalController>();
            crystal.InitializeCrystal(attackPos.gameObject,attackPos2.gameObject);
        }
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
        GameManager.instance.playerStats.activeRangePower = 0;
        // crystal = GameObject.FindGameObjectWithTag("Crystal").GetComponent<CrystalController>();
        SpawnCrystal();
    }

    void Update(){
        if(!GameManager.instance.playerData.inPause){
            if(movementInput.y < -0.7f && !GameManager.instance.playerStats.isGrounded){
                attackDirection = Direction.down;
                playerAnim.SetInteger("Direction",2);
            }
            else if(movementInput.y > 0.7f){
                attackDirection = Direction.top;
                playerAnim.SetInteger("Direction",1);
            }else{
                attackDirection = Direction.side;
                playerAnim.SetInteger("Direction",0);
            }

            if(!dashing){
                if(movementInput.y < -0.7f)
                    dashDirection = Direction.down;
                else if(movementInput.y > 0.7f)
                    dashDirection = Direction.top;
                else
                    dashDirection = Direction.side;
            }
            if(timeBtwMeleeAttack2 > 0){
                timeBtwMeleeAttack2 -= Time.deltaTime;
                if(timeBtwMeleeAttack2 <= 0){
                    Debug.Log("attackCombo:"+attackCombo+" nextAttackCombo:"+nextAttackCombo);
                    if(nextAttackCombo <= attackCombo){
                        attackCombo = 0;
                        nextAttackCombo = -1;
                    }
                        attackAnim.SetBool("active",false);
                }
            }
            if(GameManager.instance.playerStats.timeBtwMeleeAttack > 0){
                GameManager.instance.playerStats.timeBtwMeleeAttack -= Time.deltaTime;
                if(GameManager.instance.playerStats.timeBtwMeleeAttack <= 0){
                    // Debug.Log("attackCombo:"+attackCombo+" nextAttackCombo:"+nextAttackCombo);
                    if(nextAttackCombo <= attackCombo){
                        attackCombo = 0;
                        nextAttackCombo = -1;
                        attackAnim.SetBool("active",false);
                    }
                    else if(nextAttackCombo > attackCombo){
                        MeleeAttack2();
                    }

                }
            }
            if(GameManager.instance.playerStats.timeBtwPowerUse <= 0){
                if(crystal != null)
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
                else if(dashDirection == Direction.top){
                    if(GameManager.instance.playerStats.verticalDashTime <= 0){
                        GameManager.instance.playerStats.verticalDashTime = GameManager.instance.playerStats.verticalStartDashTime; 
                        rb.velocity = (Vector2.up * (GameManager.instance.playerStats.dashSpeed / 2.5f));
                    }
                }
                else if(dashDirection == Direction.down)
                    rb.velocity = (Vector2.down * (GameManager.instance.playerStats.dashSpeed / 2));

                GameManager.instance.playerStats.dashTime -= Time.deltaTime;
                if(GameManager.instance.playerStats.dashTime <= 0){
                    playerAnim.SetBool("Dash",false);
                    GameManager.instance.playerStats.dashTime = 0;
                    //rb.velocity = Vector2.zero;
                    dashing = false;
                    if((dashDirection == Direction.top && GameManager.instance.playerStats.verticalDashTime <= 0) || dashDirection == Direction.side  || dashDirection == Direction.down)
                        Instantiate(dashEffect,new Vector3(transform.position.x + ((facingRight?-0.5f:0.5f)), transform.position.y + 1, transform.position.z), Quaternion.identity);
                }
            }
            if(GameManager.instance.playerStats.verticalDashTime > 0){
                GameManager.instance.playerStats.verticalDashTime -= Time.deltaTime;
            }
        }
    }

    void NextPower(){
        if(!GameManager.instance.playerData.inPause && GameManager.instance.playerStats.rangedAttack1){
            GameManager.instance.playerStats.activeRangePower ++;
            switch (GameManager.instance.playerStats.activeRangePower ){
                case 0:
                    if(!GameManager.instance.playerStats.rangedAttack1)
                        GameManager.instance.playerStats.activeRangePower = -1;
                break;
                case 1:
                    if(!GameManager.instance.playerStats.rangedAttack2)
                        GameManager.instance.playerStats.activeRangePower = -1;
                break;
                case 2:
                    if(!GameManager.instance.playerStats.rangedAttack3)
                        GameManager.instance.playerStats.activeRangePower = -1;
                break;
            }
            InGameUIManager.instance.UpdateCircleColor();
        }
    }

    void PreviousPower(){
        if(!GameManager.instance.playerData.inPause && GameManager.instance.playerStats.rangedAttack1){
            GameManager.instance.playerStats.activeRangePower --;
            switch (GameManager.instance.playerStats.activeRangePower ){
                case -2:
                    if(GameManager.instance.playerStats.rangedAttack3)
                        GameManager.instance.playerStats.activeRangePower = 2;
                    else if(GameManager.instance.playerStats.rangedAttack2)
                        GameManager.instance.playerStats.activeRangePower = 1;
                    else if(GameManager.instance.playerStats.rangedAttack1)
                        GameManager.instance.playerStats.activeRangePower = 0;
                    else 
                        GameManager.instance.playerStats.activeRangePower = -1;
                break;
                case 0:
                    if(!GameManager.instance.playerStats.rangedAttack1)
                        GameManager.instance.playerStats.activeRangePower = -1;
                break;
                case 1:
                    if(!GameManager.instance.playerStats.rangedAttack2)
                        GameManager.instance.playerStats.activeRangePower = -1;
                break;
                case 2:
                    if(!GameManager.instance.playerStats.rangedAttack3)
                        GameManager.instance.playerStats.activeRangePower = -1;
                break;
            }
            InGameUIManager.instance.UpdateCircleColor();
        }
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
        if(!GameManager.instance.playerData.inPause){
            if(attackCombo >= nextAttackCombo && attackCombo <= 3)
                nextAttackCombo ++;
            if(GameManager.instance.playerStats.timeBtwMeleeAttack <= 0){
                attackAnim.SetBool("active",true);
                // SoundManager.instance.PlaySound("VoiceAttac1");
                SoundManager.instance.PlaySound("AttacPlayer1(Sword)");
                Collider2D[] enemiesToDamage;
                switch(attackDirection){
                    case Direction.side:
                        enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,GameManager.instance.playerStats.attackRange,0,whatIsEnemies);
                        //playerAnim.SetTrigger("attack");
                        // attackAnim.SetTrigger("Active");
                        attackAnim.SetInteger("combo",attackCombo);
                        // attackCombo ++;
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            // enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(GameManager.instance.playerStats.damage);
                            ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                            if(takeDamage != null)
                                takeDamage.TakeDamage(GameManager.instance.playerStats.damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                    case Direction.top:
                        enemiesToDamage = Physics2D.OverlapBoxAll(transform.position + new Vector3(0,3f,0),GameManager.instance.playerStats.attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        // attackAnim.SetTrigger("Active");
                        attackAnim.SetInteger("combo",attackCombo);
                        // attackCombo ++;
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
                        // attackAnim.SetTrigger("Active");
                        attackAnim.SetInteger("combo",attackCombo);
                        // attackCombo ++;
                        for(int i=0; i < enemiesToDamage.Length; i++){
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
    }

    public void MeleeAttack2(){
        attackCombo ++;
            attackAnim.SetBool("active",true);
            SoundManager.instance.PlaySound("VoiceAttac1");
            SoundManager.instance.PlaySound("AttacPlayer2(Sword)");
            Collider2D[] enemiesToDamage;
            switch(attackDirection){
                case Direction.side:
                    enemiesToDamage = Physics2D.OverlapBoxAll(attackPos.position,GameManager.instance.playerStats.attackRange,0,whatIsEnemies);
                    attackAnim.SetInteger("combo",attackCombo);
                    for(int i=0;i<enemiesToDamage.Length;i++){
                        ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                        if(takeDamage != null)
                            takeDamage.TakeDamage(GameManager.instance.playerStats.damage);
                    }
                        break;
                case Direction.top:
                    enemiesToDamage = Physics2D.OverlapBoxAll(transform.position + new Vector3(0,3f,0),GameManager.instance.playerStats.attackRange,0,whatIsEnemies);
                    playerAnim.SetTrigger("attack");
                    attackAnim.SetInteger("combo",attackCombo);
                    for(int i=0;i<enemiesToDamage.Length;i++){
                        ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                        if(takeDamage != null)
                            takeDamage.TakeDamage(GameManager.instance.playerStats.damage);
                    }
                        break;
                case Direction.down:
                    enemiesToDamage = Physics2D.OverlapBoxAll(transform.position + new Vector3(0,-1.5f,0),GameManager.instance.playerStats.attackRange,0,whatIsEnemies);
                    playerAnim.SetTrigger("attack");
                    attackAnim.SetInteger("combo",attackCombo);
                    // attackCombo ++;
                    for(int i=0; i < enemiesToDamage.Length; i++){
                        ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                        if(takeDamage != null)
                            takeDamage.TakeDamage(GameManager.instance.playerStats.damage);
                    }
                        break;
            }
            timeBtwMeleeAttack2 = GameManager.instance.playerStats.startTimeBtwMeleeAttack;
            // timeBtwMeleeAttack2 = 0.1f;
            rb.velocity = Vector2.zero;
        
    }

    public void RangedAttack(){
        if(!GameManager.instance.playerData.inPause && GameManager.instance.playerStats.rangedAttack1 && crystal != null){
            if(GameManager.instance.playerStats.activeRangePower == -1)
                GameManager.instance.playerStats.activeRangePower = 0;
            if(InGameUIManager.instance.energyProgressBar.current >= GameManager.instance.playerStats.rangedAttackCost){
                InGameUIManager.instance.energyProgressBar.current -= GameManager.instance.playerStats.rangedAttackCost;
                InGameUIManager.instance.UpdateCurrentFill();
                SoundManager.instance.PlaySound("AttacPlayerF");
                GameObject instance;
                switch(attackDirection){
                    case Direction.side:
                        crystal.PowerActivated(true);
                        instance = (GameObject) Instantiate(proyectiles[GameManager.instance.playerStats.activeRangePower],attackPos.position,transform.rotation);
                        //playerAnim.SetTrigger("attack");
                        attackAnim.SetTrigger("Active");
                            break;
                    case Direction.top:
                        crystal.PowerActivated(true);
                        instance = (GameObject) Instantiate(proyectiles[GameManager.instance.playerStats.activeRangePower],transform.position + new Vector3(0,3f,0), Quaternion.Euler(0,0,90));
                        playerAnim.SetTrigger("attack");
                        attackAnim.SetTrigger("Active");
                            break;
                    case Direction.down:
                        crystal.PowerActivated(true);
                        instance = (GameObject) Instantiate(proyectiles[GameManager.instance.playerStats.activeRangePower],transform.position + new Vector3(0,-1.5f,0), Quaternion.Euler(0,0,-90));
                        playerAnim.SetTrigger("attack");
                        attackAnim.SetTrigger("Active");
                            break;
                }
                rb.velocity = Vector2.zero;
                GameManager.instance.playerStats.timeBtwPowerUse = GameManager.instance.playerStats.startTimeBtwPowerUse;
            }
        }
    }

    public void Dash(){
        if(!GameManager.instance.playerData.inPause){
            if(InGameUIManager.instance.energyProgressBar.current >= GameManager.instance.playerStats.dashCost){
                InGameUIManager.instance.energyProgressBar.current -= GameManager.instance.playerStats.dashCost;
                InGameUIManager.instance.UpdateCurrentFill();
                if((dashDirection == Direction.top && GameManager.instance.playerStats.verticalDashTime <= 0) || dashDirection == Direction.side || dashDirection == Direction.down){
                    GameManager.instance.playerStats.dashTime = GameManager.instance.playerStats.startDashTime;
                    Instantiate(dashEffect,new Vector3(transform.position.x + ((facingRight?-0.5f:0.5f)),transform.position.y + 1,transform.position.z),Quaternion.identity);
                }
                SoundManager.instance.PlaySound("AttacPlayer1(Sword)");
                dashing = true;
                playerAnim.SetBool("Dash",true);
                GameManager.instance.playerStats.timeBtwPowerUse = GameManager.instance.playerStats.startTimeBtwPowerUse;
            }
        }
    }

    public void ColorControl(){
        if(!GameManager.instance.playerData.inPause && GameManager.instance.playerStats.stealAndGiveColor && crystal != null){
            if(GameManager.instance.playerStats.timeBtwPowerUse <= 0){
                crystal.PowerActivated(true);
                crystal.ColorSearch();
                GameManager.instance.playerStats.timeBtwPowerUse = GameManager.instance.playerStats.startTimeBtwPowerUse;
            }
        }
    }
}