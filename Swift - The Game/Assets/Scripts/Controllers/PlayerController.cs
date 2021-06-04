﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Components variables")]
    private Rigidbody2D rb;
    public LayerMask groundMask;

    [Header("Inputs")]
    private float horizontalInput;
    
    [Header("Physics oriented or forces")]
    [Range(-50, 50)][SerializeField] private float moveSpeed = 5f;
    [Range(-100, 100)][SerializeField] private float torqueForce = 35f;
    [SerializeField] private float gravityModifier = 2f;

    [Header("Jump variables")]
    [Range(-50, 50)][SerializeField] private float jumpForce = 10f;
    [SerializeField] private int extraJumpsValue = 1;
    [SerializeField] private int extraJumps = 1;
    public int ExtraJumps {get {return extraJumps;} set {ExtraJumps = value;}}
  
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
 
    private void Update()
    {
        //Sets gravityScale to gravityModifier
        rb.gravityScale = gravityModifier;

        //Basic movement of a player 
        horizontalInput = Input.GetAxisRaw("Horizontal");
        rb.AddForce(Vector2.right * horizontalInput * moveSpeed);

        if(IsGrounded())
        {
            extraJumps = extraJumpsValue;
        }    

        //This if statement is for double jump
        if(Input.GetKeyDown(KeyCode.Space) && extraJumps > 0)
        {
            //Adds up force and torque when Space is clicked
            //When you double jump then "extraJumps" variable will subtract 1 from the value of this variable
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.AddTorque(torqueForce);
            extraJumps--;
        }
        //This if statement is for normal jump
        else if(Input.GetKeyDown(KeyCode.Space) && IsGrounded() && extraJumps == 0)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            rb.AddTorque(torqueForce);
        }
    }

    //This bool determines whenever player is on the ground or not using raycast
    private bool IsGrounded()
    {
        //Parameters for raycast
        Vector2 position = transform.position;
        var direction = Vector2.down;
        const float distance = 1f;

        var hit = Physics2D.Raycast(position, direction, distance, groundMask);

        //if raycast hits anything in groundMask
        if(hit.collider != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
