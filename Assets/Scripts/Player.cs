﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public float gravity = -10f;
    public float speed = 10f;
    private CharacterController characterController;
    private float inputH;
    private float inputV;
    public bool isRunning;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("jump", true);
        }
        else
        {
            animator.SetBool("jump", false);
        }
        inputH = Input.GetAxis("Horizontal");
        inputV = Input.GetAxis("Vertical");

       // animator.SetFloat("inputH", inputH);
        animator.SetFloat("inputV", inputV);


        float moveX = inputH * speed;
        float moveZ = inputV * speed;
        if (moveZ >= 0f || moveZ <= 0f )
        {
            moveX = 0f;
        }

        //if()

        Vector3 movement = new Vector3(moveX, 0f, moveZ);
        movement = Vector3.ClampMagnitude(movement, speed);
        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);
        movement.y = gravity;
        characterController.Move(movement);

        if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            characterController.GetComponent<AudioSource>().Play();
        }
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            characterController.GetComponent<AudioSource>().Stop();
        }
    }
}
