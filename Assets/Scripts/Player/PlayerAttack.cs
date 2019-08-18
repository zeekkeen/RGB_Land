using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    float timeBtwAttack;
    public float startTimeBtwAttack=0.3f;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public Animator camAnim,playerAnim;
    // public float attackRange;
    public Vector2 attackRange;
    public int damage;
    void Update()
    {
        if(timeBtwAttack<=0)
        {
            if(Input.GetKey(KeyCode.J))
            {
                camAnim.SetTrigger("shake");
                playerAnim.SetTrigger("attack");
                //Collider2D[] enemiesToDamage=Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemies);
                Collider2D[] enemiesToDamage=Physics2D.OverlapBoxAll(attackPos.position,attackRange,0,whatIsEnemies);
                for(int i=0;i<enemiesToDamage.Length;i++)
                {
                    enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            timeBtwAttack=startTimeBtwAttack;
            }
        }else
        {
            timeBtwAttack-=Time.deltaTime;
        }
    }

    void OnDrawGizmosSelected()
    {
        //Gizmos.color=Color.green;Gizmos.DrawWireSphere(attackPos.position,attackRange);
        Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
    }
}
