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

    private Rigidbody2D enemyRB;
    public Animator animator;

	
	public void Damage()
	{
		Debug.Log(Health);
		Health++;
		
		if (Health > 5){
            animator.SetBool("isDead", true);
			Debug.Log("Dead");
			Destroy(gameObject);
		}
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

        if (isGrounded)
        {
            enemyRB.AddForce(new Vector2(distanceFromPlayer, pounceHeight), ForceMode2D.Impulse);
        }

        animator.SetBool("isAttacking", true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}
