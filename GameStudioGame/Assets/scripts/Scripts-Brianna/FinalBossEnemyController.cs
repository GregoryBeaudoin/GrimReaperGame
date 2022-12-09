using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossEnemyController : Enemy, IDamageable
{
	public int Health {get; set; }
    private Animator animator;

	public void Damage()
	{
		Health++;

		if (GameObject.Find("TestPlayer").GetComponent<PlayerMovement>().isStronger == true)
        {
			Health += 1; 
        }

			knockback();
		
		if (Health > 5){
			animator.SetBool("isDead", true);
		}
	}
	
	public void Dead()
	{
		Destroy(gameObject);
	}
	
	public void knockback()
	{
		if (GameObject.Find ("TestPlayer").GetComponent<CharCont> ().isFacingRight == true)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * 150, ForceMode2D.Impulse);
        }
			
		else
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * 150, ForceMode2D.Impulse);
        }
			
    }

    [SerializeField] public float speed;
    [SerializeField] private Vector3[] positions;
    private int index;

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, positions[index], Time.deltaTime * speed);
        
        if (transform.position == positions[index])
        {
            if (index == positions.Length -1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }
}
