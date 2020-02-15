using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Panda;


public class Unit: MonoBehaviour, ITakeDamage{

    public EnemyStats_SO statsTemplate;
    public EnemyStats_SO enemyStats;
    public TypeOfColor unitTypeOfColor;
    public List <SpriteRenderer> lifeRender = new List<SpriteRenderer>{};
    public GameObject bloodEffect, deathEffect;
    public GameObject bloodSplash, corpse, projectile;
    public float previousHealth;
    public Animator anim;
    RipplePostProcessor camRipple;
	public bool facingRigth = true;
	public GameObject dashEffect, groundDetection, noGroundDetection;
    public LayerMask groundLayer, playerLayer;
    [HideInInspector]
	public Rigidbody2D rb;
    public float attackRange = 2f;
    GameObject player;

    void Start(){
        enemyStats = Instantiate(statsTemplate);
        enemyStats.maxHealth = enemyStats.currentHealth;
        previousHealth = enemyStats.currentHealth;
        //anim=GetComponent<Animator>();
		// anim = GetComponentInChildren<Animator>();
        //anim.SetBool("isRunning",true);
		rb = GetComponent<Rigidbody2D>();
        camRipple = Camera.main.GetComponent<RipplePostProcessor>();
        enemyStats.attackRange = attackRange;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update(){
        if(enemyStats.dazedTime <= 0)
            enemyStats.moveSpeed = 5;
        else{
            enemyStats.moveSpeed = 0;
            enemyStats.dazedTime -= Time.deltaTime;
        }
    }
    public void TakeDamage(int damage){
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        enemyStats.dazedTime = enemyStats.StartDazedTime;
        enemyStats.currentHealth -= damage;
    }

    #region navigation tasks

    // [Task]
    // public bool IsHealthLessThan( float health )
    // {
    //     return this.health < health;
    // }

    // [Task]
    // public bool IsHealth_PercentLessThan(float percent)
    // {
    //     return (this.health / startHealth)*100.0 < percent;
    // }
    
    [Task]
    public bool Move(){
        anim.SetInteger("State", 1);
        rb.velocity = ((facingRigth) ? Vector2.right * enemyStats.moveSpeed : Vector2.left * enemyStats.moveSpeed);
        return true;
    }

    [Task]
    public bool FastMove(){
        anim.SetInteger("State", 2);
        rb.velocity = ((facingRigth) ? Vector2.right * enemyStats.moveSpeed : Vector2.left * enemyStats.moveSpeed) * 2;
        return true;
    }

    [Task]
    public bool Rotate(){
        anim.SetInteger("State", 2);
        // transform.up = Vector3.Lerp(transform.up, (GameObject.FindGameObjectWithTag("Player").transform.position - transform.position), 5f);
        noGroundDetection.transform.up = Vector3.Lerp(noGroundDetection.transform.up, (GameObject.FindGameObjectWithTag("Player").transform.position - noGroundDetection.transform.position), 5f);
        return true;
    }

    [Task]
    public bool Idle(){
        anim.SetInteger("State", 0);
        return true;
    }

    [Task]
    public bool Reload(){
        enemyStats.attackSpeedTimer -= Time.deltaTime;
        rb.velocity = Vector2.zero;
        if(enemyStats.attackSpeedTimer <= 0)
            return false;
        else 
            return true;
    }

    [Task]
    public bool Reload2(){
        anim.SetInteger("State", 1);
        enemyStats.attackSpeedTimer -= Time.deltaTime;
        // rb.velocity = Vector2.zero;
        if(enemyStats.attackSpeedTimer <= 0)
            return false;
        else 
            return true;
    }

    [Task]
    public bool GroundDetected(){
        RaycastHit2D groundInfo = Physics2D.Raycast(noGroundDetection.transform.position, Vector2.down, enemyStats.distance, groundLayer);
        RaycastHit2D groundInfo2 = Physics2D.Raycast(noGroundDetection.transform.position, (facingRigth ? Vector2.right : Vector2.left), 2f, groundLayer);
        if (groundInfo.collider)
            Debug.Log(groundInfo.collider.gameObject.name);
        //     return true;
        // else 
            return groundInfo.collider && !groundInfo2.collider;
    }

    [Task]
    public bool EnemyInShootRange(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null){
        // float dist = Vector2.Distance(transform.position,player.transform.position);
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        RaycastHit2D colliderInfo = Physics2D.Raycast(transform.position, dir, enemyStats.visionDistance, playerLayer);
        if (colliderInfo.collider){
            if (colliderInfo.collider.gameObject.tag == "Player"){
            anim.SetInteger("State", 1);
            return true;
        }
        }}
        return false;
    }

    [Task]
    public bool EnemyDetected(){
        RaycastHit2D colliderInfo = Physics2D.Raycast(transform.position, (facingRigth ? Vector2.right : Vector2.left), enemyStats.visionDistance, playerLayer);
        if (colliderInfo.collider)
            return true;
        else 
            return false;
    }

    [Task]
    public bool EnemyClose(){
        RaycastHit2D colliderInfo = Physics2D.Raycast(transform.position, (facingRigth ? Vector2.right : Vector2.left), enemyStats.attackRange, playerLayer);
        if (colliderInfo.collider)
            return true;
        else 
            return false;
    }

