using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : PlayerSystems
{
    // Health systems
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private int curHealth = 0;
    [SerializeField] private Vector2 spawnPoint;

    private void Start()
    {
        curHealth = maxHealth;
    }

    private void Update()
    {
        HealthConditions();
    }

    private void HealthConditions() //when player health reached 0 reset the game
    {
        if (curHealth <= 0)
        {
            curHealth = maxHealth;
            transform.position = spawnPoint;
        }
    }

    public void takeDamage (int damage) //To be called to make the player to take damage
    {
        curHealth -= damage;
    }

    public int Health(int health)
    {
        health = curHealth;
        return health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
