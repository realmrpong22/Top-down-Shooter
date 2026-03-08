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

    private void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.ReduceHealth(damage);
            Destroy(gameObject);
        }
        if (collision.tag == "Wall")
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.GetMask("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
