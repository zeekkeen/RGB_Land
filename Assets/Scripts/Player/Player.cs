﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float jumpHeight=4f;
    public float timeToJumpApex=0.4f;
    float accelerationTimeAirbone=0.2f;
    float acceletarionTimeGrounded=0.1f;
    float moveSpeed = 6f;
    float gravity;
    float jumpVelocity;
    Vector3 velocity;
    float velocityXSmoothing;
    Controller2D controller;

    void Start()
    {
        controller = GetComponent<Controller2D>();
        gravity=-(2*jumpHeight)/Mathf.Pow(timeToJumpApex,2);
        jumpVelocity=Mathf.Abs(gravity)*timeToJumpApex;
        print("Gravity: "+ gravity+" Jump velocity: "+ jumpVelocity);
    }

    void Update()
    {
        if(controller.collisions.above || controller.collisions.below)
        {
            velocity.y=0;
        }
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if(Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            velocity.y=jumpVelocity;
        }
        float targetVelocityX = input.x * moveSpeed;
        velocity.x=Mathf.SmoothDamp(velocity.x,targetVelocityX,ref velocityXSmoothing,(controller.collisions.below)?acceletarionTimeGrounded:accelerationTimeAirbone);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
