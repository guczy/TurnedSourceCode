using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public Slider healthSlider;  // Reference to the health slider
    public AudioSource healSound;  // Reference to the AudioSource for healing sound
    public Text storedHealsText;  // Reference to the Text component for displaying stored heals

    private int storedHeals = 0;  // Variable to store the number of heal items in inventory

    void Start()
    {
        currentHealth = maxHealth;
        healthSlider.maxValue = maxHealth;  // Set the maximum value of the slider
        healthSlider.value = currentHealth;  // Set the current value of the slider

        // Ensure the AudioSource is assigned
        if (healSound == null)
        {
            healSound = GetComponent<AudioSource>();
        }

        // Initialize the stored heals text
        UpdateStoredHealsText();
    }

    void Update()
    {
        // Check if the player presses the "H" key to use a stored heal
        if (Input.GetKeyDown(KeyCode.H) && storedHeals > 0)
        {
            UseStoredHeal();
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        healthSlider.value = currentHealth;  // Update the slider value

        if (currentHealth <= 0)
        {
            SceneManager.LoadSceneAsync(0);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Heal"))
        {
            if (currentHealth < maxHealth)
            {
                HealToFull();  // Heal the player to full health
                PlayHealSound();  // Play the healing sound
            }
            else
            {
                storedHeals++;  // Add heal item to inventory if health is full
                Debug.Log("Heal item added to inventory. Total stored heals: " + storedHeals);
                UpdateStoredHealsText();  // Update the stored heals text
            }
            Destroy(other.gameObject);  // Remove the heal item from the scene
        }
    }

    void HealToFull()
    {
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;  // Update the slider value
    }

    void PlayHealSound()
    {
        if (healSound != null)
        {
            healSound.Play();
        }
        else
        {
            Debug.LogWarning("Heal sound is not assigned.");
        }
    }

    void UseStoredHeal()
    {
        storedHeals--;  // Use one stored heal
        HealToFull();  // Heal the player to full health
        PlayHealSound();  // Play the healing sound
        Debug.Log("Used one stored heal. Remaining stored heals: " + storedHeals);
        UpdateStoredHealsText();  // Update the stored heals text
    }

    void UpdateStoredHealsText()
    {
        if (storedHealsText != null)
        {
            storedHealsText.text = "" + storedHeals;
        }
        else
        {
            Debug.LogWarning("Stored Heals Text is not assigned.");
        }
    }
}
