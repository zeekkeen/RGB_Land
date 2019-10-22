using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    public int damage = 10;
    public GameObject destroyEffect;
	Rigidbody2D rb;
	// bool facingRight=true;
	float moveTime=0f;
	public float startTime=1f,speed=10f;

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
        moveTime=startTime;
	}

	void Update() {
		rb.velocity = transform.up * speed * Time.deltaTime;
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
			takeDamage.TakeDamage(damage);
		DestroyProjectil();
	}
	private void OnCollisionEnter2D(Collision2D other) {
		ITakeDamage takeDamage = other.gameObject.GetComponent<ITakeDamage>();
		if(takeDamage != null)
			takeDamage.TakeDamage(damage);
		DestroyProjectil();
	}

    void DestroyProjectil()
    {
        Instantiate(destroyEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}