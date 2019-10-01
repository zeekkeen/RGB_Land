using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
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
    ////

    public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	public float moveSpeed = 6;

	public Vector2 wallJumpClimb;
	public Vector2 wallJumpOff;
	public Vector2 wallLeap;

	public float wallSlideSpeedMax = 3;
	public float wallStickTime = .25f;
	float timeToWallUnstick;

	float gravity;
	float maxJumpVelocity;
	float minJumpVelocity;
	Vector3 velocity;
	float velocityXSmoothing;


	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;
	bool facingRight=true;
	float dashTime=0f;
	public float startDashTime=0.1f,dashSpeed=7f,distance;
	bool dash=false;
	public GameObject dashEffect,groundDetection;
    void Start()
    {
        //anim=GetComponent<Animator>();
		anim=GetComponentInChildren<Animator>();
        anim.SetBool("isRunning",true);
        //camRipple=Camera.main.GetComponent<RipplePostProcessor>();

        //controller = GetComponent<Controller2D> ();
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
        directionalInput=new Vector2(1,0);
    }

	void Update() {
		
	}

	void flip(){
		facingRight=!facingRight;
		Vector3 scaler=transform.localScale;
		scaler.x*=-1;
		transform.localScale=scaler;
        directionalInput*=-1;
	}
    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
        dazedTime=StartDazedTime;
        health-=damage;
        Debug.Log("damague taken!!");
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
