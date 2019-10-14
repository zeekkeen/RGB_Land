using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Panda;


public class Unit: MonoBehaviour{

    public EnemyStats_SO statsTemplate;
    public EnemyStats_SO enemyStats;
    public List <SpriteRenderer> lifeRender = new List<SpriteRenderer>{};
    public GameObject bloodEffect, deathEffect;
    public GameObject bloodSplash, corpse;
    Animator anim;
    RipplePostProcessor camRipple;
	public bool facingRigth = true;
	public GameObject dashEffect, groundDetection, noGroundDetection;
    public LayerMask groundLayer;
	Rigidbody2D rb;

    void Start(){
        enemyStats = Instantiate(statsTemplate);
        enemyStats.maxHealth = enemyStats.currentHealth;
        //anim=GetComponent<Animator>();
		anim = GetComponentInChildren<Animator>();
        //anim.SetBool("isRunning",true);
		rb = GetComponent<Rigidbody2D>();
        camRipple = Camera.main.GetComponent<RipplePostProcessor>();
    }

    // void Update()
    // {
    //     if( (Time.time - lastReloadTime) * reloadRate >= 1.0f )
    //     {
    //         ammo++;
    //         if (ammo > startAmmo) ammo = startAmmo;
    //         lastReloadTime = Time.time;
    //     }

    // }

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

   
    #endregion

    #region combat tasks

    
    #endregion
}