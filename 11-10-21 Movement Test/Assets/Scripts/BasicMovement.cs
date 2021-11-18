using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    public Animator animator;

    public float moveSpeed = 5f;

    public Rigidbody rb;

    Vector3 movement;
    Vector3 localPositionOffset;

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z= Input.GetAxisRaw("Vertical");


        if (movement != Vector3.zero)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.z);
        }

        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void FixedUpdate()
    {

        localPositionOffset = movement * moveSpeed * Time.fixedDeltaTime;
        //Vector3 newPosition = rb.position + transform.TransformDirection(localPositionOffset);
        //rb.MovePosition(newPosition);
        localPositionOffset.z *= -1;

       rb.MovePosition(rb.position + transform.TransformVector( localPositionOffset) );
    }
}
