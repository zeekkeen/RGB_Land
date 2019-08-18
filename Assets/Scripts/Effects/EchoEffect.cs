using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    float timeBtwSpawns;
    public float startTimeBtwSpawns=0.05f;
    public GameObject echo;

    void Update()
    {
        if(timeBtwSpawns<=0)
        {
            GameObject instance= (GameObject)Instantiate(echo,transform.position,transform.rotation);
            if(transform.localScale.x<0)instance.transform.localScale=new Vector3(-1*instance.transform.localScale.x,instance.transform.localScale.y,instance.transform.localScale.z);
            Destroy(instance,1f);
            timeBtwSpawns=startTimeBtwSpawns;
        }    
        else
        {
            timeBtwSpawns-=Time.deltaTime;
        }
    }
}