    [Task]
    public bool EnemyVisibleInRange(){
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if(player != null){
        // float dist = Vector2.Distance(transform.position,player.transform.position);
        Vector2 dir = player.transform.position - transform.position;
        dir.Normalize();
        RaycastHit2D colliderInfo = Physics2D.Raycast(transform.position, dir, enemyStats.visionDistance, playerLayer);
        if (colliderInfo.collider){
            if (colliderInfo.collider.gameObject.tag == "Player"){
            anim.SetInteger("State", 3);
            return true;
        }
        }}
        return false;
    }

     [Task]
    public bool ChargeAttack(){
        anim.SetInteger("State", 2);
        StartCoroutine(WaitForChargeAttack(1));
        // rb.AddForce((facingRigth ? Vector2.right : Vector2.left) * 2000 + Vector2.up * 1000);
        // anim.SetInteger("State", 2);
        enemyStats.attackSpeedTimer = enemyStats.attackSpeed;
        return true;
    }

    IEnumerator WaitForChargeAttack(float s){
        rb.velocity = Vector2.zero;
        yield return new WaitForSeconds(s);
        rb.AddForce((facingRigth ? Vector2.right : Vector2.left) * 2000 + Vector2.up * 1000);
    }

    [Task]
    public bool MeleeAttack(){
        Collider2D[] enemiesToDamage;
        enemiesToDamage = Physics2D.OverlapBoxAll(noGroundDetection.transform.position,new Vector2(enemyStats.attackRange, enemyStats.attackRange),0,playerLayer);
        if (enemiesToDamage != null){
            for(int i=0;i<enemiesToDamage.Length;i++){
                            ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
                            if(takeDamage != null)
                                takeDamage.TakeDamage(enemyStats.damage);
                        }
            enemyStats.attackSpeedTimer = enemyStats.attackSpeed;
            return true;
        }
        else 
            return false;
    }

    [Task]
    public bool RangeAttack(){
        anim.SetInteger("State", 3);
        Instantiate(projectile,noGroundDetection.transform.position,noGroundDetection.transform.rotation);
        // anim.SetInteger("State", 2);
        enemyStats.attackSpeedTimer = enemyStats.attackSpeed;
        return true;
    }

    [Task]
    public bool MultipleRangeAttack(int modifier){
        anim.SetInteger("State", 3);
        GameObject aux = Instantiate(projectile,noGroundDetection.transform.position,noGroundDetection.transform.rotation);
        aux.transform.Rotate(0,0,Random.Range(5,modifier));
        Instantiate(projectile,noGroundDetection.transform.position,noGroundDetection.transform.rotation);
        GameObject aux2 = Instantiate(projectile,noGroundDetection.transform.position,noGroundDetection.transform.rotation);
        aux2.transform.Rotate(0,0,-Random.Range(5,modifier));
        // anim.SetInteger("State", 2);
        enemyStats.attackSpeedTimer = enemyStats.attackSpeed;
        return true;
    }

    [Task]
    public bool RedPillar(){
        anim.SetInteger("State", 3);
        GameObject aux = Instantiate(projectile,noGroundDetection.transform.position,Quaternion.identity);
        aux.GetComponentInChildren<RedPillar>().damage = enemyStats.damage;
        // anim.SetInteger("State", 2);
        enemyStats.attackSpeedTimer = enemyStats.attackSpeed;
        return true;
    }

    [Task]
    public bool RedPillarPosition(){
        anim.SetInteger("State", 2);
        noGroundDetection.transform.parent = null;
        noGroundDetection.transform.position = new Vector3(GameObject.FindGameObjectWithTag("Player").transform.position.x,noGroundDetection.transform.position.y,0);
        return true;
    }

    [Task]
    public bool Flip(){
        facingRigth = !facingRigth;
        if(facingRigth)
            transform.rotation = Quaternion.Euler(0,0,0);
        else 
            transform.rotation = Quaternion.Euler(0,180,0);
        return true;
    }

    [Task]
    public bool IsHealthLessThanPrevious(){
        if(enemyStats.currentHealth < previousHealth){
            enemyStats.dazedTime = enemyStats.StartDazedTime;
            previousHealth = enemyStats.currentHealth;
            return true;
        }
        else 
            return false;
    }

    [Task]
    public bool FlipDamage(){
        if((player.transform.position.x < transform.position.x && facingRigth) || (player.transform.position.x > transform.position.x && !facingRigth))
            Flip();
        return true;
    }

    [Task]
    public bool ChangeColor1(){
        float colored, percentOfHealth;
        rb.velocity = Vector2.zero;
        percentOfHealth = (enemyStats.currentHealth * 100) / enemyStats.maxHealth;
        colored = 1 - (percentOfHealth / 100);
        foreach (SpriteRenderer element in lifeRender){
            switch (unitTypeOfColor){
                    case TypeOfColor.red:
                        element.color = new Color(element.color.r, colored, colored);
                        break;
                    case TypeOfColor.green:
                        element.color = new Color(colored, element.color.g, colored);
                        break;
                    case TypeOfColor.blue:
                        element.color = new Color(colored, colored, element.color.b);
                        break;
                }
        }
        return true;
    }

    [Task]
    public bool IsNoHealth(){
        if(enemyStats.currentHealth <= 0)
            return true;
        else 
            return false;
    }

    [Task]
    public bool Dead(){
        camRipple.RippleEffect();
        Instantiate(corpse,transform.position, Quaternion.identity);
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);

        return true;
    }

   
    #endregion

    #region combat tasks

    
    #endregion

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            ITakeDamage takeDamage = other.gameObject.GetComponent<ITakeDamage>();
                            if(takeDamage != null)
                                takeDamage.TakeDamage(enemyStats.damage);
        }
    }
}