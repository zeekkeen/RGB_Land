using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBoss : MonoBehaviour,ITakeDamage{
    
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
    public float force;
    
    void Start(){
        enemyStats = Instantiate(statsTemplate);
        enemyStats.maxHealth = enemyStats.currentHealth;
        //anim=GetComponent<Animator>();
		anim = GetComponentInChildren<Animator>();
        //anim.SetBool("isRunning",true);
		rb = GetComponent<Rigidbody2D>();
        camRipple = Camera.main.GetComponent<RipplePostProcessor>();
    }

    
    public void TakeDamage(int damage){
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        // enemyStats.dazedTime = enemyStats.StartDazedTime;
        enemyStats.currentHealth -= damage;
        ChangeColor1();
        // Debug.Log("damague taken!!");
		if(enemyStats.currentHealth <= 0)
			Dead();
    }
    
    public void Dead(){
        camRipple.RippleEffect();
        Instantiate(corpse,transform.position, Quaternion.identity);
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        anim.SetBool("death",true);
        GameManager.instance.AddAchievement(Achievement.RangeAttack1);
        Destroy(gameObject,1.7f);
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
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player"){
            ITakeDamage takeDamage = other.gameObject.GetComponent<ITakeDamage>();
            if(takeDamage != null){
                takeDamage.TakeDamage(1);
                Vector3 vec = other.transform.position - transform.position;
                other.gameObject.GetComponent<Rigidbody2D>().AddForce(vec.normalized * force * (-1));
            }
        }   
    }
}