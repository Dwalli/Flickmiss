using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    // Health systems
    [SerializeField] private int maxHealth = 6;
    [SerializeField] private int curHealth;
    [SerializeField] private Vector2 spawnPoint;

    private void Awake()
    {
        curHealth = maxHealth;
    }
    private void Update()
    {
      
    }

    public void ResetHealth() 
    {
       curHealth = maxHealth;
    }

    public void takeDamage (int damage) //To be called to make the player to take damage
    {
        curHealth -= damage;
    }

    public int Health() { return curHealth; }

    public Vector2 SpawnPoint() { return spawnPoint; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
