using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankEnemyControllerFrost : Enemy, IDamageable
{
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
	
	public AudioClip attackAudioClip;
	public AudioClip hitAudioClip;
	public AudioClip deathAudioClip;
	public AudioSource audioSource;
	
	public float wait = 3000f;

    public int Health {get; set; }
    
    public void Damage()
	{
		audioSource.clip = hitAudioClip;
		audioSource.volume = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        audioSource.Play();
		
		Health++;
		
        knockback();

		if (Health > 3){
			animator.SetBool("isDead", true);
		}
	}

    public void Dead()
	{
		audioSource.clip = deathAudioClip;
		audioSource.volume = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        audioSource.Play();
		GameObject.Find ("TestPlayer").GetComponent<CharCont> ().playerHealth +=10;
		Destroy(gameObject);
	}
	
	public void knockback()
	{
		if (GameObject.Find ("TestPlayer").GetComponent<CharCont> ().isFacingRight == true)
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * 150, ForceMode2D.Impulse);
		else 
			GetComponent<Rigidbody2D>().AddForce(Vector2.left * 150, ForceMode2D.Impulse);
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
            hit = Physics2D.Raycast(raycast.position, Vector2.left, raycastLength, raycastMask);
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
		
		EnemyLogic();
		//Debug.Log(hit.collider);
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
            StopAttack();
        }
        else if((attackRange >= distance) && (isCooling == false))
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

        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("walking-tankfrost"))
        {
            Vector2 targetPosition = new Vector2(target.transform.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void Attack()
    {
		audioSource.clip = attackAudioClip;
		audioSource.volume = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        audioSource.Play();
		
		
		wait -= 1;
		
		if (wait <= 0)
		{
		GameObject.Find ("TestPlayer").GetComponent<CharCont> ().playerHealth -= 5f;
		Debug.Log(GameObject.Find ("TestPlayer").GetComponent<CharCont> ().playerHealth);
		wait = 3000f;
		}
		
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
            Debug.DrawRay(raycast.position, transform.right * raycastLength, Color.red);
        }
        else if(attackRange > distance)
        {
            Debug.DrawRay(raycast.position, transform.right * raycastLength, Color.green);
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