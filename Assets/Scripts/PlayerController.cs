using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    float horizontalInput,verticalInput;

    Rigidbody2D rb;

    bool facingRigth=true;
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
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
    }

    void FixedUpdate()
    {
        isGrounded=Physics2D.OverlapCircle(groundCheck.position,checkRadius,whatIsGround);
        horizontalInput=Input.GetAxis("Horizontal");
        rb.velocity=new Vector2(horizontalInput * speed, rb.velocity.y);

        if(!facingRigth && horizontalInput > 0)
            flip();
        else if(facingRigth && horizontalInput < 0)
            flip();
        
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

    void Update() {
        if (isGrounded)
            extraJumps = extraJumpsValue;
        if (Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
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
    void flip()
    {
        facingRigth=!facingRigth;
        Vector3 scaler =transform.localScale;
        scaler.x*=-1;
        transform.localScale=scaler;
    }
}
