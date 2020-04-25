using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABlueBoss : MonoBehaviour, ITakeDamage
{
    [Header("Valores de Enemigo")]
    public EnemyStats_SO statsTemplate;
    public EnemyStats_SO enemyStats;
    public List<SpriteRenderer> lifeRender;
    public BlueBossManager manager;
    [Header("Proyectil")]
    public GameObject proyectilPrefab;
    public Transform proyectilSpawn;
    [Header("Valores de Ataque")]
    public float tiempoEspera;
    public int contadorAtaque;
    Animator anim;
    List<List<int>> patron= new List<List<int>>(){new List<int>(){1,2},new List<int>{4},new List<int>{6,7}};
    public bool canHit=true;
    public int countHit=2;


    public GameObject portal;
    void Start()
    {
        enemyStats = Instantiate(statsTemplate);
        enemyStats.maxHealth = enemyStats.currentHealth;
        anim=GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public void Voltear()
    {
        transform.localScale=new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
    }
    public void CanHit(int i)
    {
        canHit=i==1;
    }
    public void Ataque2()
    {
        manager.CreatePatron(patron[Random.Range(0,patron.Count-1)]);
        anim.SetInteger("CountAttack",anim.GetInteger("CountAttack")-1);
    }
    public void Ataque1()
    {
        Instantiate(proyectilPrefab,proyectilSpawn.position,proyectilSpawn.rotation);
        anim.SetInteger("CountAttack",anim.GetInteger("CountAttack")-1);
    }

    public void Spawnplataforms()
    {
        manager.SpawnPlataforms((transform.localScale.x>0?1:-1));
    }
    public void ChangeDir(int value)
    {
        switch (value)
        {
            case 0:
            transform.localScale=new Vector3(-Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            break;
            case 1:
            transform.localScale=new Vector3(Mathf.Abs(transform.localScale.x)*(Random.Range(-1,1)==-1?1:-1),transform.localScale.y,transform.localScale.z);
            break;
            case 2:
            transform.localScale=new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
            break;
        }
    }
    public void ChangeColor1(){
        float colored, percentOfHealth;
        percentOfHealth = (enemyStats.currentHealth*100) / enemyStats.maxHealth;
        colored = 1 - (percentOfHealth / 100);
        foreach (SpriteRenderer element in lifeRender){
            switch (enemyStats.typeOfColor)
            {
                case TypeOfColor.red:
                    element.color = new Color(element.color.r, colored, colored);
                    break;
                case TypeOfColor.green:
                    element.color = new Color(colored, element.color.g, colored);
                    break;
                case TypeOfColor.blue:
                    element.color = new Color(colored, 178+(77*colored), element.color.b);
                    break;
            }
        }
    }
    void Dead()
    {
        anim.SetBool("Dead",true);
        GameManager.instance.AddAchievement(Achievement.RangeAttack3);
        GameObject.FindObjectOfType<SpawnBoss>().DestroyWalls();
        portal.SetActive(true);
        Destroy(manager.gameObject,2f);
    }
    public void UntimoAtaque()
    {
        if(countHit==0)
            Instantiate(proyectilPrefab,proyectilSpawn.position,proyectilSpawn.rotation);
    }

    public void TakeDamage(int damage)
    {
        if(canHit)
        {
            enemyStats.currentHealth -= damage;
            if(countHit>0&&(enemyStats.currentHealth*100)/enemyStats.maxHealth<33.3*countHit)
            {
                canHit=false;
                countHit--;
                contadorAtaque++;
                tiempoEspera--;
                anim.SetTrigger("Desaparecer");
            }
            ChangeColor1();
            //animacion de recibir ataque
            Debug.Log("damague taken!!");
            if(enemyStats.currentHealth <= 0){
                canHit=false;
                Dead();
            }
            //torsoAnim.SetTrigger("Morir");
        }
        
    }
}
