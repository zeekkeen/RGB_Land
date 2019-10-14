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
    public GameObject bloodSplash, corpse;
    public float previousHealth;
    Animator anim;
    RipplePostProcessor camRipple;
	public bool facingRigth = true;
	public GameObject dashEffect, groundDetection, noGroundDetection;
    public LayerMask groundLayer;
	Rigidbody2D rb;

    void Start(){
        enemyStats = Instantiate(statsTemplate);
        enemyStats.maxHealth = enemyStats.currentHealth;
        previousHealth = enemyStats.currentHealth;
        //anim=GetComponent<Animator>();
		anim = GetComponentInChildren<Animator>();
        //anim.SetBool("isRunning",true);
		rb = GetComponent<Rigidbody2D>();
        camRipple = Camera.main.GetComponent<RipplePostProcessor>();
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
        // ChangeColor1();
        // Debug.Log("damague taken!!");
		// if(enemyStats.currentHealth <= 0)
		// 	Dead();
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
    public bool Move()
    {
        rb.velocity = ((facingRigth) ? Vector2.right * enemyStats.moveSpeed : Vector2.left * enemyStats.moveSpeed);
        return true;
    }

    [Task]
    public bool NoGroundDetected()
    {
        RaycastHit2D groundInfo = Physics2D.Raycast(noGroundDetection.transform.position, Vector2.down, enemyStats.distance, groundLayer);
        if (!groundInfo.collider)
            return true;
        else 
            return false;
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
}