using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Weapon Stats")]
    public float shotDelay = 3f;
    public float bulletDamage = 25f;
    public int bulletCount = 1;
    public int pierceCount = 0;
    public float bulletSpeed = 20f;

    [Header("Player Stats")]
    public float moveSpeedMultiplier = 1f;
}