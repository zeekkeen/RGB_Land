using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damageMultiplier;
    public GameObject destroyEffect;
	Rigidbody2D rb;
	bool facingRight = true;
	float moveTime = 0f;
	public float startTime = 1f, speed = 10f;

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
        moveTime = startTime;
	}

	void Update() {
        if(transform.rotation.y == 0 && transform.rotation.z == 0)
			rb.velocity = new Vector2(1 * speed * Time.deltaTime, rb.velocity.y);
        else if(transform.rotation.y != 0 && transform.rotation.z == 0)
			rb.velocity = new Vector2(-1 * speed * Time.deltaTime, rb.velocity.y);
		else if(transform.rotation.z > 0 && transform.rotation.y == 0)
			rb.velocity = new Vector2(rb.velocity.x, 1 * speed * Time.deltaTime);
		else 
			rb.velocity = new Vector2(rb.velocity.x, -1 * speed * Time.deltaTime);
		moveTime-=Time.deltaTime;
		if(moveTime<=0)
		{
			moveTime=0;
            DestroyProjectil();
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		ITakeDamage takeDamage = other.gameObject.GetComponent<ITakeDamage>();
		if(takeDamage != null)
			takeDamage.TakeDamage((int)(GameManager.instance.playerStats.damage * damageMultiplier));
		DestroyProjectil();
	}

	private void OnCollisionEnter2D(Collision2D other) {
		ITakeDamage takeDamage = other.gameObject.GetComponent<ITakeDamage>();
		if(takeDamage != null)
			takeDamage.TakeDamage((int)(GameManager.instance.playerStats.damage * damageMultiplier));
		DestroyProjectil();
	}

    void DestroyProjectil(){
        Instantiate(destroyEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}