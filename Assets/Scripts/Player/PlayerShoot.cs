using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform gun;
    
    Animator animator;
    PlayerStats stats;
    float shootCooldownTimer = 0f;

    bool continuousShoot;
    bool singleShot;

    Camera mainCam;

    void Start()
    {
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        mainCam = Camera.main;
    }
    
    void Update()
    {
        shootCooldownTimer -= Time.deltaTime;

        if ((continuousShoot || singleShot) && shootCooldownTimer <= 0f)
        {
            Shoot();
            shootCooldownTimer = stats.shotDelay;
        }
        singleShot = false;
    }


    void Shoot()
    {
        animator.SetTrigger("isShooting");
        // Calculate direction towards mouse position
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f; // Distance of the plane from the camera
        Vector3 aimDirection = mainCam.ScreenToWorldPoint(mousePosition) - transform.position;
        aimDirection.z = 0;
        aimDirection.Normalize();

        float spreadAngle = 10f;

        for (int i = 0; i < stats.bulletCount; i++)
        {
            float angleOffset = (i - (stats.bulletCount - 1) / 2f) * spreadAngle;

            Vector2 direction = Quaternion.Euler(0, 0, angleOffset) * aimDirection;

            GameObject bullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);

            bullet.transform.right = direction;

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = direction * stats.bulletSpeed;

            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.damage = stats.bulletDamage;
            bulletScript.pierceRemaining = stats.pierceCount;
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
