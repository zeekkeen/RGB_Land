using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoAzul : MonoBehaviour, ITakeDamage
{

    public float currentHealth;

    public float maxHealth;

    public float moveSpeed;

    public bool dir = true;

    public List <SpriteRenderer> lifeRender = new List<SpriteRenderer>{};

    public TypeOfColor unitTypeOfColor;

    public Rigidbody2D rb;

    public Animator anim;

    public GameObject bloodEffect;

    public GameObject player;

    short cont = 0;
    // Start is called before the first frame update
    void Start()
    {
        player = null;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, (dir ? Vector3.right : Vector3.left) * 5f,Color.red);
    }

    private void FixedUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, (dir ? Vector2.right : Vector2.left),5f,LayerMask.GetMask("Player"));
        if (hit.collider != null &&  player==null)
        {
            player = hit.collider.gameObject;
            anim.SetTrigger("Atacar");
        }
    }


    public void Mover()
    {
        rb.velocity = (dir? Vector2.right: Vector2.left) * moveSpeed;
        cont++;
        if (cont>4)
        {
            cont = 0;
            dir = !dir;
            transform.localScale = new Vector3(-transform.localScale.x,1,1);
            ChangeWalk(0);
            rb.velocity = Vector2.zero;
        }
    }

    public void ChangeWalk(int p)
    {
        rb.velocity = Vector2.zero;
        anim.SetBool("Walk", p==1);
    }


    public void ChangeColor1(){
        float colored, percentOfHealth;
        rb.velocity = Vector2.zero;
        percentOfHealth = (currentHealth * 100) / maxHealth;
        colored = 1 - (percentOfHealth / 100);
        foreach (SpriteRenderer element in lifeRender){
            switch (unitTypeOfColor){
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


    public void Atacar()
    {
        rb.AddForce((dir ? Vector2.right : Vector2.left) * moveSpeed/2);
        player = null;
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        currentHealth -= damage;
        anim.SetBool("Morir", currentHealth <= 0);
        ChangeColor1();
    }
}
