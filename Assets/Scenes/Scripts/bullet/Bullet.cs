using UnityEngine;

public class Bullet : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Debug log to see which object the bullet hit
        Debug.Log("Bullet hit something: " + other.gameObject.name);

        // Check if the object has the "Enemy" tag
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit zombie!");

            // Get the EnemyHealth component
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            if (enemyHealth != null)
            {
                // Register the hit on the enemy
                enemyHealth.RegisterHit();
            }
            else
            {
                Debug.LogError("Enemy object does not have an EnemyHealth component!");
            }

            // Destroy the bullet immediately when it hits the enemy
            Destroy(gameObject);
        }
    }
}
