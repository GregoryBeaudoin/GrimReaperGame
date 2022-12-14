using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events; 
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class CharCont : MonoBehaviour
{
	[Header("Player Status")]
	[SerializeField] public double playerHealth = 100;
	[SerializeField] public bool statusFire;
	[SerializeField] public bool statusIce;
	[SerializeField] public bool statusConfusion;
	
	public GameObject fireEffect;
    public GameObject iceEffect;
	public GameObject confusionEffect;
	
    [SerializeField] public float jumpForce = 300f;
    [SerializeField] public float dashSpeed = 15f;

    [Range(0, 0.3f)] [SerializeField] public float smoothMove = 0.05f;
    [SerializeField] private bool airControl = false;
    [SerializeField] private LayerMask isGround;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Collider2D dashDisableCollider;

	public Animator animator; 
    private float groundedRadius = 0.2f;
    public bool isGrounded;
    private Rigidbody2D rb2D;
    public bool isFacingRight = true;
    private Vector3 vectorVelocity = Vector3.zero;

    [Header("Events")]
    [Space]

    public UnityEvent onLandEvent; 

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    public BoolEvent onDashEvent;
    private bool wasDashing = false;

	public float effectTimer;
	public float randomChance;
	public bool isCountingDown = false;

	public void setFire()
	{
		if (isCountingDown == false)
		{
			randomChance = Random.Range(1.0f, 4.0f);
			if (randomChance <= 2.0f)
			{
				isCountingDown = true;
				effectTimer = Random.Range(1.0f, 5.0f);
				statusFire = true;
				fireEffect.SetActive(true);
			}
		}
	}

	public void setIce()
	{
		if (isCountingDown == false)
		{
			randomChance = Random.Range(1.0f, 4.0f);
			if (randomChance <= 2.0f)
			{
				isCountingDown = true;
				effectTimer = Random.Range(1.0f, 5.0f);
				statusIce = true;
				iceEffect.SetActive(true);
			}
		}
	}

	public void setConfusion()
	{
		if (isCountingDown == false)
		{
			randomChance = Random.Range(1.0f, 4.0f);
			if (randomChance <= 2.0f)
			{
				isCountingDown = true;
				effectTimer = Random.Range(1.0f, 5.0f);
				statusConfusion = true;
				confusionEffect.SetActive(true);
			}
		}
	}

	void Update()
	{
		if (playerHealth <= 0)
        {
            animator.SetBool("isDead", true);
        }
		
		if (effectTimer > 0) {
			effectTimer -= Time.deltaTime;
			if (statusFire == true)
				playerHealth -= .01;
		}
		else {
			isCountingDown = false;
			statusConfusion = false;
			confusionEffect.SetActive(false);
			statusIce = false;
			iceEffect.SetActive(false);
			statusFire = false;
			fireEffect.SetActive(false);
		}
	}

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>(); 

        if (onLandEvent == null)
        {
            onLandEvent = new UnityEvent(); 
        }
        if (onDashEvent == null)
        {
            onDashEvent = new BoolEvent(); 
        }
    }
	
	public void Start()
	{
		Physics.IgnoreLayerCollision(6, 7);
	}
	
	private void OnTriggerEnter2D(Collider2D collision) 
    {	
		if(GameObject.Find ("TestPlayer").GetComponent<PlayerMovement> ().isDashing == false)
		{
			if (collision.gameObject.CompareTag("Enemy"))
			{
				if (SceneManager.GetActiveScene().name == "Lava 1" || SceneManager.GetActiveScene().name == "Lava 2" || SceneManager.GetActiveScene().name == "Lava"  )
					setFire();
				if (SceneManager.GetActiveScene().name == "Ice 1" || SceneManager.GetActiveScene().name == "Ice 2" || SceneManager.GetActiveScene().name == "Ice"  )
					setIce();
				if (SceneManager.GetActiveScene().name == "Default 1" || SceneManager.GetActiveScene().name == "Default 2" || SceneManager.GetActiveScene().name == "Default"  )
					setConfusion();
				animator.SetBool("isHit", true);
				playerHealth-=10;
				Debug.Log(playerHealth);
				knockback();
			}
			
			if (collision.gameObject.CompareTag("Bullet"))
			{
				if (SceneManager.GetActiveScene().name == "Lava 1" || SceneManager.GetActiveScene().name == "Lava 2" || SceneManager.GetActiveScene().name == "Lava"  )
					setFire();
				if (SceneManager.GetActiveScene().name == "Ice 1" || SceneManager.GetActiveScene().name == "Ice 2" || SceneManager.GetActiveScene().name == "Ice"  )
					setIce();
				if (SceneManager.GetActiveScene().name == "Default 1" || SceneManager.GetActiveScene().name == "Default 2" || SceneManager.GetActiveScene().name == "Default"  )
					setConfusion();
				animator.SetBool("isHit", true);
				playerHealth-=10;
				Debug.Log(playerHealth);
				knockback();
				Destroy(collision.gameObject);
			}
		}	   
    }
	
	
	
	public void gameOver()
	{
		Scene level = SceneManager.GetActiveScene();
        int currentLevel = level.buildIndex;
		PlayerPrefs.SetInt("level", currentLevel);
		SceneManager.LoadScene("GameOver");	
	}

    public void RestartScene()
    {
        Scene level = SceneManager.GetActiveScene();
        int currentLevel = level.buildIndex;
        SceneManager.LoadScene(currentLevel);
    }
	
	public void endHit()
	{
		animator.SetBool("isHit", false);
	}
	
	public void knockback()
	{
		if (GameObject.Find ("TestPlayer").GetComponent<CharCont> ().isFacingRight == true)
			GetComponent<Rigidbody2D>().AddForce(Vector2.left * 100, ForceMode2D.Impulse);
		else 
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * 100, ForceMode2D.Impulse);
	}

    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, isGround); 
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true; 
                if (!wasGrounded)
                {
                    onLandEvent.Invoke(); 
                }
            }
        }
		
		
    }

    public void Move(float move, bool dash, bool jump)
    {
        if (isGrounded || airControl)
        {
            if (dash)
            {
                if (!wasDashing)
                {
                    wasDashing = true;
                    onDashEvent.Invoke(true); 
                }

                move *= dashSpeed; 

                if (dashDisableCollider != null)
                {
                    dashDisableCollider.enabled = true; 
                }

                if (wasDashing)
                {
                    wasDashing = false;
                    onDashEvent.Invoke(false); 
                }
            }

            Vector3 targetVelocity = new Vector2(move * 10f, rb2D.velocity.y);

            rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref vectorVelocity, smoothMove); 

            if (move > 0 && !isFacingRight)
            {
                Flip(); 
            }
            else if (move < 0 && isFacingRight)
            {
                Flip(); 
            }
        }

        if (jump)
        {
            //isGrounded = false;
            rb2D.AddForce(new Vector2(0f, jumpForce)); 
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
