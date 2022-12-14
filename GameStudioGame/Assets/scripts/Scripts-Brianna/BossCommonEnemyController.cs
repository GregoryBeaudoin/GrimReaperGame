using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCommonEnemyController : Enemy, IDamageable
{
	public int Health {get; set; }
    public float speed;
    public float lineOfSight;

    [SerializeField] float pounceHeight;
    [SerializeField] Transform player;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Vector2 boxSize;
    private bool isGrounded = true;
    private bool isJumping = false;

    private Rigidbody2D enemyRB;
    public Animator animator;

	public AudioClip attackAudioClip;
	public AudioClip hitAudioClip;
	public AudioClip deathAudioClip;
	public AudioSource audioSource;
	
	public float wait = 3000f;
	
	public void Damage()
	{
		audioSource.clip = hitAudioClip;
		audioSource.volume = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        audioSource.Play(); 
		
		Health++;

		if (GameObject.Find("TestPlayer").GetComponent<PlayerMovement>().isStronger == true)
        {
			Health += 1; 
        }

		knockback();
		
		if (Health > 10){
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
		Debug.Log(health);
    }

    void JumpAttack()
    {
		audioSource.clip = attackAudioClip;
		audioSource.volume = PlayerPrefs.GetFloat("EffectsVolume", 0.75f);
        audioSource.Play();
		
		wait -= 1;
		
		if (wait <= 0)
		{
		GameObject.Find ("TestPlayer").GetComponent<CharCont> ().playerHealth -=3;
		Debug.Log(GameObject.Find ("TestPlayer").GetComponent<CharCont> ().playerHealth);
		wait = 3000f;
		}
		
		
		
        float distanceFromPlayer = player.position.x - transform.position.x;

        //Debug.Log(isGrounded);

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
