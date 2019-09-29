using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    float horizontalInput,verticalInput;

    Rigidbody2D rb;

    bool facingRight=true;
    bool isGrounded,isJumping;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround, whatIsLadder;
    public int extraJumps;
    public int extraJumpsValue;
    public float distance;
    bool isClimbing;
    float jumpTimeCounter;
    public float jumpTime;

    public float dashSpeed, startDashTime;
    float dashTime;
    bool dash=false;
    public GameObject dashEffect,dustEffect;
    Animator anim,camAnim;
    bool landed=false;
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        anim=GetComponentInChildren<Animator>();
		camAnim = Camera.main.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if(!dash)
        {
            isGrounded=Physics2D.OverlapCircle(groundCheck.position,checkRadius,whatIsGround);
            horizontalInput=Input.GetAxis("Horizontal");
            rb.velocity=new Vector2(horizontalInput * speed, rb.velocity.y);

            if(!facingRight && horizontalInput > 0)
                flip();
            else if(facingRight && horizontalInput < 0)
                flip();
            if(horizontalInput != 0)
                anim.SetBool("isRunning",true);
            else 
                anim.SetBool("isRunning",false);
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,Vector2.up,distance,whatIsLadder);
            if (hitInfo.collider != null)
            {
                if(Input.GetKeyDown(KeyCode.UpArrow))
                    isClimbing=true;
            }else
            {
                if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
                isClimbing=false;
            }
            if (isClimbing && hitInfo.collider != null)
            {
                verticalInput=Input.GetAxisRaw("Vertical");
                rb.velocity=new Vector2(rb.velocity.x,verticalInput * speed);
                rb.gravityScale=0;
            }else
                rb.gravityScale=5;
        }
        
    }

    void Update() {
        if (isGrounded && !landed)
        {
            landed=true;
            extraJumps = extraJumpsValue;
            anim.SetTrigger("Landed");
			camAnim.SetTrigger("shake");
			Instantiate(dustEffect,transform.position,Quaternion.identity);
        }
        if(!dash)
		{
            if (Input.GetKeyDown(KeyCode.X))
            {
                dash=true;
                dashTime=startDashTime;
                Instantiate(dashEffect,transform.position,Quaternion.identity);
            }
            if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
            {
                anim.SetTrigger("Jump");
                landed=false;
                if (extraJumps == 1)
                {
                    Instantiate(dustEffect,transform.position,Quaternion.identity);
                    camAnim.SetTrigger("shake");
                }
                isJumping=true;
                jumpTimeCounter=jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                extraJumps--;
            }else if(Input.GetKeyDown(KeyCode.Space) && extraJumps == 0 && isGrounded)
            rb.velocity = Vector2.up * jumpForce;
            if (Input.GetKey(KeyCode.Space) && isJumping)
            {
                if(jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter-=Time.deltaTime;
                }else
                {
                    isJumping=false;
                }
            }
            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping=false;
            }
        }
        else if(dash)
		{
            rb.velocity = ((facingRight)?Vector2.right*dashSpeed:Vector2.left*dashSpeed);
			dashTime-=Time.deltaTime;
			if(dashTime<=0)
			{
				dashTime=0;
                rb.velocity = Vector2.zero;
				dash=false;
				Instantiate(dashEffect,transform.position,Quaternion.identity);
			}
		}
    }
    void flip()
    {
        facingRight=!facingRight;
        Vector3 scaler =transform.localScale;
        scaler.x*=-1;
        transform.localScale=scaler;
    }
}
