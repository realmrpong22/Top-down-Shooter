using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gun;
    [SerializeField] float shotDelay;
    [SerializeField] float bulletSpeed = 20f;
    
    Animator animator;
    float lastShotTime;

    bool continuousShoot;
    bool singleShot;

    Camera mainCam;

    void Start()
    {
        animator = GetComponent<Animator>();
        mainCam = Camera.main;
    }
    
    void Update()
    {
        if (continuousShoot || singleShot)
        {
            float timeSinceLastShot = Time.time - lastShotTime;  
            if (timeSinceLastShot > shotDelay)
            {
                Shoot();
                lastShotTime = Time.time;
                singleShot = false;
            }
        }
    }


    void Shoot()
    {
        animator.SetTrigger("isShooting");
        // Calculate direction towards mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Distance of the plane from the camera
        Vector3 aimDirection = mainCam.ScreenToWorldPoint(mousePosition) - transform.position;
        aimDirection.z = 0; // Ensure the bullet stays in the 2D plane

        // Instantiate bullet prefab
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        // Rotate bullet to face the shooting direction (if bullet sprite needs to face forward)
        bullet.transform.right = aimDirection.normalized;

        // Apply force to the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = aimDirection.normalized * bulletSpeed;
        }
    }

    void OnFire(InputValue inputValue)
    {
        continuousShoot = inputValue.isPressed;

        if (inputValue.isPressed)
        {
            singleShot = true;
        }
    }

}
