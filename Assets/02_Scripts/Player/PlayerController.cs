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
    float movementInputX;

    void Awake() {
        inputAction = new PlayerControls();
        inputAction.GamePlay.MoveLeft.performed += ctx => MoveLeft();
        inputAction.GamePlay.MoveRight.performed += ctx => MoveRight();
        // inputAction.GamePlay.MoveLeft.canceled += ctx => MoveCanceled();
        // inputAction.GamePlay.MoveRight.canceled += ctx => MoveCanceled();
        inputAction.GamePlay.move.canceled += ctx => MoveCanceled();
        inputAction.GamePlay.Jump.performed += ctx => Jump();
        inputAction.GamePlay.Jump.canceled += ctx => JumpKeyUp();
        transform.position = GameManager.instance.playerData.lastPosition;
        anim.SetBool("IsDead",false);
    }

    void OnEnable() {
        inputAction.Enable();
    }

    void OnDisable() {
        inputAction.Disable();
    }

    void MoveLeft(){
        movementInputX = -1;
    }

    void MoveRight(){
        movementInputX = 1;
    }

    void MoveCanceled(){
        movementInputX = 0;
    }
    
    void Start(){
        rb  = GetComponent<Rigidbody2D>();
        // anim = GetComponentInChildren<Animator>();
    }

    void FixedUpdate() {
        
    }

    void Update(){
        if(!GameManager.instance.playerData.inPause){
            GameManager.instance.playerStats.isGrounded = Physics2D.OverlapCircle(groundPos.position, GameManager.instance.playerStats.checkRadius, whatIsGround);

            if(GameManager.instance.playerStats.isGrounded){
                rb.sharedMaterial.friction = 0.4f;
                // rb.gravityScale = 1;
            }
            else{
                rb.sharedMaterial.friction = 0;
                // rb.gravityScale = 5;
            }

            if(GameManager.instance.playerStats.isGrounded){
                GameManager.instance.playerStats.doubleJump = false;
                anim.SetBool("IsJumping", false);
            }
            else
                anim.SetBool("IsJumping", true);
            // float moveInput = movementInputX.x;
            if(!GetComponent<PlayerAttack>().dashing)
                rb.velocity = new Vector2(movementInputX * GameManager.instance.playerStats.moveSpeed, rb.velocity.y);
            if(movementInputX == 0)
                anim.SetBool("IsRunning",false);
            else 
                anim.SetBool("IsRunning",true);

            if(movementInputX < 0){
                transform.eulerAngles = new Vector3(0,180,0);
                facingRight = false;
            }
            else if(movementInputX > 0){
                transform.eulerAngles = new Vector3(0,0,0);
                facingRight = true;
            }
        }
    }

    public void TakeDamage(int damage){
        Instantiate(dustEffect, transform.position, Quaternion.identity);
        GameManager.instance.playerStats.currentHealth --;//-= damage;
        LifesManager.instance.RefreshUI();
        if(GameManager.instance.playerStats.currentHealth <= 0){
            // Destroy(this.gameObject);
            anim.SetBool("IsDead",true);
            InGameUIManager.instance.GameOverPanel();
        }
    }

    void OnCollisionStay2D(Collision2D other) {
        if(other.gameObject.tag == "platform"){
            // transform.parent = other.transform;
            transform.SetParent(other.transform);
             GameManager.instance.playerStats.isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "platform"){
            // transform.parent = null;
            transform.SetParent(null);
             GameManager.instance.playerStats.isGrounded = false;
        }
    }

    public void Jump(){
        if(!GameManager.instance.playerData.inPause){
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
    }
    
    public void JumpKeyUp(){
        GameManager.instance.playerStats.isJumping = false;
    }
}