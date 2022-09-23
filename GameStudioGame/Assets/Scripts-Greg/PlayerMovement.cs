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
    private float horizontalDir;
    private float verticalDir;
    //private bool isFaceRight = true; 

    [SerializeField] public float dashSpeed = 15f;
    [SerializeField] public float dashLength = 0.3f;
    [SerializeField] private float dashBufferLength = 0.1f;
    private float dashBufferCounter;
    private bool isDashing;
    private bool hasDashed;
    private bool canDash => dashBufferCounter > 0f && !hasDashed;

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

        if (Input.GetButtonDown("Dash"))
        {
            dashBufferCounter = dashBufferLength; 
        }
        else
        {
            dashBufferCounter -= Time.deltaTime; 
        }
    }

    private void FixedUpdate()
    {
        if (canDash)
        {
            StartCoroutine(Dash(horizontalDir, verticalDir)); 
        }
        if (!isDashing)
        {
            controller.Move(horizontalMove * Time.deltaTime, dash, jump);
            jump = false;
        }
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

    IEnumerator Dash(float x, float y)
    {
        float dashStartTime = Time.time;
        hasDashed = true;
        isDashing = true;
        jump = false;

        rb2D.velocity = Vector2.zero;
        rb2D.gravityScale = 0f;
        rb2D.drag = 0f;

        Vector2 dir; 
        if (x != 0f || y != 0f)
        {
            dir = new Vector2(x, y); 
        }
        else
        {
            if (controller.isFacingRight)
            {
                dir = new Vector2(1f, 0f);
            }
            else
            {
                dir = new Vector2(-1f, 0f); 
            }
        }

        while (Time.time < dashStartTime + dashLength)
        {
            rb2D.velocity = dir.normalized * dashSpeed;
            yield return null; 
        }

        isDashing = false;
        rb2D.gravityScale = 1f;
        hasDashed = false; 
    }
}
