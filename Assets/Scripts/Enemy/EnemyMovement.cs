using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float enemySpeed = 2f;
    [SerializeField] float rotationSpeed;
    [SerializeField] float awareDistance;
    [SerializeField] float damage;

    Rigidbody2D rb;
    Animator animator;
    SpriteRenderer spriteRenderer;
    [SerializeField] GameObject enemyHealthBarUIPrefab;

    bool isAware;
    Transform player;
    Vector2 targetDirection;
    Vector2 directionToPlayer;
    float changeDirectionTime;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        targetDirection = transform.up;
        GameObject healthBar = Instantiate(enemyHealthBarUIPrefab, transform);
        healthBar.transform.localPosition = Vector3.up * 1.0f;
        EnemyHealthBarUI healthBarScript = healthBar.GetComponentInChildren<EnemyHealthBarUI>();
        healthBarScript.enemy = transform;
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        CheckAwareness();
    }

    void FixedUpdate()
    {
        UpdateEnemyDirection();
        //RotateEnemy();
        MoveEnemy();
    }

    void CheckAwareness()
    {
        if (Vector2.Distance(transform.position, player.position) <= awareDistance)
        {
            isAware = true;
        }
        else
        {
            isAware = false;
        }
    }

    void UpdateEnemyDirection()
    {
        RandomDirection();
        PlayerDirection();
    }

    void PlayerDirection()
    {
        if (isAware)
        {
            targetDirection = (player.position - transform.position).normalized;
        }
    }

    void RandomDirection()
    {
        changeDirectionTime -= Time.deltaTime;

        if (changeDirectionTime <= 0)
        {
            targetDirection = Random.insideUnitCircle.normalized;
            changeDirectionTime = Random.Range(1f, 4f);
        }
    }

    void RotateEnemy()
    {
        Quaternion targetRotation = Quaternion.LookRotation(transform.forward, targetDirection);
        Quaternion rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        rb.SetRotation(rotation);
    }

    void MoveEnemy()
    {
        rb.velocity = targetDirection * enemySpeed;

        if (rb.velocity.magnitude > 0)
        {
            animator.SetBool("isMoving", true);
            spriteRenderer.flipX = rb.velocity.x < 0;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }    
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerMovement>())
        {
            var health = collision.gameObject.GetComponent<Health>();
            health.ReduceHealth(damage);
        }
    }
}
