using UnityEngine;

public class AmmoRefill : MonoBehaviour
{
    public int magazineCount = 1; // Number of magazines to add

    private AudioSource audioSource; // Declare the AudioSource variable

    private void Start()
    {
        // Get the AudioSource component attached to the same GameObject
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Try to get the ShootingAk script attached to the player or its children
            ShootingAk shootingAkScript = other.GetComponentInChildren<ShootingAk>();

            if (shootingAkScript != null)
            {
                // Add magazines to the player's inventory
                shootingAkScript.AddMagazines(magazineCount);
                // Play any attached audio source
                AudioSource audioSource = GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                // Destroy the pickup object
                Destroy(gameObject);
            }
        }
    }
}
