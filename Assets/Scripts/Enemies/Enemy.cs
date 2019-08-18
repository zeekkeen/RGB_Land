using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (BoxCollider2D))]
public class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    float dazedTime;
    public float StartDazedTime = 0.6f;
    public GameObject bloodEffect,deathEffect;
    public GameObject bloodSplash,corpse;
    Animator anim;
    RipplePostProcessor camRipple;
    void Start()
    {
        anim=GetComponent<Animator>();
        anim.SetBool("isRunning",true);
        camRipple=Camera.main.GetComponent<RipplePostProcessor>();
    }

    void Update()
    {
        if(dazedTime<=0)speed=5;
        else
        {
            speed=0;
            dazedTime-=Time.deltaTime;
        }
        if(health<=0)
        {
            camRipple.RippleEffect();
            Instantiate(corpse,transform.position,Quaternion.identity);
            Instantiate(bloodSplash,transform.position,Quaternion.identity);
            Instantiate(deathEffect,transform.position,Quaternion.identity);
            Destroy(gameObject);
        }
        //transform.Translate(Vector2.left*speed*Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        Instantiate(bloodEffect,transform.position,Quaternion.identity);
        dazedTime=StartDazedTime;
        health-=damage;
        Debug.Log("damague taken!!");
    }
}
