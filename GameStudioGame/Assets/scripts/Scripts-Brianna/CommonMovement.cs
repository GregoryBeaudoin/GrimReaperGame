using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonMovement : MonoBehaviour
{
    public float speed;
    public float raycastDistance;
    public Transform groundDetection;

    private bool isGrounded;
    private Rigidbody2D enemyRB;
    private bool isMovingRight = true;
	private bool right = false;
	private float wait = 2127f;
	private bool isFacingRight = false;
    
    void Update()
    {
		/*
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        RaycastHit2D groundCheck = Physics2D.Raycast(groundDetection.position, Vector2.down, raycastDistance);

        if (groundCheck.collider == false)
        {
            if (isMovingRight == true)
            {
                transform.eulerAngles =  new Vector2(0, 180);
                isMovingRight = false;
            }
            else
            {
                transform.eulerAngles =  new Vector2(0, 0);
                isMovingRight = true;
            }
        }
		*/
		
		
		if (right)
		{
			
		 transform.Translate(Vector2.right * .5f * Time.deltaTime);
		 wait -= .5f;
			if (wait <= 0)
			{
				right = false;
				wait = 2127f;
				Flip();
			}
		}
		
		else
		{
			transform.Translate(Vector2.left * .5f * Time.deltaTime);
			wait -= .5f;
			if (wait <= 0)
			{
				right = true;
				wait = 2127f;
				Flip();
			}
		}
		
		
		
    }
	
	 private void Flip()
    {
        isFacingRight = !isFacingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale; 
    }
	
	
	
	
}
