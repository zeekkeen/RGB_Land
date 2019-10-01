﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    Animator anim;

    void Start()
    {
        anim=GetComponent<Animator>();
    }

void OnTriggerEnter2D(Collider2D other) {
    if (other.CompareTag("Player"))
    {
        anim.SetTrigger("move");
    }
}
}
