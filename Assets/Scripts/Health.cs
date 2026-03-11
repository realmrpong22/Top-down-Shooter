using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] float currentHealth;
    [SerializeField] float maxHealth;
    [SerializeField] float iFrameDuration;

    public GameObject healthBar;

    Animator animator;

    SpriteRenderer spriteRenderer;

    Color originalColor;

    public UnityEvent onDeath;
    public bool iFrame {  get; set; }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
        currentHealth = maxHealth;
    }

    public float healthPercentage
    {
        get { return currentHealth / maxHealth; }
    }

    public void ReduceHealth(float health)
    {
        if (currentHealth < 0)
        {
            return;
        }

        if (iFrame)
        {
            return;
        }

        currentHealth -= health;
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        if (currentHealth == 0)
        {
            onDeath?.Invoke();

            animator.SetBool("isMoving", false);
            animator.SetTrigger("isDead");
            if (gameObject.tag == "Player")
            {
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerShoot>().enabled = false;
            }
            else if (gameObject.tag == "Enemy")
            {
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;

                var movement = GetComponent<EnemyMovement>();
                if (movement != null) movement.enabled = false;

                var bigEnemy = GetComponent<BigEnemy>();
                if (bigEnemy != null) bigEnemy.enabled = false;
            }
            GetComponent<CapsuleCollider2D>().enabled = false;

            Destroy(gameObject, 1.0f);
            Destroy(healthBar);

        }
        else
        {
            StartIFrame(iFrameDuration);
        }
    }

    public void AddHealth(float health)
    {
        if (currentHealth == maxHealth)
        {
            return;
        }
        currentHealth += health;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void StartIFrame(float duration)
    {
        StartCoroutine(IFrameCoroutine(duration));
    }

    IEnumerator IFrameCoroutine(float duration)
    {
        iFrame = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(duration);
        iFrame = false;
        spriteRenderer.color = originalColor;
    }
}
