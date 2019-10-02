using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float timeBtwMeleeAttack,timeBtwRangedAttack;
    public float startTimeBtwMeleeAttack=0.3f,startTimeBtwRangedAttack=0.3f;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator camAnim,playerAnim;
    // public float attackRange;
    public Vector2 attackRange;
    public int damage;
    public GameObject proyectile;

    void Start ()
    {
        camAnim=Camera.main.GetComponent<Animator>();
        playerAnim=GetComponentInChildren<Animator>();
    }
    void Update()
    {
        if(timeBtwMeleeAttack<=0)
        {
            if(Input.GetKey(KeyCode.J))
            {
                playerAnim.SetTrigger("attack");
                //Collider2D[] enemiesToDamage=Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemies);
                Collider2D[] enemiesToDamage=Physics2D.OverlapBoxAll(attackPos.position,attackRange,0,whatIsEnemies);
                for(int i=0;i<enemiesToDamage.Length;i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                    camAnim.SetTrigger("shake");
                }
                timeBtwMeleeAttack=startTimeBtwMeleeAttack;
            }
        }else
        {
            timeBtwMeleeAttack-=Time.deltaTime;
        }
        if(timeBtwRangedAttack<=0)
        {
            if(Input.GetKey(KeyCode.L))
            {
                playerAnim.SetTrigger("attack");
                GameObject instance=(GameObject) Instantiate(proyectile,attackPos.position,transform.rotation);
                // if(transform.rotation.y == 0)
                //     instance.transform.localScale=new Vector3(-1*instance.transform.localScale.x,instance.transform.localScale.y,instance.transform.localScale.z);
                timeBtwRangedAttack=startTimeBtwRangedAttack;
            }
        }else
        {
            timeBtwRangedAttack-=Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.color=Color.green;Gizmos.DrawWireSphere(attackPos.position,attackRange);
        Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
    }
}
