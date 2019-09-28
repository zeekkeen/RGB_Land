using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpForce;
    float moveInput;

    Rigidbody2D rb;

    bool facingRigth=true;
    bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public int extraJumps;
    public int extraJumpsValue;
    
    void Start()
    {
        rb=GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
    }

    void FixedUpdate()
    {
        isGrounded=Physics2D.OverlapCircle(groundCheck.position,checkRadius,whatIsGround);
        moveInput=Input.GetAxis("Horizontal");
        rb.velocity=new Vector2(moveInput*speed,rb.velocity.y);

        if(!facingRigth && moveInput > 0)
            flip();
        else if(facingRigth && moveInput < 0)
            flip();
    }

    void Update() {
        if (isGrounded)
            extraJumps = extraJumpsValue;
        if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }else if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded)
        rb.velocity = Vector2.up * jumpForce;
    }
    void flip()
    {
        facingRigth=!facingRigth;
        Vector3 scaler =transform.localScale;
        scaler.x*=-1;
        transform.localScale=scaler;
    }
}
