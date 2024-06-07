using UnityEngine;

public class PlayerPunch : MonoBehaviour
{
    private Animator animator;  // Reference to the Animator component
    public bool isPunching = false;  // Flag to check if the player is currently punching

    void Start()
    {
        animator = GetComponent<Animator>();  // Get the Animator component attached to the player
        if (animator == null)
        {
            Debug.LogError("Animator component not found!");
        }
    }

    void Update()
    {
        // Check if the left mouse button is clicked and the player is not already punching
        if (Input.GetMouseButtonDown(0) && !isPunching)
        {
            Debug.Log("Punch initiated");
            Punch();  // Call the Punch method
            
        }
        EndPunch();
    }

    void Punch()
    {
        isPunching = true;  // Set the punching flag to true
        animator.SetTrigger("Punch");  // Trigger the punch animation
        Debug.Log("Punch trigger set");
    }

    // This method is called as an Animation Event at the end of the punch animation
    public void EndPunch()
    {
        isPunching = false;  // Reset the punching flag
        Debug.Log("Punch ended");
    }
}
