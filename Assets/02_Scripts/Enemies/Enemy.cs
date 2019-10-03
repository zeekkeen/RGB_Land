﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public SpriteRenderer lifeRender;
    public int health = 0,maxHealth=0;
    public float speed;
    float dazedTime;
    public float StartDazedTime = 0.6f;
    public GameObject bloodEffect,deathEffect;
    public GameObject bloodSplash,corpse;
    Animator anim;
    RipplePostProcessor camRipple;
	bool facingRight=true;
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
		Vector3 scaler=transform.localScale;
		scaler.x*=-1;
		transform.localScale=scaler;
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
        float alpha,percentOfHealth;
        percentOfHealth = (health*100) / maxHealth;
        alpha = percentOfHealth / 100;
        lifeRender.color = new Color(lifeRender.color.r, lifeRender.color.g, lifeRender.color.b, alpha);
    }
}
