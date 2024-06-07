using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 200f;
    public float fireRate = 2f; // Number of shots per second

    [Header("Audio")]
    public AudioSource src;
    public AudioClip pistol;
    public AudioClip reloadSound;

    [Header("UI")]
    public Text ammoText;

    private bool canShoot = true;
    private bool isReloading = false;

    private int bulletsFired = 0;
    private const int maxBullets = 12;
    private int magazines = 1;

    private float nextFireTime = 0f;

    void Start()
    {
        // Ensure player with Glock tag is correctly set up in the scene
        GameObject playerWithGlockModel = GameObject.FindGameObjectWithTag("Player Glock");

        if (playerWithGlockModel == null)
        {
            Debug.LogError("Player Glock GameObject not found!");
            Debug.LogError("Player Glock GameObject not found!");
        }
    }

    void Update()
    {
        if (PauseMenu.isPaused)
        {
            return;
        }

        UpdateAmmoText();

        if (Time.time >= nextFireTime && canShoot && !isReloading && Input.GetButton("Fire1"))
        {
            if (bulletsFired < maxBullets)
            {
                Shoot();
                src.clip = pistol;
                src.Play();
                Debug.Log("Shooting! Bullets fired: " + bulletsFired);
                nextFireTime = Time.time + 1f / fireRate; // Set the next fire time
            }
            else
            {
                Debug.Log("Out of bullets!");
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    void Shoot()
    {
        Debug.Log("Shooting!");

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 bulletVelocity = firePoint.right * bulletSpeed;
            rb.velocity = bulletVelocity;
        }

        Debug.Log("Bullet Fired");
        bulletsFired++;

        if (bulletsFired >= maxBullets)
        {
            canShoot = false;
        }
    }

    void Reload()
    {
        if (magazines > 0 && !isReloading)
        {
            isReloading = true;
            canShoot = false;
            bulletsFired = 0;
            magazines--;
            src.clip = reloadSound;
            src.Play();
            Debug.Log("Reloading! Magazines left: " + magazines);

            Invoke("FinishReloading", 2f);
        }
        else if (magazines <= 0)
        {
            Debug.Log("No magazines left!");
        }
    }

    void FinishReloading()
    {
        isReloading = false;
        canShoot = true;
        Debug.Log("Finished reloading!");
    }

    void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            int remainingBullets = maxBullets - bulletsFired;
            ammoText.text = remainingBullets + "/" + (maxBullets * magazines);
        }
    }

    public void AddMagazines(int amount)
    {
        magazines += amount;
        Debug.Log("Magazines added: " + amount + ". Total magazines: " + magazines);
        UpdateAmmoText();
    }
}
