using System.Collections;
using UnityEngine;

public class IAMano : MonoBehaviour,ITakeDamage {
    Animator anim;
    Rigidbody2D rb;
    public GameObject player;
    public GameObject[] waypoints;


    public Transform dPosition;

    public bool returnAtack=true;

    public float dir;

	void Start () 
    {
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
	}
	void Update () 
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        anim.SetFloat("Distance", distance);
        
    }
    
    public GameObject GetPlayer()
    {
        return player;
    }
    public void WAtack()
    {
        StartCoroutine(WaitToAtact());
    }
    public void WToDo(float t, string trigger)
    {
        StartCoroutine(WaitToDo(t,trigger));
    }
    public IEnumerator WaitToAtact()
    {
        yield return new WaitForSeconds(4);
        returnAtack=true;
    }
    public IEnumerator WaitToDo(float t, string trigger)
    {
        yield return new WaitForSeconds(t);
        anim.SetTrigger(trigger);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.layer==10)
        {
            rb.bodyType=RigidbodyType2D.Kinematic;
            rb.velocity=Vector2.zero;
            StartCoroutine(WaitToDo(4,"Return"));
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.tag=="Player"&&returnAtack)
        {
            returnAtack=false;
            Debug.Log("Detectado");
            anim.SetTrigger("Attack");
            
        }
    }
    private void OnTriggerexit2D(Collider2D other) 
    {
    }

    public void TakeDamage(int damage)
    {
        
    }
}