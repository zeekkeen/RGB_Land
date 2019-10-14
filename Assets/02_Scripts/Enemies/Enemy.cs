using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CapsuleCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class Enemy : MonoBehaviour ,ITakeDamage{

    public EnemyStats_SO statsTemplate;
    public EnemyStats_SO enemyStats;
    public List <SpriteRenderer> lifeRender = new List<SpriteRenderer>{};
    public GameObject bloodEffect, deathEffect;
    public GameObject bloodSplash, corpse;
    Animator anim;
    RipplePostProcessor camRipple;
	bool facingRight = false;
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

	void Update() {
        if(enemyStats.dazedTime <= 0)
            enemyStats.moveSpeed = 5;
        else{
            enemyStats.moveSpeed = 0;
            enemyStats.dazedTime -= Time.deltaTime;
        }
        RaycastHit2D groundInfo = Physics2D.Raycast(noGroundDetection.transform.position, Vector2.down, enemyStats.distance, groundLayer);
        if (!groundInfo.collider)
            flip();
		rb.velocity = ((facingRight) ? Vector2.right * enemyStats.moveSpeed:Vector2.left * enemyStats.moveSpeed);
	}

	void flip(){
		facingRight =! facingRight;
        if(!facingRight)
            transform.eulerAngles = new Vector3(0,0,0);
        else
            transform.eulerAngles = new Vector3(0,180,0);
	}

    public void TakeDamage(int damage){
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        enemyStats.dazedTime = enemyStats.StartDazedTime;
        enemyStats.currentHealth -= damage;
        ChangeColor1();
        Debug.Log("damague taken!!");
		if(enemyStats.currentHealth <= 0)
			Dead();
    }
    public void Dead(){
        camRipple.RippleEffect();
        Instantiate(corpse,transform.position, Quaternion.identity);
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    public void ChangeColor1(){
        float colored, percentOfHealth;
        percentOfHealth = (enemyStats.currentHealth*100) / enemyStats.maxHealth;
        colored = 1 - (percentOfHealth / 100);
        foreach (SpriteRenderer element in lifeRender){
            switch (enemyStats.typeOfColor){
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
    }
    public void ChangeColor2(){
        float alpha, percentOfHealth;
        percentOfHealth = (enemyStats.currentHealth*100) / enemyStats.maxHealth;
        alpha = percentOfHealth / 100;
        foreach (SpriteRenderer element in lifeRender){
            element.color = new Color(element.color.r, element.color.g, element.color.b, alpha);
        }
        // lifeRender.color = new Color(lifeRender.color.r, lifeRender.color.g, lifeRender.color.b, alpha);
    }
}
