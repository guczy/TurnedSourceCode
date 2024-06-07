using UnityEngine;

public class GlockPickup : MonoBehaviour
{
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
            Shooting shootingScript = other.GetComponentInChildren<Shooting>();
            if (shootingScript != null)
            {
                shootingScript.AddMagazines(2);
                if (audioSource != null) // Check if AudioSource is not null
                {
                    audioSource.Play();
                }
                Destroy(gameObject);
            }
        }
    }
}
