using UnityEngine;

public class BigEnemy : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float shootRange = 5f;
    [SerializeField] float shootCooldown = 2f;

    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePoint;

    Transform player;
    Rigidbody2D rb;
    Animator animator;

    float shootTimer;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
    }

    void FixedUpdate()
    {
        float distance = Vector2.Distance(transform.position, player.position);

        if (distance > shootRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.SetBool("isMoving", false);
            TryShoot();
        }
    }

    void MoveTowardsPlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;

        animator.SetBool("isMoving", true);
    }

    void TryShoot()
    {
        if (shootTimer > 0) return;

        animator.SetTrigger("Shoot");

        Vector2 direction = (player.position - transform.position).normalized;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * 6f;

        shootTimer = shootCooldown;
    }
}