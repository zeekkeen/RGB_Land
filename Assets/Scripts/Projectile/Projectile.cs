using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed=5f;
    public float lifeTime;
    public float distance;
    public LayerMask whatIsSolid;
    public int damage=10;
    public GameObject destroyEffect;

    private void Start() {
        Invoke("DestroyProjectil",lifeTime);
    }
    void Update()
    {
        RaycastHit2D hitInfo=Physics2D.Raycast(transform.position,transform.forward,distance,whatIsSolid);
        if (hitInfo.collider!=null)
        {
            if (hitInfo.collider.CompareTag("Enemy"))
            {
                hitInfo.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
            DestroyProjectil();
        }
        if(transform.localScale.x>0)transform.Translate(speed*Time.deltaTime,0,0);
        else transform.Translate(-speed*Time.deltaTime,0,0);
    }

    void DestroyProjectil()
    {
        Instantiate(destroyEffect,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    
}
