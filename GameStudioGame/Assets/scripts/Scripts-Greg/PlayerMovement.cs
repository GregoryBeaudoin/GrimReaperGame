using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayerMovement : MonoBehaviour
{
    public CharCont controller;
    public Rigidbody2D rb2D;
    public Animator animator;
    public GameObject projectile;

    public GameObject firstFragment;
    public GameObject firstProjectile;
    public GameObject secondFragment;
    public GameObject secondProjectile;
    public GameObject thirdFragment;
    public GameObject thirdProjectile;
    public GameObject fourthFragment;
    public GameObject fourthProjectile; 

    [SerializeField] public float runSpeed = 40f;
    public float launchVelocity = 700f;

    public bool isExplosive = true;
    public bool isStronger = false;
    public bool isFaster = false;
    public bool isDasher = false; 

    float horizontalMove = 0f;
    float verticalMove = 0f; 
    bool jump = false;
    bool dash = false;
    public string horizontalAxis;
    public string verticalAxis;
    private float horizontalDir;
    private float verticalDir; 

    [SerializeField] public float dashSpeed = 15f;
    [SerializeField] public float dashLength = 0.3f;
    [SerializeField] private float dashBufferLength = 0.1f;
    private float dashBufferCounter;
    public bool isDashing;
    private bool hasDashed;
    private bool canDash => dashBufferCounter > 0f && !hasDashed;

    public AudioClip flyingAudioClip;
    public AudioClip castingAudioClip;
    public AudioClip slashingAudioClip;
    public AudioClip dashingAudioClip; 
    public AudioSource audioSource; 

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("TestPlayer").GetComponent<CharCont>().statusIce == true)
        {
            runSpeed = 20f;
        }
        else
            runSpeed = 40f;
		
		if (GameObject.Find("TestPlayer").GetComponent<CharCont>().statusConfusion == true)
        {
            horizontalMove = Input.GetAxisRaw(horizontalAxis) * -runSpeed;
        }
		else
        {
            horizontalMove = Input.GetAxisRaw(horizontalAxis) * runSpeed;
        }
		
        verticalMove = Input.GetAxisRaw(verticalAxis);
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));

        //Jump
        if (Input.GetButtonDown("Jump") && (controller.isGrounded = true)) 
        {
            jump = true;
            animator.SetBool("isJumping", true);
            audioSource.clip = flyingAudioClip;
            audioSource.Play(); 
        }
        else if (controller.isGrounded == false)
        {
            jump = false;
            animator.SetBool("isJumping", false); 
        }


        //Attack
		if (Input.GetButtonDown("MainAttack"))
        {
            animator.SetBool("isAttacking", true);
            audioSource.clip = slashingAudioClip;
            audioSource.Play(); 
        }
		else
        {
            animator.SetBool("isAttacking", false);
        }	
			
        //Dash
        if (Input.GetButtonDown("Dash"))
        {
            dashBufferCounter = dashBufferLength;
            animator.SetBool("isDashing", true);
            audioSource.clip = dashingAudioClip;
            audioSource.Play(); 
        }
        else
        {
            dashBufferCounter -= Time.deltaTime;
            animator.SetBool("isDashing", false); 
        }

        //Cast
        if (Input.GetButtonDown("Cast"))
        {
            if (controller.isFacingRight)
            {
                animator.SetBool("isCasting", true);
                audioSource.clip = castingAudioClip;
                audioSource.Play(); 
                GameObject soul = Instantiate(projectile, transform.position, Quaternion.identity);
                soul.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(launchVelocity, 0, 0));
                if (soul.GetComponent<SoulProjectile>().isHit == true)
                {
                    Debug.Log("Stop"); 
                    soul.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(-launchVelocity, 0, 0));
                }
                        
                Destroy(soul, 0.75f);
            }
            else
            {
                animator.SetBool("isCasting", true);
                audioSource.clip = castingAudioClip;
                audioSource.Play(); 
                GameObject soul = Instantiate(projectile, transform.position, Quaternion.identity);
                soul.GetComponent<SpriteRenderer>().flipX = false; 
                soul.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(-launchVelocity, 0, 0));
                if (soul.GetComponent<SoulProjectile>().isHit == true)
                {
                    Debug.Log("Stop now"); 
                    soul.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector3(launchVelocity, 0, 0));
                }
                
                Destroy(soul, 0.75f);
            }
        }
        else
        {
            animator.SetBool("isCasting", false); 
        }
    }

    private void FixedUpdate()
    {
        if (canDash)
        {
            StartCoroutine(Dash(horizontalDir, verticalDir)); 
        }
        if (!isDashing)
        {
            controller.Move(horizontalMove * Time.deltaTime, dash, jump);
            jump = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" && controller.isGrounded == false)
        {
            jump = false; 
        }

        else if (collision.gameObject.name == "FirstSoul")
        {
            Debug.Log("Next Scene");
            LoadNextScene(); 
        }

        else if (collision.gameObject.name == "SpeedUpgrade")
        {
            Debug.Log("Increased speed");
            isFaster = true; 
            runSpeed = 60f;
            projectile = firstProjectile;
            Destroy(firstFragment);
            LoadNextScene(); 
        }
        else if (collision.gameObject.name == "DashUpgrade")
        {
            Debug.Log("Increased dash");
            isDasher = true; 
            dashSpeed = 20f;
            projectile = secondProjectile;
            Destroy(secondFragment);
            LoadNextScene(); 
        }
        else if (collision.gameObject.name == "ExplosiveSoulUpgrade")
        {
            Debug.Log("Explosive Souls"); 
            isExplosive = true;
            projectile = thirdProjectile;
            Destroy(thirdFragment); 
        }

        else if (collision.gameObject.name == "AttackUpgrade")
        {
            Debug.Log("Increased damage");
            isStronger = true;
            projectile = fourthProjectile;
            Destroy(fourthFragment); 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Platform" && controller.isGrounded == true)
        {
            jump = true; 
        }
    }

    IEnumerator Dash(float x, float y)
    {
        float dashStartTime = Time.time;
        hasDashed = true;
        isDashing = true;
        jump = false;

        rb2D.velocity = Vector2.zero;
        rb2D.gravityScale = 0f;
        rb2D.drag = 0f;

        Vector2 dir; 
        if (x != 0f || y != 0f)
        {
            dir = new Vector2(x, y); 
        }
        else
        {
            if (controller.isFacingRight)
            {
                dir = new Vector2(1f, 0f);
            }
            else
            {
                dir = new Vector2(-1f, 0f); 
            }
        }

        while (Time.time < dashStartTime + dashLength)
        {
            rb2D.velocity = dir.normalized * dashSpeed;
            yield return null; 
        }

        isDashing = false;
        rb2D.gravityScale = 1f;
        hasDashed = false; 
    }

    void LoadNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int nextLevelBuildIndex = 1 + scene.buildIndex;
        SceneManager.LoadScene(nextLevelBuildIndex);
    }
}
