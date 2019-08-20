using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Controller2D))]
public class Projectile : MonoBehaviour
{
    public int damage=10;
    public GameObject destroyEffect;
    Controller2D controller;

	Vector2 directionalInput;
	bool facingRight=true;
	float moveTime=0f;
	public float startTime=1f,Speed=10f;

	void Start() {
		controller = GetComponent<Controller2D> ();
        moveTime=startTime;
	}

	void Update() {
        if(transform.localScale.x>0)controller.Move (new Vector3(Time.deltaTime*Speed*1,0,0), directionalInput);
        else controller.Move (new Vector3(Time.deltaTime*Speed*(-1),0,0), directionalInput);
			
		moveTime-=Time.deltaTime;
		if(moveTime<=0)
		{
			moveTime=0;
            DestroyProjectil();
		}
		if (controller.collisions.above || controller.collisions.below || controller.collisions.left || controller.collisions.right) {
			if (controller.collisions.hitGO.CompareTag("Enemy"))
            {
                controller.collisions.hitGO.GetComponent<Enemy>().TakeDamage(damage);
            }
            DestroyProjectil();
		}
	}

    void DestroyProjectil()
    {
        Instantiate(destroyEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
