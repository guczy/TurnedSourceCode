using UnityEngine;

public class UIFollowPlayer : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's transform
    public Vector3 offset; // Offset from the player's position

    void Update()
    {
        // Check if the player's transform is assigned
        if (playerTransform != null)
        {
            // Calculate the target position for the UI element
            Vector3 targetPosition = playerTransform.position + offset;

            // Set the position of the UI element to the target position
            transform.position = targetPosition;
        }
    }
}
