using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

public class CrystalController : MonoBehaviour{
    // public Vector3 dir;
    public float moveSpeed = 5f, rotationTimer = 0, rotationTime = 2.5f;
    bool movingRight = false;
    SpriteRenderer gemRenderer;
    public GameObject startPosition, startPosition2;
    public Vector2 attackRange = new Vector2(7,7);
    public LayerMask whatIsColorObject;
    PlayerController playerController;
    Animator anim;
    bool powerActivated = false;

    // void Start(){
    //     transform.position = startPosition.transform.position;
    //     gemRenderer = GetComponentInChildren<SpriteRenderer>();
    //     anim = GetComponentInChildren<Animator>();
    //     playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    // }

    void Update(){
        if(playerController != null){
            if(playerController.rb.velocity != Vector2.zero)
                moveSpeed = 10f;
            else 
                moveSpeed = 5f;
            if(!powerActivated)
                transform.position = Vector2.MoveTowards(transform.position,(playerController.facingRight ? startPosition.transform.position : startPosition2.transform.position) , moveSpeed * Time.deltaTime);
            else 
                transform.position = Vector2.MoveTowards(transform.position,startPosition.transform.position, moveSpeed * 3 * Time.deltaTime);
        }
    }

    public void InitializeCrystal(GameObject sP1, GameObject sP2){
        gemRenderer = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        startPosition = sP1;
        startPosition2 = sP2;
        transform.position = startPosition.transform.position;
    }

    void RotateAround(){
        rotationTimer += Time.deltaTime;
        if(movingRight){
            gemRenderer.sortingOrder = 10;
            transform.Translate(moveSpeed * Time.deltaTime,0,0,Space.Self);
        }else {
            gemRenderer.sortingOrder = 0;
            transform.Translate(-moveSpeed * Time.deltaTime,0,0,Space.Self);
        }
        if(rotationTimer >= rotationTime){
            movingRight = !movingRight;
            rotationTimer = 0;
        }
        //transform.RotateAround(transform.parent.position,dir,rotationSpeed*Time.deltaTime);
    }

    public void ColorSearch(){
        transform.position = startPosition.transform.position;
        Collider2D[] colorObjects;
        colorObjects = Physics2D.OverlapBoxAll(startPosition.transform.position,attackRange,0,whatIsColorObject);
        if(colorObjects != null)
            for(int i=0;i<colorObjects.Length;i++){
                colorObjects[i].GetComponent<ColorObject>().SwitchColor();
            }
    }

    public void PowerActivated(bool state){
        powerActivated = state;
        anim.SetBool("PowerActivated", state);
    }
    // void OnDrawGizmosSelected(){
    //     Gizmos.color = Color.yellow;
    //     //Gizmos.DrawWireSphere(attackPos.position,attackRange);
    //     // Gizmos.DrawWireCube(attackPos.position,new Vector3(attackRange.x,attackRange.y,1));
    //     Gizmos.DrawWireCube(transform.position,new Vector3(attackRange.x,attackRange.y,1));
    // }

    #region navigation tasks
    
    [Task]
    public bool Move()
    {
        // rb.velocity = ((facingRigth) ? Vector2.right * enemyStats.moveSpeed : Vector2.left * enemyStats.moveSpeed);
        return true;
    }

    [Task]
    public bool Reload()
    {
        // enemyStats.attackSpeedTimer -= Time.deltaTime;
        // if(enemyStats.attackSpeedTimer <= 0)
        //     return false;
        // else 
            return true;
    }

    [Task]
    public bool NoGroundDetected()
    {
        // RaycastHit2D groundInfo = Physics2D.Raycast(noGroundDetection.transform.position, Vector2.down, enemyStats.distance, groundLayer);
        // if (!groundInfo.collider)
        //     return true;
        // else 
            return false;
    }

    [Task]
    public bool EnemyDetected()
    {
        // RaycastHit2D colliderInfo = Physics2D.Raycast(transform.position, (facingRigth ? Vector2.right : Vector2.left), enemyStats.visionDistance, playerLayer);
        // if (colliderInfo.collider)
        //     return true;
        // else 
            return false;
    }

    [Task]
    public bool EnemyClose()
    {
        // RaycastHit2D colliderInfo = Physics2D.Raycast(transform.position, (facingRigth ? Vector2.right : Vector2.left), enemyStats.attackRange, playerLayer);
        // if (colliderInfo.collider)
        //     return true;
        // else 
            return false;
    }

    [Task]
    public bool Attack()
    {
        // Collider2D[] enemiesToDamage;
        // enemiesToDamage = Physics2D.OverlapBoxAll(noGroundDetection.transform.position,new Vector2(enemyStats.attackRange, enemyStats.attackRange),0,playerLayer);
        // if (enemiesToDamage != null){
        //     for(int i=0;i<enemiesToDamage.Length;i++){
        //                     ITakeDamage takeDamage = enemiesToDamage[i].GetComponent<ITakeDamage>();
        //                     if(takeDamage != null)
        //                         takeDamage.TakeDamage(enemyStats.damage);
        //                 }
        //     enemyStats.attackSpeedTimer = enemyStats.attackSpeed;
        //     return true;
        // }
        // else 
            return false;
    }

    [Task]
    public bool Flip(){
        // facingRigth = !facingRigth;
        // if(facingRigth)
        //     transform.rotation = Quaternion.Euler(0,0,0);
        // else 
        //     transform.rotation = Quaternion.Euler(0,180,0);
        return true;
    }

    [Task]
    public bool IsHealthLessThanPrevious(){
        // if(enemyStats.currentHealth < previousHealth){
        //     enemyStats.dazedTime = enemyStats.StartDazedTime;
        //     previousHealth = enemyStats.currentHealth;
        //     return true;
        // }
        // else 
            return false;
    }

    [Task]
    public bool ChangeColor1(){
        // float colored, percentOfHealth;
        // rb.velocity = Vector2.zero;
        // percentOfHealth = (enemyStats.currentHealth * 100) / enemyStats.maxHealth;
        // colored = 1 - (percentOfHealth / 100);
        // foreach (SpriteRenderer element in lifeRender){
        //     switch (unitTypeOfColor){
        //             case TypeOfColor.red:
        //                 element.color = new Color(element.color.r, colored, colored);
        //                 break;
        //             case TypeOfColor.green:
        //                 element.color = new Color(colored, element.color.g, colored);
        //                 break;
        //             case TypeOfColor.blue:
        //                 element.color = new Color(colored, colored, element.color.b);
        //                 break;
        //         }
        // }
        return true;
    }

    [Task]
    public bool IsNoHealth(){
        // if(enemyStats.currentHealth <= 0)
        //     return true;
        // else 
            return false;
    }

    [Task]
    public bool Dead(){
        // camRipple.RippleEffect();
        // Instantiate(corpse,transform.position, Quaternion.identity);
        // Instantiate(bloodSplash, transform.position, Quaternion.identity);
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        // Destroy(gameObject);

        return true;
    }

   
    #endregion
}