using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (CapsuleCollider2D))]
[RequireComponent (typeof (Rigidbody2D))]
public class PlayerController : MonoBehaviour, ITakeDamage{
        
    public LayerMask whatIsGround;
    public Rigidbody2D rb;
    public Animator anim;
    public Transform groundPos;
    public GameObject dustEffect;
    public bool facingRight = true;
    PlayerControls inputAction;
    Vector2 movementInput;

    void Awake() {
        inputAction = new PlayerControls();
        inputAction.GamePlay.move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputAction.GamePlay.move.canceled += ctx => movementInput = Vector2.zero;
        inputAction.GamePlay.Jump.performed += ctx => Jump();
        inputAction.GamePlay.Jump.canceled += ctx => JumpKeyUp();
    }

    void OnEnable() {
        inputAction.Enable();
    }

    void OnDisable() {
        inputAction.Disable();
    }

    void Start(){
        transform.position = GameManager.instance.playerData.lastPosition;
        rb  = GetComponent<Rigidbody2D>();
        // anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate() {
        
    }

    void Update(){
        GameManager.instance.playerStats.isGrounded = Physics2D.OverlapCircle(groundPos.position, GameManager.instance.playerStats.checkRadius, whatIsGround);

        if(GameManager.instance.playerStats.isGrounded){
            rb.sharedMaterial.friction = 0.4f;
            // rb.gravityScale = 1;
        }
        else{
            rb.sharedMaterial.friction = 0;
            // rb.gravityScale = 5;
        }
        // if(GameManager.instance.playerStats.isGrounded && Input.GetKeyDown(KeyCode.Space)){
        //     anim.SetTrigger("takeOf");
        //     GameManager.instance.playerStats.isJumping = true;
        //     GameManager.instance.playerStats.jumpTimeCounter = GameManager.instance.playerStats.jumpTime;
        //     rb.velocity = Vector2.up * GameManager.instance.playerStats.jumpForce;
        //     Instantiate(dustEffect,transform.position,Quaternion.identity);
        // }

        if(GameManager.instance.playerStats.isGrounded){
            GameManager.instance.playerStats.doubleJump = false;
            anim.SetBool("IsJumping", false);
        }
        else
            anim.SetBool("IsJumping", true);

        // if(GameManager.instance.playerStats.isJumping && Input.GetKeyDown(KeyCode.Space)){
        //     if(GameManager.instance.playerStats.jumpTimeCounter > 0){
        //         rb.velocity = Vector2.up * GameManager.instance.playerStats.jumpForce;
        //         Instantiate(dustEffect,transform.position,Quaternion.identity);
        //         GameManager.instance.playerStats.jumpTimeCounter -= Time.deltaTime;
        //     }
        //     else
        //         GameManager.instance.playerStats.isJumping = false;
        // }

        // if(Input.GetKeyUp(KeyCode.Space))
        //     GameManager.instance.playerStats.isJumping = false;

        // if(!GameManager.instance.playerStats.isGrounded && !GameManager.instance.playerStats.doubleJump && Input.GetKeyDown(KeyCode.Space)){
        //     // GameManager.instance.playerStats.isJumping = true;
        //     GameManager.instance.playerStats.doubleJump = true;
        //     GameManager.instance.playerStats.isJumping = true;
        //     GameManager.instance.playerStats.jumpTimeCounter = GameManager.instance.playerStats.jumpTime;
        //     rb.velocity = Vector2.up * GameManager.instance.playerStats.jumpForce;
        //     Instantiate(dustEffect,transform.position,Quaternion.identity);
        // }

        float moveInput = movementInput.x;
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
        GameManager.instance.playerStats.currentHealth --;//-= damage;
        LifesManager.instance.RefreshUI();
        if(GameManager.instance.playerStats.currentHealth <= 0)
            Destroy(this.gameObject);
    }

    void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "platform"){
            transform.parent = other.transform;
             GameManager.instance.playerStats.isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "platform"){
            transform.parent = null;
             GameManager.instance.playerStats.isGrounded = false;
        }
    }

    public void Jump(){
        if(GameManager.instance.playerStats.isGrounded){
            anim.SetTrigger("takeOf");
            GameManager.instance.playerStats.isJumping = true;
            GameManager.instance.playerStats.jumpTimeCounter = GameManager.instance.playerStats.jumpTime;
            rb.velocity = Vector2.up * GameManager.instance.playerStats.jumpForce;
            Instantiate(dustEffect,transform.position,Quaternion.identity);
        }
        //
        if(GameManager.instance.playerStats.isJumping){
            if(GameManager.instance.playerStats.jumpTimeCounter > 0){
                rb.velocity = Vector2.up * GameManager.instance.playerStats.jumpForce;
                Instantiate(dustEffect,transform.position,Quaternion.identity);
                GameManager.instance.playerStats.jumpTimeCounter -= Time.deltaTime;
            }
            else
                GameManager.instance.playerStats.isJumping = false;
        }
        //
        if(!GameManager.instance.playerStats.isGrounded && !GameManager.instance.playerStats.doubleJump){
            GameManager.instance.playerStats.doubleJump = true;
            GameManager.instance.playerStats.isJumping = true;
            GameManager.instance.playerStats.jumpTimeCounter = GameManager.instance.playerStats.jumpTime;
            rb.velocity = Vector2.up * GameManager.instance.playerStats.jumpForce;
            Instantiate(dustEffect,transform.position,Quaternion.identity);
        }
    }
    
    public void JumpKeyUp(){
        GameManager.instance.playerStats.isJumping = false;
    }
}