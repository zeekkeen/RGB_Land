using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    public enum Tipo
    {
        Burbuja,
        Bola
    };
    public Tipo tipo;
    [Range(0,10)]
    public float speed=5;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        if (tipo==Tipo.Burbuja)
        {
            rb=GetComponent<Rigidbody2D>();
            rb.AddForce((GameObject.FindGameObjectWithTag("Player").transform.position-transform.position)*speed,ForceMode2D.Impulse);   
        }
        
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.layer==10)
        {
            Destroy(gameObject);
        }
        if(other.gameObject.tag == "Player"){
            Destroy(gameObject);
            ITakeDamage takeDamage = other.gameObject.GetComponent<ITakeDamage>();
            if(takeDamage != null){
                takeDamage.TakeDamage(1);
                //Vector3 vec = other.transform.position - transform.position;
                //other.gameObject.GetComponent<Rigidbody2D>().AddForce(vec.normalized * force * (-1));
            }
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (tipo==Tipo.Burbuja)
        {
            
        }
    }
}
