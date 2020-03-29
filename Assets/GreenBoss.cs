using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBoss : MonoBehaviour
{


    Transform centro;
    Animator manoD,manoI,Torso;


    // Start is called before the first frame update
    void Start()
    {
        centro=transform.Find("CentroManos");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
