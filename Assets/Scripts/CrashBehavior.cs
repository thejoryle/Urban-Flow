using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class CrashBehavior : MonoBehaviour
{
    PlayerController playerController;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Body Collider")) 
        { 
            playerController = collision.gameObject.GetComponentInParent<PlayerController>();
            playerController.crashed = true;
            StartCoroutine(playerController.Crash());
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Foot Collider"))
        {
            playerController = collision.gameObject.GetComponent<PlayerController>();
            if (playerController.rb.velocity.y <= 0)
            {
                playerController.isGrounded = true;
                playerController.animator.SetBool("isGrounded", true);
                playerController.EndJumpAnimation();
            }
        }
    }
}
