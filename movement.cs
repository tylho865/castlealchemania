using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // jump info
    public float speed = 5.0f;
    public float jumpForce = 20.0f;
    public float chargedJumpForce = 5.0f;
    public float maxJumpTime = 0.5f;
    public float wallJumpForce = 10.0f;

    // store the rigidbody2D component
    private Rigidbody2D rb;

    // jump variables
    private bool isJumping = false;
    private float jumpTimeCounter;

    // Platform and wall stuff
    private bool isTouchingPlatform = false;
    private bool isTouchingWall = false;

    private float previousMoveHorizontal = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        }
        if (!isJumping && isTouchingWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, -4.0f);
        }
        if (moveHorizontal != 0)
        {
            if (moveHorizontal > 0)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (moveHorizontal < 0)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            previousMoveHorizontal = moveHorizontal;
        }
        // jump from platform
        if (isTouchingPlatform && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        // jump from wall
        if (isTouchingWall && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            jumpTimeCounter = maxJumpTime;
            float wallJumpDirection = isTouchingWall ? -Mathf.Sign(transform.localScale.x) : 0;
            rb.velocity = new Vector2(wallJumpDirection * wallJumpForce, jumpForce + 40);
        }

        // continue while holding jump key
        if (isJumping && Input.GetKey(KeyCode.Space) && !isTouchingWall)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, chargedJumpForce + 5);
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        // stop jump when jump key is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isTouchingPlatform = true;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            isTouchingPlatform = false;
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            isTouchingWall = false;
        }
    }
}
