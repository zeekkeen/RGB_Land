using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBoss : MonoBehaviour
{
    bool first=false;
    public List<Animator> animators;
    public List<GameObject> walls;
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag=="Player")
        {
            if(!first)
            {
                first=true;
                foreach (GameObject g in walls)
                {
                    g.SetActive(true);
                }
                foreach (Animator a in animators)
                {
                    a.enabled=true;
                }
                //Acomodar Camara
            }
            
        }    
    }
    public void DestroyWalls()
    {
        foreach (GameObject g in walls)
        {
            g.SetActive(false);
        }
    }
}
