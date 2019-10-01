using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    float dazedTime;
    public float StartDazedTime = 0.6f;
    public GameObject bloodEffect,deathEffect;
    public GameObject bloodSplash,corpse;
    Animator anim;
    //RipplePostProcessor camRipple;
	bool facingRight=true;
	float dashTime=0f;
	public float startDashTime=0.1f,dashSpeed=7f,distance;
	bool dash=false;
	public GameObject dashEffect,groundDetection;

	Rigidbody2D rb;
    void Start()
    {
        //anim=GetComponent<Animator>();
		anim=GetComponentInChildren<Animator>();
        anim.SetBool("isRunning",true);
		rb=GetComponent<Rigidbody2D>();
        //camRipple=Camera.main.GetComponent<RipplePostProcessor>();

    }

	void Update() {
		rb.velocity = ((facingRight)?Vector2.right*speed:Vector2.left*speed);
	}

	void flip(){
		facingRight=!facingRight;
		Vector3 scaler=transform.localScale;
		scaler.x*=-1;
		transform.localScale=scaler;
	}
    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
        dazedTime=StartDazedTime;
        health-=damage;
        Debug.Log("damague taken!!");
		if(health <= 0)
			Dead();
    }
    public void Dead()
    {
        //camRipple.RippleEffect();
        Instantiate(corpse,transform.position,Quaternion.identity);
        Instantiate(bloodSplash,transform.position,Quaternion.identity);
        Instantiate(deathEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
