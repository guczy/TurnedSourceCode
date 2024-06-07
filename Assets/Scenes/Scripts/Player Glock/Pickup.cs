using UnityEngine;

public class PlayerGlockPickup : MonoBehaviour
{
    public GameObject player_glock; // The player's model with the Glock
    public KeyCode pickupKey = KeyCode.E; // Key to pick up the Glock

    private bool isNearGlock = false; // Flag to check if the player is near the Glock
    private GameObject glockModel; // Reference to the Glock model in the scene

    void Update()
    {
        // Check if the player is near the Glock and presses the pickup key
        if (isNearGlock && Input.GetKeyDown(pickupKey))
        {
            PickUpGlock();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player enters the trigger zone of the Glock
        if (other.CompareTag("Glock"))
        {
            isNearGlock = true;
            glockModel = other.gameObject; // Cache the Glock model reference
            Debug.Log("Player is near the Glock.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player exits the trigger zone of the Glock
        if (other.CompareTag("Glock"))
        {
            isNearGlock = false;
            glockModel = null; // Clear the cached Glock model reference
            Debug.Log("Player is no longer near the Glock.");
        }
    }

    void PickUpGlock()
    {
        // Ensure the playerWithGlockModel is assigned
        if (player_glock == null)
        {
            Debug.LogError("Player with Glock model is not assigned in the Inspector.");
            return;
        }

        // Activate the player model with the Glock
        player_glock.SetActive(true);
        Debug.Log("Player picked up the Glock.");

        // Remove the Glock model from the scene
        Destroy(glockModel);
        Debug.Log("Glock model removed from the scene.");
    }
}
