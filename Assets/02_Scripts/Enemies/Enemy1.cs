using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody2D))]
public class Enemy1 : MonoBehaviour ,ITakeDamage{

    public TypeOfColor typeOfColor;
    public enum TypeOfColor{
        red,
        green,
        blue
    }

    public List <SpriteRenderer> lifeRender = new List<SpriteRenderer>{};
    public int health = 0,maxHealth=0;
    public float speed;
    float dazedTime;
    public float StartDazedTime = 0.6f;
    public GameObject bloodEffect,deathEffect;
    public GameObject bloodSplash,corpse;
    Animator anim;
    RipplePostProcessor camRipple;
	bool facingRight = false;
	float dashTime=0f;
	public float startDashTime=0.1f,dashSpeed=7f,distance=5f;
	bool dash=false;
	public GameObject dashEffect,groundDetection,noGroundDetection;
    public LayerMask groundLayer;

	Rigidbody2D rb;
    void Start(){
        maxHealth=health;
        //anim=GetComponent<Animator>();
		anim=GetComponentInChildren<Animator>();
        //anim.SetBool("isRunning",true);
		rb=GetComponent<Rigidbody2D>();
        camRipple=Camera.main.GetComponent<RipplePostProcessor>();

    }

	void Update() {
        if(dazedTime<=0)
            speed=5;
        else{
            speed=0;
            dazedTime-=Time.deltaTime;
        }
        RaycastHit2D groundInfo = Physics2D.Raycast(noGroundDetection.transform.position,Vector2.down,distance,groundLayer);
        if (!groundInfo.collider)
            flip();
		rb.velocity = ((facingRight)?Vector2.right*speed:Vector2.left*speed);
	}

	void flip(){
		facingRight=!facingRight;
        if(!facingRight)
            transform.eulerAngles = new Vector3(0,0,0);
        else
            transform.eulerAngles = new Vector3(0,180,0);
	}

    public void TakeDamage(int damage){
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
        dazedTime=StartDazedTime;
        health-=damage;
        ChangeColor1();
        Debug.Log("damague taken!!");
		if(health <= 0)
			Dead();
    }
    public void Dead(){
        camRipple.RippleEffect();
        Instantiate(corpse,transform.position,Quaternion.identity);
        Instantiate(bloodSplash,transform.position,Quaternion.identity);
        Instantiate(deathEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }

    public void ChangeColor1(){
        float colored, percentOfHealth;
        percentOfHealth = (health*100) / maxHealth;
        colored = 1 - (percentOfHealth / 100);
        foreach (SpriteRenderer element in lifeRender){
            switch (typeOfColor){
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
        float alpha,percentOfHealth;
        percentOfHealth = (health*100) / maxHealth;
        alpha = percentOfHealth / 100;
        foreach (SpriteRenderer element in lifeRender){
            element.color = new Color(element.color.r, element.color.g, element.color.b, alpha);
        }
        // lifeRender.color = new Color(lifeRender.color.r, lifeRender.color.g, lifeRender.color.b, alpha);
    }
}
