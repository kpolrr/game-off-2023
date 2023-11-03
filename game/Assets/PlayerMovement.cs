// Script created by @davegamedevelopment
// Modified by kpolr & amkiwi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;

    public float groundDrag;

    public float jumpForce;
    public float airMultiplier;
    public float jumpAmount;

    public float jumpCooldown;

    public float playerHeight;
    public LayerMask whatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    public Rigidbody rb;



    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        kbInput();
        SpeedControl();

        if (jumpCooldown > 0)
        {
            jumpCooldown -= Time.deltaTime;
        }

        if(grounded)
        {
            rb.drag = groundDrag;
            jumpAmount = 1;

        } else
        {
            rb.drag = 0;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();

        if(Input.GetKey("space") && jumpAmount > 0) {
            if (jumpCooldown < 0.01)
            {
                Jump();
                jumpAmount -= 1;
                jumpCooldown = 0.3f;
            }
        }
    }

    private void kbInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.y);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }
}
