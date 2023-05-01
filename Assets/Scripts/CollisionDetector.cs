using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float runSpeed = 10f;
    public float jumpForce = 10f;
    public bool isGrounded;

    private Rigidbody2D rb;
    private bool isCollidingWithWall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float horizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = new Vector2(horizontalMove * runSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontalMove * moveSpeed, rb.velocity.y);
        }

        if (isCollidingWithWall && Mathf.Sign(horizontalMove) == Mathf.Sign(transform.localScale.x))
        {
            rb.velocity = new Vector2(0f, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            // personagem está no chão
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Walls"))
        {
            // define que há colisão com a parede
            isCollidingWithWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            // define que não há mais colisão com a parede
            isCollidingWithWall = false;
        }
    }
}
