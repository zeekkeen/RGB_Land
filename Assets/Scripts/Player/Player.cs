using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Controller2D))]
public class Player : MonoBehaviour {

	public float maxJumpHeight = 4;
	public float minJumpHeight = 1;
	public float timeToJumpApex = .4f;
	float accelerationTimeAirborne = .2f;
	float accelerationTimeGrounded = .1f;
	float moveSpeed = 6;

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

	Controller2D controller;

	Vector2 directionalInput;
	bool wallSliding;
	int wallDirX;
	bool facingRight=true,Landed=false;
	float dashTime=0f;
	public float startDashTime=0.1f,dashSpeed=7f,timeBtwTrail=0,startTimeBtwTrail=0.5f;
	bool dash=false;
	public GameObject dashEffect,dustEffect;
	Animator anim,camAnim;

	void Start() {
		controller = GetComponent<Controller2D> ();
		anim=GetComponentInChildren<Animator>();
		camAnim = Camera.main.GetComponent<Animator>();
		gravity = -(2 * maxJumpHeight) / Mathf.Pow (timeToJumpApex, 2);
		maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
		minJumpVelocity = Mathf.Sqrt (2 * Mathf.Abs (gravity) * minJumpHeight);
	}

	void Update() {
		CalculateVelocity ();
		HandleWallSliding ();
		if(!dash)
		{
			if(velocity.x!=0 && controller.collisions.below)anim.SetBool("isRunning",true);
			if(controller.playerInput.x==0 || !controller.collisions.below)anim.SetBool("isRunning",false);
			controller.Move (velocity * Time.deltaTime, directionalInput);
			if (directionalInput.x!=0 || directionalInput.y!=0)
			{
				if (timeBtwTrail<=0)
				{
					Instantiate(dustEffect,transform.position-Vector3.down*(-0.5f),Quaternion.identity);
					timeBtwTrail=startTimeBtwTrail;
				}else
				{
					timeBtwTrail-=Time.deltaTime;
				}
			}
		}
		else if(dash)
		{
			controller.Move (new Vector3(Time.deltaTime*dashSpeed*((facingRight)?1:-1),0,0), directionalInput);
			dashTime-=Time.deltaTime;
			if(dashTime<=0)
			{
				dashTime=0;
				dash=false;
				Instantiate(dashEffect,transform.position,Quaternion.identity);
			}
		}
		if (controller.collisions.above || controller.collisions.below) {
			velocity.y = 0;
		}
		if (controller.collisions.below && !Landed) {
			Landed=true;
			anim.SetTrigger("Landed");
			camAnim.SetTrigger("shake");
			Instantiate(dustEffect,transform.position,Quaternion.identity);
		}
	}

	public void SetDirectionalInput (Vector2 input) {
		directionalInput = input;
		if(!facingRight&&directionalInput.x>0)flip();
		else if(facingRight&&directionalInput.x<0)flip();
	}

	public void OnJumpInputDown() {
		if (wallSliding) {
			controller.jumpCount=0;
			if (wallDirX == directionalInput.x) {
				velocity.x = -wallDirX * wallJumpClimb.x;
				velocity.y = wallJumpClimb.y;
			}
			else if (directionalInput.x == 0) {
				velocity.x = -wallDirX * wallJumpOff.x;
				velocity.y = wallJumpOff.y;
			}
			else {
				velocity.x = -wallDirX * wallLeap.x;
				velocity.y = wallLeap.y;
			}
		}
		// if (controller.collisions.below) {
		// 	velocity.y = maxJumpVelocity;
		// }
		if (controller.jumpCount < controller.maxJumpCount) {
			Landed=false;
			velocity.y = maxJumpVelocity;
			controller.jumpCount++;
			anim.SetTrigger("Jump");
			if (controller.jumpCount==1)
			{
				Instantiate(dustEffect,transform.position,Quaternion.identity);
				camAnim.SetTrigger("shake");
			}
		}
	}

	public void OnJumpInputUp() {
		if (velocity.y > minJumpVelocity) {
			velocity.y = minJumpVelocity;
		}
	}

	public void OnDashInputDown() {
		dash=true;
		dashTime=startDashTime;
		Instantiate(dashEffect,transform.position,Quaternion.identity);
	}
		

	void HandleWallSliding() {
		wallDirX = (controller.collisions.left) ? -1 : 1;
		wallSliding = false;
		if ((controller.collisions.left || controller.collisions.right) && !controller.collisions.below && velocity.y < 0) {
			wallSliding = true;

			if (velocity.y < -wallSlideSpeedMax) {
				velocity.y = -wallSlideSpeedMax;
			}

			if (timeToWallUnstick > 0) {
				velocityXSmoothing = 0;
				velocity.x = 0;

				if (directionalInput.x != wallDirX && directionalInput.x != 0) {
					timeToWallUnstick -= Time.deltaTime;
				}
				else {
					timeToWallUnstick = wallStickTime;
				}
			}
			else {
				timeToWallUnstick = wallStickTime;
			}

		}

	}

	void CalculateVelocity() {
		float targetVelocityX = directionalInput.x * moveSpeed;
		velocity.x = Mathf.SmoothDamp (velocity.x, targetVelocityX, ref velocityXSmoothing, (controller.collisions.below)?accelerationTimeGrounded:accelerationTimeAirborne);
		velocity.y += gravity * Time.deltaTime;
	}

	void flip(){
		facingRight=!facingRight;
		Vector3 scaler=transform.localScale;
		scaler.x*=-1;
		transform.localScale=scaler;
	}
}
