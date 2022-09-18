using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharCont controller;
    public Rigidbody2D rb2D;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    float verticalMove = 0f; 
    bool jump = false;
    bool dash = false;
    public string horizontalAxis;
    public string verticalAxis;

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw(horizontalAxis) * runSpeed;
        verticalMove = Input.GetAxisRaw(verticalAxis); 

        if (Input.GetButtonDown("Jump") && (controller.isGrounded = true))
        {
            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }
            else if (!Input.GetButtonDown("Jump"))
            {
                jump = false; 
            }
        }
    }

    private void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.deltaTime, dash, jump);
        jump = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" && controller.isGrounded == false)
        {
            jump = false; 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" && controller.isGrounded == true)
        {
            jump = true; 
        }
    }
}
