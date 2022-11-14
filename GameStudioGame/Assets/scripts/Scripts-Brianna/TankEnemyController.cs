using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyController : MonoBehaviour {

    public Transform raycast;
    public LayerMask raycastMask;
    public float raycastLength;
    public float attackRange; 
    public float speed;
    public float timer; 

    private RaycastHit2D hit;
    private Transform target;
    private Animator animator;
    private float distance;
    private bool attackMode;
    private bool inRange;
    private bool isCooling;
    private float intTimer;

    public int Health {get; set; }
    
    public void Damage()
	{
		Health++;
		
		if (Health > 3){
			animator.SetBool("isDead", true);
		}
	}

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Awake()
    {
        intTimer = timer;
    }

    void Update ()
    {
        if (!attackMode)
        {
            Move();
        }

        if (inRange)
        {
            hit = Physics2D.Raycast(raycast.position, transform.right, raycastLength, raycastMask);
            RaycastDebugger();
        }

        if(hit.collider != null)
        {
            EnemyLogic();
        }
        else if(hit.collider == null)
        {
            inRange = false;
        }

        if(inRange == false)
        {
            animator.SetBool("isWalking", true);
            StopAttack();
        }
	}

    void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.tag == "Player")
        {
            target = trig.transform;
            inRange = true;
            Flip();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if(distance > attackRange)
        {
            Move();
            StopAttack();
        }
        else if(attackRange >= distance && isCooling == false)
        {
            Attack();
        }

        if (isCooling)
        {
            Cooldown();
            animator.SetBool("isAttacking", false);
        }
    }

    void Move()
    {
        animator.SetBool("isWalking", true);

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("walking-tank"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; 
        attackMode = true; 

        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if(timer <= 0 && isCooling && attackMode)
        {
            isCooling = false;
            timer = intTimer;
        }
    }

    void StopAttack()
    {
        isCooling = false;
        attackMode = false;
        animator.SetBool("isAttacking", false);
    }

    void RaycastDebugger()
    {
        if(distance > attackRange)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLength, Color.red);
        }
        else if(attackRange > distance)
        {
            Debug.DrawRay(raycast.position, Vector2.left * raycastLength, Color.green);
        }
    }

    public void TriggerCooling()
    {
        isCooling = true;
    }

    private void Flip()
    {
        Vector2 rotation = transform.eulerAngles;

        if (transform.position.x < target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f;
        }

        transform.eulerAngles = rotation;
    }
}
