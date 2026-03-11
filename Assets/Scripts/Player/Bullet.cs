using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    PlayerMovement player;
    Rigidbody2D rb;
    float xSpeed;

    public float damage = 10f;

    public int pierceRemaining = 0;

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Health health = collision.GetComponent<Health>();

            if (health != null)
            {
                health.ReduceHealth(damage);
            }

            if (pierceRemaining > 0)
            {
                pierceRemaining--;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
