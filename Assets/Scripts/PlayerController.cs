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
        // TODO only allow jump if play is on the floor
        rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
    }
}
