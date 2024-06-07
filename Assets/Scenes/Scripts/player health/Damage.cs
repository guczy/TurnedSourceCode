using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount = 50;
    public float damageRadius = 1.0f; // Radius within which damage is dealt
    public float damageInterval = 1.0f; // Time interval between damage
    private GameObject player;
    private Health playerHealth;
    public AudioSource painSound;
    private float nextDamageTime = 0.0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerHealth = player.GetComponent<Health>();
        }
    }

    void Update()
    {
        if (player != null && Time.time >= nextDamageTime)
        {
            // Check if player is within the damage radius
            if (Vector2.Distance(transform.position, player.transform.position) <= damageRadius)
            {
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(damageAmount);
                    painSound.Play();   
                    nextDamageTime = Time.time + damageInterval;
                }
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw the damage radius in the editor for debugging purposes
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRadius);
    }
}