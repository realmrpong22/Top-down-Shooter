using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float damage = 10f;

    void Start()
    {
        Destroy(gameObject, 3f);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Health health = collision.GetComponent<Health>();

            if (health != null)
            {
                health.ReduceHealth(damage);
            }

            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}