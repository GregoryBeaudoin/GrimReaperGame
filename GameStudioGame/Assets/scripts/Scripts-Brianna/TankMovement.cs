using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public float speed;
    public float raycastDistance;
    public Transform groundDetection;

    private bool isGrounded;
    private Rigidbody2D enemyRB;
    private bool isMovingRight = true;
    
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetection.position, Vector2.down, raycastDistance);

        if (groundCheck.collider == false)
        {
            if (isMovingRight == true)
            {
                transform.eulerAngles =  new Vector2(0, 0);
                isMovingRight = false;
            }
            else
            {
                transform.eulerAngles =  new Vector2(0, -180);
                isMovingRight = true;
            }
        }
    }
}
