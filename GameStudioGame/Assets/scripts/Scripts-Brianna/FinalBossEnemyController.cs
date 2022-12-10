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

    //[SerializeField] public float speed;
    private float speed;
    [SerializeField] public float diveSpeed;
    [SerializeField] private Vector3[] positions;
    [SerializeField] private Vector3[] divePositions;
    private int index;
    private bool isMoving;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Stages());
        Split();
    }

    void Move()
    {
        isMoving = true;
        speed = 2f;

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


    void Dive()
    {
        isMoving = false;
        speed = 5f;

        transform.position = Vector2.MoveTowards(transform.position, divePositions[index], Time.deltaTime * diveSpeed);
        
        if (transform.position == divePositions[index])
        {
            if (index == divePositions.Length -1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
    }

    [SerializeField] GameObject projectile;

    void Split()
    {
        if (isMoving == true)
        {
            Debug.Log("hello");
            Instantiate(projectile);
        }
        else if (isMoving == false)
        {
            Debug.Log("boooooooo");
        }
    }

    IEnumerator Stages()
    {
        while ()
        Move();
        
        yield return new WaitForSeconds(5);
        Debug.Log("time's up");
        
        Dive();
    }
}
