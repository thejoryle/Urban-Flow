using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float moveSpeed;
    public float jumpForce;
    public float gravity;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // rb.velocity = new Vector3(0, gravity, moveSpeed);
        rb.velocity = new Vector3(0, rb.velocity.y, moveSpeed);
    }

    void OnJump()
    {
        // Only allow jumps if we are not in the air
        if (rb.velocity.y == 0)
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }
}
