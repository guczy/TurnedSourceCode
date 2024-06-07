using UnityEngine;

public class PlayerWeaponPickup : MonoBehaviour
{
    public GameObject glockModel; // The Glock model in the scene
    public GameObject ak47Model; // The AK47 model in the scene
    public WeaponSwitcher weaponSwitcher; // Reference to the WeaponSwitcher script
    public CameraFollowPlayer cameraFollowScript; // Reference to the CameraFollowPlayer script

    [Header("Audio")]
    public AudioSource src;
    public AudioClip weaponPickUp;

    private bool isNearGlock = false; // Flag to check if the player is near the Glock
    private bool isNearAK47 = false; // Flag to check if the player is near the AK47

    void Start()
    {
        // Ensure cameraFollowScript is assigned
        if (cameraFollowScript == null)
        {
            cameraFollowScript = Camera.main.GetComponent<CameraFollowPlayer>();
            if (cameraFollowScript == null)
            {
                Debug.LogError("CameraFollowPlayer script not found on the main camera.");
            }
        }

        // Ensure weaponSwitcher is assigned
        if (weaponSwitcher == null)
        {
            weaponSwitcher = GetComponent<WeaponSwitcher>();
            if (weaponSwitcher == null)
            {
                Debug.LogError("WeaponSwitcher script not found on the player.");
            }
        }
    }

    void Update()
    {
        // Check if the player is near the Glock and presses the 'E' key
        if (isNearGlock && Input.GetKeyDown(KeyCode.E))
        {
            PickUpGlock();
            src.clip = weaponPickUp;
            src.Play();
        }

        // Check if the player is near the AK47 and presses the 'E' key
        if (isNearAK47 && Input.GetKeyDown(KeyCode.E))
        {
            PickUpAK47();
            src.clip = weaponPickUp;
            src.Play();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player enters the trigger zone of the Glock
        if (other.gameObject == glockModel)
        {
            isNearGlock = true;
            Debug.Log("Player is near the Glock.");
        }

        // Check if the player enters the trigger zone of the AK47
        if (other.gameObject == ak47Model)
        {
            isNearAK47 = true;
            Debug.Log("Player is near the AK47.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exits the trigger zone of the Glock
        if (other.gameObject == glockModel)
        {
            isNearGlock = false;
            Debug.Log("Player is no longer near the Glock.");
        }

        // Check if the player exits the trigger zone of the AK47
        if (other.gameObject == ak47Model)
        {
            isNearAK47 = false;
            Debug.Log("Player is no longer near the AK47.");
        }
    }

    void PickUpGlock()
    {
        // Ensure all required components are assigned
        if (glockModel == null || weaponSwitcher == null)
        {
            Debug.LogError("Glock model or WeaponSwitcher is not assigned.");
            return;
        }

        // Switch the weapon to Glock
        weaponSwitcher.EnableWeapon("Glock");
        Debug.Log("Player picked up the Glock.");

        // Remove the Glock model from the scene
        Destroy(glockModel);
        Debug.Log("Glock model removed from the scene.");

        // Reset the flag
        isNearGlock = false;
    }

    void PickUpAK47()
    {
        // Ensure all required components are assigned
        if (ak47Model == null || weaponSwitcher == null)
        {
            Debug.LogError("AK47 model or WeaponSwitcher is not assigned.");
            return;
        }

        // Switch the weapon to AK47
        weaponSwitcher.EnableWeapon("AK47");
        Debug.Log("Player picked up the AK47.");

        // Remove the AK47 model from the scene
        Destroy(ak47Model);
        Debug.Log("AK47 model removed from the scene.");

        // Reset the flag
        isNearAK47 = false;
    }
}
