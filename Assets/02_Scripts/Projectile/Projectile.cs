using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int damage=10;
    public GameObject destroyEffect;
	Rigidbody2D rb;

	Vector2 directionalInput;
	bool facingRight=true;
	float moveTime=0f;
	public float startTime=1f,speed=10f;

	void Start() {
		rb = GetComponent<Rigidbody2D> ();
        moveTime=startTime;
	}

	void Update() {
        if(transform.localScale.x>0)
			rb.velocity = new Vector2(1 * speed * Time.deltaTime, rb.velocity.y);
        else 
			rb.velocity = new Vector2(-1 * speed * Time.deltaTime, rb.velocity.y);
			
		moveTime-=Time.deltaTime;
		if(moveTime<=0)
		{
			moveTime=0;
            DestroyProjectil();
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
			if (other.gameObject.CompareTag("Enemy"))
            {
                other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            DestroyProjectil();
	}

    void DestroyProjectil()
    {
        Instantiate(destroyEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
}
