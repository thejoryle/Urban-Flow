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
    public bool isGrounded;
    public float velX;
    public float velY;
    public float velZ;

    // vars to enable randomized jump animation
    private enum JumpVersion
    {
        jump0, jump1, jump2, none
    }
    private JumpVersion jumpVersion;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Debug.Log(isGrounded);
        velX = rb.velocity.x;
        velY = rb.velocity.y;
        velZ = rb.velocity.z;
    }

    private void FixedUpdate()
    {
        if (!isGrounded)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, moveSpeed);

            if (rb.velocity.y < 0)
                rb.AddForce(new Vector3(0, -0.2f, 0), ForceMode.Impulse);
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
            Debug.Log("Jumping");
            isGrounded = false;
            transform.position += new Vector3(0, .1f, 0);
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            animator.SetBool("isGrounded", false);
            RandomizeJumpAnimation();
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

    void RandomizeJumpAnimation()
    {
        int choice = Random.Range(0, 3);
        jumpVersion = (JumpVersion) choice;

        switch (choice)
        {
            case 0:
                animator.SetBool("isJumping0", true);
                break;
            case 1:
                animator.SetBool("isJumping1", true);
                break;
            case 2:
                animator.SetBool("isJumping2", true);
                break;
        }
    }

    void EndJumpAnimation()
    {
        switch ((int)jumpVersion)
        {
            case 0:
                animator.SetBool("isJumping0", false);
                break;
            case 1:
                animator.SetBool("isJumping1", false);
                break;
            case 2:
                animator.SetBool("isJumping2", false);
                break;
        }
        // set jump version to "none"
        jumpVersion = (JumpVersion) 3;
    }
}
