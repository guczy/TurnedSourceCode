using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public bool moving = false;
    private float speed = 10.0f;
    private Animator anim;
    private AudioSource walkingSound;

    [Header("Stamina Settings")]
    public float maxStamina = 100f;
    public float currentStamina;
    public float staminaDrainRate = 20f;  // Stamina drained per second while sprinting
    public float staminaRegenRate = 10f;  // Stamina regenerated per second while not sprinting
    public Slider staminaSlider;  // Reference to the stamina slider

    void Start()
    {
        // Initialize stamina
        currentStamina = maxStamina;

        // Set up the stamina slider
        if (staminaSlider != null)
        {
            staminaSlider.maxValue = maxStamina;
            staminaSlider.value = currentStamina;
        }

        // Find the Animator component in any child object
        anim = GetComponentInChildren<Animator>();
        if (anim == null)
        {
            Debug.LogError("No Animator component found on the player or its children.");
        }

        // Find the AudioSource component on the player GameObject or its children
        walkingSound = GetComponentInChildren<AudioSource>();
        if (walkingSound == null)
        {
            Debug.LogError("No AudioSource component found on the player GameObject or its children.");
        }
    }

    void Update()
    {
        movement();
        UpdateStamina();
    }

    void movement()
    {
        bool isMoving = false;

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);
            isMoving = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
            isMoving = true;
        }

        if (isMoving)
        {
            if (walkingSound != null && !walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
            if (anim != null)
            {
                anim.SetFloat("speed", 0.2f);
            }
        }
        else
        {
            if (walkingSound != null && walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
            if (anim != null)
            {
                anim.SetFloat("speed", 0f);
            }
        }

        if (Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
        {
            speed = 15.0f;
            currentStamina -= staminaDrainRate * Time.deltaTime;
            if (currentStamina < 0)
            {
                currentStamina = 0;
            }
        }
        else
        {
            speed = 10.0f;
        }

        moving = isMoving;

        // Update the stamina slider
        if (staminaSlider != null)
        {
            staminaSlider.value = currentStamina;
        }
    }

    void UpdateStamina()
    {
        if (!Input.GetKey(KeyCode.LeftShift) && currentStamina < maxStamina)
        {
            currentStamina += staminaRegenRate * Time.deltaTime;
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }

            // Update the stamina slider
            if (staminaSlider != null)
            {
                staminaSlider.value = currentStamina;
            }
        }
    }
}
