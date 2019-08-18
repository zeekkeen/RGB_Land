using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Corpse : MonoBehaviour
{
    public GameObject effect;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")) Instantiate(effect,transform.position,Quaternion.identity);
    }
}
