﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CapsuleCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour{
        
    public LayerMask whatIsGround;
    Rigidbody2D rb;
    Animator anim;
    public Transform groundPos;
    public GameObject dustEffect;

    void Start(){
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponentInChildren<Animator>();
    }

    void Update(){
        GameManager.instance.playerData.playerStats.isGrounded = Physics2D.OverlapCircle(groundPos.position, GameManager.instance.playerData.playerStats.checkRadius, whatIsGround);

        if(GameManager.instance.playerData.playerStats.isGrounded && Input.GetKeyDown(KeyCode.Space)){
            anim.SetTrigger("takeOf");
            GameManager.instance.playerData.playerStats.isJumping = true;
            GameManager.instance.playerData.playerStats.jumpTimeCounter = GameManager.instance.playerData.playerStats.jumpTime;
            rb.velocity = Vector2.up * GameManager.instance.playerData.playerStats.jumpForce;
            Instantiate(dustEffect,transform.position,Quaternion.identity);
        }

        if(GameManager.instance.playerData.playerStats.isGrounded){
            GameManager.instance.playerData.playerStats.doubleJump = false;
            anim.SetBool("IsJumping", false);
        }
        else
            anim.SetBool("IsJumping", true);

        if(GameManager.instance.playerData.playerStats.isJumping && Input.GetKeyDown(KeyCode.Space)){
            if(GameManager.instance.playerData.playerStats.jumpTimeCounter > 0){
                rb.velocity = Vector2.up * GameManager.instance.playerData.playerStats.jumpForce;
                Instantiate(dustEffect,transform.position,Quaternion.identity);
                GameManager.instance.playerData.playerStats.jumpTimeCounter -= Time.deltaTime;
            }
            else
                GameManager.instance.playerData.playerStats.isJumping = false;
        }

        if(Input.GetKeyUp(KeyCode.Space))
            GameManager.instance.playerData.playerStats.isJumping = false;

        if(!GameManager.instance.playerData.playerStats.isGrounded && !GameManager.instance.playerData.playerStats.doubleJump && Input.GetKeyDown(KeyCode.Space)){
            GameManager.instance.playerData.playerStats.isJumping = true;
            GameManager.instance.playerData.playerStats.doubleJump = true;
            GameManager.instance.playerData.playerStats.isJumping = true;
            GameManager.instance.playerData.playerStats.jumpTimeCounter = GameManager.instance.playerData.playerStats.jumpTime;
            rb.velocity = Vector2.up * GameManager.instance.playerData.playerStats.jumpForce;
            Instantiate(dustEffect,transform.position,Quaternion.identity);
        }

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * GameManager.instance.playerData.playerStats.moveSpeed, rb.velocity.y);
        if(moveInput == 0)
            anim.SetBool("IsRunning",false);
        else 
            anim.SetBool("IsRunning",true);

        if(moveInput < 0)
            transform.eulerAngles = new Vector3(0,180,0);
        else if(moveInput > 0)
            transform.eulerAngles = new Vector3(0,0,0);
    }

}