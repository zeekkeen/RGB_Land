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
    public Direction direction;

    public enum Direction{
        top,
        down,
        side
    }

    void Start (){
        camAnim=Camera.main.GetComponent<Animator>();
        playerAnim=GetComponentInChildren<Animator>();
        direction = Direction.side;
    }
    void Update(){
        if(Input.GetKey(KeyCode.UpArrow))
            direction = Direction.top;
        if(Input.GetKey(KeyCode.DownArrow))
            direction = Direction.down;
        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            direction = Direction.side;
        if(timeBtwMeleeAttack <= 0){
            if(Input.GetKey(KeyCode.J)){
                Collider2D[] enemiesToDamage;
                switch(direction){
                    case Direction.side:
                        enemiesToDamage=Physics2D.OverlapBoxAll(attackPos.position,attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            enemiesToDamage[i].GetComponent<Enemy1>().TakeDamage(damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                    case Direction.top:
                        enemiesToDamage=Physics2D.OverlapBoxAll(transform.position + new Vector3(0,3f,0),attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            enemiesToDamage[i].GetComponent<Enemy1>().TakeDamage(damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                    case Direction.down:
                        enemiesToDamage=Physics2D.OverlapBoxAll(transform.position + new Vector3(0,-1.5f,0),attackRange,0,whatIsEnemies);
                        playerAnim.SetTrigger("attack");
                        for(int i=0;i<enemiesToDamage.Length;i++){
                            enemiesToDamage[i].GetComponent<Enemy1>().TakeDamage(damage);
                            //camAnim.SetTrigger("shake");
                        }
                            break;
                }
                direction = Direction.side;
                // playerAnim.SetTrigger("attack");
                // //Collider2D[] enemiesToDamage=Physics2D.OverlapCircleAll(attackPos.position,attackRange,whatIsEnemies);
                // //Collider2D[] enemiesToDamage=Physics2D.OverlapBoxAll(attackPos.position,attackRange,0,whatIsEnemies);
                // for(int i=0;i<enemiesToDamage.Length;i++)
                // {
                //     enemiesToDamage[i].GetComponent<Enemy>().TakeDamage(damage);
                //     camAnim.SetTrigger("shake");
                // }
                timeBtwMeleeAttack = startTimeBtwMeleeAttack;
            }
        }else
        {
            timeBtwMeleeAttack-=Time.deltaTime;
        }
        if(timeBtwRangedAttack<=0)
        {
            if(Input.GetKey(KeyCode.L)){
                GameObject instance;
                switch(direction){
                    case Direction.side:
                        instance = (GameObject) Instantiate(proyectile,attackPos.position,transform.rotation);
                        playerAnim.SetTrigger("attack");
                            break;
                    case Direction.top:
                        instance = (GameObject) Instantiate(proyectile,transform.position + new Vector3(0,3f,0),Quaternion.Euler(0,0,90));
                        playerAnim.SetTrigger("attack");
                            break;
                    case Direction.down:
                        instance = (GameObject) Instantiate(proyectile,transform.position + new Vector3(0,-1.5f,0),Quaternion.Euler(0,0,-90));
                        playerAnim.SetTrigger("attack");
                            break;
                }
                direction = Direction.side;
                // GameObject instance=(GameObject) Instantiate(proyectile,attackPos.position,transform.rotation);
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
        // Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
        switch(direction){
                    case Direction.side:
                        Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
                            break;
                    case Direction.top:
                        Gizmos.DrawWireCube(transform.position + new Vector3(0,3f,0),new Vector3(attackRange.x,attackRange.y,1));
                            break;
                    case Direction.down:
                        Gizmos.DrawWireCube(transform.position + new Vector3(0,-1.5f,0),new Vector3(attackRange.x,attackRange.y,1));
                            break;
                }
    }
}
