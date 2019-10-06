using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (CapsuleCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class Controller2D : MonoBehaviour{
    
    public Rigidbody2D rb;
    public Animator anim;
    public float moveSpeed, jumpForce;
    public Transform groundPos;
    public bool isGrounded;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpTime, jumpTimeCounter;
    public bool isJumping, doubleJump;
    public GameObject dustEffect;

    void Start(){
        rb=GetComponent<Rigidbody2D>();
        anim=GetComponentInChildren<Animator>();
    }

    void Update(){
        isGrounded = Physics2D.OverlapCircle(groundPos.position, checkRadius, whatIsGround);

        if(isGrounded && Input.GetKeyDown(KeyCode.Space)){
            anim.SetTrigger("takeOf");
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            Instantiate(dustEffect,transform.position,Quaternion.identity);
        }

        if(isGrounded){
            doubleJump = false;
            anim.SetBool("IsJumping", false);
        }
        else
            anim.SetBool("IsJumping", true);

        if(isJumping && Input.GetKeyDown(KeyCode.Space)){
            if(jumpTimeCounter > 0){
                rb.velocity = Vector2.up * jumpForce;
                Instantiate(dustEffect,transform.position,Quaternion.identity);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
                isJumping = false;
        }

        if(Input.GetKeyUp(KeyCode.Space))
            isJumping = false;

        if(!isGrounded && !doubleJump && Input.GetKeyDown(KeyCode.Space)){
            isJumping = true;
            doubleJump = true;
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = Vector2.up * jumpForce;
            Instantiate(dustEffect,transform.position,Quaternion.identity);
        }

        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
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