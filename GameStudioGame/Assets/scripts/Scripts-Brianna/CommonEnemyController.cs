using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyController : Enemy, IDamageable
{
	public int Health {get; set; }
    public float speed;
    public float lineOfSight;

    [SerializeField] float pounceHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 boxSize;
    private bool isGrounded;
    private bool isJumping = false;

    private Rigidbody2D enemyRB;
    public Animator animator;

	
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
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * 150, ForceMode2D.Impulse);
		else 
			GetComponent<Rigidbody2D>().AddForce(Vector2.left * 150, ForceMode2D.Impulse);
	}

   // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody2D>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapBox(groundCheck.position, boxSize, 0, groundLayer);

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            JumpAttack();
        }

        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    void JumpAttack()
    {
        float distanceFromPlayer = player.position.x - transform.position.x;

        Debug.Log(isGrounded);

        if (isGrounded)
        {
            enemyRB.AddForce(new Vector2(distanceFromPlayer, pounceHeight), ForceMode2D.Impulse);
            animator.SetBool("isAttacking", true);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
