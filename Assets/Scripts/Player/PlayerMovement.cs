using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    Animator animator;

    Vector2 moveInput;

    [SerializeField] float moveSpeed = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MovePlayer();
    }

    void OnMove(InputValue inputValue)
    {
        moveInput = inputValue.Get<Vector2>();
    }

    void MovePlayer()
    {
        Vector2 moveDirection = new Vector2(moveInput.x, moveInput.y).normalized;
        rb.velocity = moveDirection * moveSpeed;

        if(moveDirection.x != 0 || moveDirection.y != 0)
        {
            animator.SetBool("isMoving", true);
            spriteRenderer.flipX = moveDirection.x < 0;
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }
}
