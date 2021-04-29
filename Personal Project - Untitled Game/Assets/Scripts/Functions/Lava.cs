﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lava : MonoBehaviour
{

    public PlayerHealthController playerHealth;
    private Rigidbody2D playerRb;

    private float force = 15f;
    private float lavaDamage = 10f;
    private int damagePerTouch = 5;

    void Awake()
    {
        playerRb = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update() 
    {

        Debug.Log("\nLava Damage: " + lavaDamage);

        if(LavaDamage() != null)
        {
            lavaDamage += Time.deltaTime;
        }
            
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(LavaDamage());
            playerRb.AddForce(Vector2.up * force, ForceMode2D.Impulse);
        }
        else 
        {
            Destroy(other.gameObject);
        }
        
    }

    IEnumerator LavaDamage()
    {
        lavaDamage = 10;

        for (int i = 0; i < damagePerTouch; i++)
        {
            playerHealth.TakeDamage(lavaDamage);
            yield return new WaitForSeconds(0.4f);
        }

        yield return null;
    }
}
