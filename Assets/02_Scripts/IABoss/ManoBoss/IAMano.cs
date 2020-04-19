using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAMano : MonoBehaviour {

    public EnemyStats_SO statsTemplate;
    public EnemyStats_SO enemyStats;

    Animator anim;
    Rigidbody2D rb;
    public GameObject player;
    public GameObject[] waypoints;
    public List<SpriteRenderer> lifeRender;
    public List<SpriteRenderer> torsolifeRender;

    public Transform dPosition;

    public bool returnAtack=true;
    public bool puedeAtacar=false;
    bool muerto=false;

    public float dir;


    public Animator torsoAnim;

	void Start () 
    {
        enemyStats = Instantiate(statsTemplate);
        enemyStats.maxHealth = enemyStats.currentHealth;

        player=GameObject.FindGameObjectWithTag("Player");
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
	}
	void Update () 
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        anim.SetFloat("Distance", distance);
        if(distance<40&&returnAtack)
        {
            returnAtack=false;
            //Debug.Log("Detectado");
            anim.SetTrigger("Attack");
            
        }
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
        if(other.gameObject.tag == "Player"){
            if(puedeAtacar)
            {
                ITakeDamage takeDamage = other.gameObject.GetComponent<ITakeDamage>();
                if(takeDamage != null){
                    takeDamage.TakeDamage(1);
                    //Vector3 vec = other.transform.position - transform.position;
                    //other.gameObject.GetComponent<Rigidbody2D>().AddForce(vec.normalized * force * (-1));
                }
            }
        }   
    }
    public void ChangeColor1(){
        float colored, percentOfHealth;
        percentOfHealth = (enemyStats.currentHealth*100) / enemyStats.maxHealth;
        colored = 1 - (percentOfHealth / 100);
        foreach (SpriteRenderer element in lifeRender){
            switch (enemyStats.typeOfColor){
                    case TypeOfColor.red:
                        element.color = new Color(element.color.r, colored, colored);
                        break;
                    case TypeOfColor.green:
                        element.color = new Color(colored, element.color.g, colored);
                        break;
                    case TypeOfColor.blue:
                        element.color = new Color(colored, colored, element.color.b);
                        break;
                }
        }
    }

    public void Dead(){
        /* camRipple.RippleEffect();
        Instantiate(corpse,transform.position, Quaternion.identity);
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        Instantiate(deathEffect, transform.position, Quaternion.identity); */
        anim.SetBool("death",true);
        torsoAnim.SetBool("Morir"+dir,true);
        GameManager.instance.AddAchievement(Achievement.RangeAttack1);
        Destroy(gameObject,1.7f);
    }

    public void TakeDamage(int damage)
    {
        //Instantiate(bloodEffect, transform.position, Quaternion.identity);
        // enemyStats.dazedTime = enemyStats.StartDazedTime;
        enemyStats.currentHealth -= damage;
        ChangeColor1();
        torsoAnim.SetTrigger("RecibeAtaque");
        Debug.Log("damague taken!!");
		if(enemyStats.currentHealth <= 0 && !muerto){
            muerto=true;
            Dead();
            foreach (SpriteRenderer element in torsolifeRender){
            element.color+=new Color(0.5f,0,0.5f);
            }
            torsoAnim.SetBool("Morrir"+dir,true);
            if(torsoAnim.GetBool("Morir-1")&&torsoAnim.GetBool("Morir1")){
                Destroy(torsoAnim.transform.parent.gameObject,3f);
            }

        }
            //torsoAnim.SetTrigger("Morir");
			
    }
}