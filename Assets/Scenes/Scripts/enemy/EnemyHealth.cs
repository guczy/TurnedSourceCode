using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHits = 4; // Number of hits required to deactivate the enemy
    private int currentHits = 0;

    [SerializeField] FloatingHealthBar healthBar;

    // Reference to the material of the enemy
    private Material enemyMaterial;
    private Color originalColor;
    public Color hitColor = Color.red; // Color to turn the enemy when hit
    public float hitDuration = 0.1f; // Duration to keep the hit color

    //loottable
    [Header("Loot")]
    public List<LootItem> lootTable = new List<LootItem>();

    void Start()
    {
        // Get the material of the enemy
        healthBar = GetComponentInChildren<FloatingHealthBar>();
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            enemyMaterial = renderer.material;
            originalColor = enemyMaterial.color;
        }
    }

    public void RegisterHit()
    {
        currentHits++;
        Debug.Log(gameObject.name + " hit " + currentHits + " times.");
        healthBar.UpdateHealthBar(maxHits - currentHits, maxHits);

        if (currentHits >= maxHits)
        {
            Die(); // Call Die method when the enemy is deactivated
        }
        else
        {
            // Start the coroutine to change the enemy color temporarily
            StartCoroutine(FlashColor());
        }
    }

    void Die()
    {
        foreach (LootItem lootItem in lootTable)
        {
            if (Random.Range(0f, 100f) <= lootItem.DropChance)
            {
                InstantiateLoot(lootItem.itemPrefab); // Corrected field name to itemPrefab
                break;
            }
        }

        // Deactivate the enemy
        KillCounter.Instance.AddKill(); // Increment kill count
        Destroy(gameObject);
    }

    void InstantiateLoot(GameObject lootPrefab)
    {
        if (lootPrefab != null)
        {
            GameObject droppedLoot = Instantiate(lootPrefab, transform.position, Quaternion.identity);
        }
    }

    IEnumerator FlashColor()
    {
        // Change the enemy color to hitColor
        enemyMaterial.color = hitColor;

        // Wait for a short duration
        yield return new WaitForSeconds(hitDuration);

        // Change the enemy color back to the original color
        enemyMaterial.color = originalColor;
    }
}
