using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Animator animator;

    [Header("Movement Vars")]
    public float moveSpeed;
    public float jumpForce;
    public float extraGravity;
    public bool isGrounded;

    // vars to enable randomized jump animation
    private enum JumpVersion
    {
        jump0, jump1, jump2, none
    }
    private JumpVersion jumpVersion;
    private float jumpForceModifier; // used to equalize elevation of each jump animation


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, moveSpeed);

            if (rb.velocity.y < 0)
                rb.AddForce(new Vector3(0, extraGravity, 0), ForceMode.Impulse);
        }
        else
        {
            rb.velocity = new Vector3(0, 0, moveSpeed);
        }
    }

    void OnJump()
    {
        // Only allow jumps if we are not in the air
        if (isGrounded)
        {
            isGrounded = false;
            transform.position += new Vector3(0, .1f, 0); // gets player unstuck from ground
            animator.SetBool("isGrounded", false);
            RandomizeJumpAnimation();
            rb.AddForce(new Vector3(0, jumpForce * jumpForceModifier, 0), ForceMode.Impulse);           
        }
    }

    void OnCollisionStay()
    {
        if(rb.velocity.y <= 0)
        {
            isGrounded = true;
            animator.SetBool("isGrounded", true);
            EndJumpAnimation();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = .01f;
        GameObject.Find("Game Over Screen").GetComponent<Canvas>().enabled = true;
    }

    void RandomizeJumpAnimation()
    {
        int choice = Random.Range(0, 3);
        jumpVersion = (JumpVersion) choice;

        switch (choice)
        {
            case 0:
                // straight leg flip
                animator.SetBool("isJumping0", true);
                jumpForceModifier = 1.1f;
                break;
            case 1:
                // hurdle jump
                animator.SetBool("isJumping1", true);
                jumpForceModifier = 1.1f;
                break;
            case 2:
                // twist flip
                animator.SetBool("isJumping2", true);
                jumpForceModifier = 0.75f;
                break;
        }
    }

    void EndJumpAnimation()
    {
        switch ((int)jumpVersion)
        {
            case 0:
                // straight leg flip
                animator.SetBool("isJumping0", false);
                break;
            case 1:
                // hurdle jump
                animator.SetBool("isJumping1", false);
                break;
            case 2:
                // twist flip
                animator.SetBool("isJumping2", false);
                break;
        }
        // set jump version to "none"
        jumpVersion = (JumpVersion) 3;
        jumpForceModifier = 1;
    }
}
