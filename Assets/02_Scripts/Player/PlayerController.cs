using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CapsuleCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour, ITakeDamage{
        
    public LayerMask whatIsGround;
    public Rigidbody2D rb;
    public Animator anim;
    public Transform groundPos;
    public GameObject dustEffect;
    public bool facingRight = true;

    void Start(){
        transform.position = GameManager.instance.playerData.lastPosition;
        rb  = GetComponent<Rigidbody2D>();
        // anim = GetComponentInChildren<Animator>();
    }

    void Update(){
        GameManager.instance.playerStats.isGrounded = Physics2D.OverlapCircle(groundPos.position, GameManager.instance.playerStats.checkRadius, whatIsGround);

        if(GameManager.instance.playerStats.isGrounded && Input.GetKeyDown(KeyCode.Space)){
            anim.SetTrigger("takeOf");
            GameManager.instance.playerStats.isJumping = true;
            GameManager.instance.playerStats.jumpTimeCounter = GameManager.instance.playerStats.jumpTime;
            rb.velocity = Vector2.up * GameManager.instance.playerStats.jumpForce;
            Instantiate(dustEffect,transform.position,Quaternion.identity);
        }

        if(GameManager.instance.playerStats.isGrounded){
            GameManager.instance.playerStats.doubleJump = false;
            anim.SetBool("IsJumping", false);
        }
        else
            anim.SetBool("IsJumping", true);

        if(GameManager.instance.playerStats.isJumping && Input.GetKeyDown(KeyCode.Space)){
            if(GameManager.instance.playerStats.jumpTimeCounter > 0){
                rb.velocity = Vector2.up * GameManager.instance.playerStats.jumpForce;
                Instantiate(dustEffect,transform.position,Quaternion.identity);
                GameManager.instance.playerStats.jumpTimeCounter -= Time.deltaTime;
            }
            else
                GameManager.instance.playerStats.isJumping = false;
        }

        if(Input.GetKeyUp(KeyCode.Space))
            GameManager.instance.playerStats.isJumping = false;

        if(!GameManager.instance.playerStats.isGrounded && !GameManager.instance.playerStats.doubleJump && Input.GetKeyDown(KeyCode.Space)){
            // GameManager.instance.playerStats.isJumping = true;
            GameManager.instance.playerStats.doubleJump = true;
            GameManager.instance.playerStats.isJumping = true;
            GameManager.instance.playerStats.jumpTimeCounter = GameManager.instance.playerStats.jumpTime;
            rb.velocity = Vector2.up * GameManager.instance.playerStats.jumpForce;
            Instantiate(dustEffect,transform.position,Quaternion.identity);
        }

        float moveInput = Input.GetAxisRaw("Horizontal");
        if(!GetComponent<PlayerAttack>().dashing)
        rb.velocity = new Vector2(moveInput * GameManager.instance.playerStats.moveSpeed, rb.velocity.y);
        if(moveInput == 0)
            anim.SetBool("IsRunning",false);
        else 
            anim.SetBool("IsRunning",true);

        if(moveInput < 0){
            transform.eulerAngles = new Vector3(0,180,0);
            facingRight = false;
        }
        else if(moveInput > 0){
            transform.eulerAngles = new Vector3(0,0,0);
            facingRight = true;
        }
    }

    public void TakeDamage(int damage){
        Instantiate(dustEffect, transform.position, Quaternion.identity);
        GameManager.instance.playerStats.currentHealth -= damage;
        if(GameManager.instance.playerStats.currentHealth <= 0)
            Destroy(this.gameObject);
    }

}