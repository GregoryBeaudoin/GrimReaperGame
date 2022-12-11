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
		
		if (Health > 30){
			Destroy(gameObject);
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
    private float diveSpeed;
    [SerializeField] private Vector3[] positions;
    [SerializeField] private Vector3[] divePositions;
    private int index;
    private bool isMoving = false;

    private Transform player;
    public GameObject bullet;
    public GameObject bulletParent;

    public float bulletSpeed;
    public float lineOfSight;
    public float shootingRange;
    public float firingRate = 1f;
    private float fireTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(Stages());
        Dive();
        Split();
    }

    void Move()
    {
        isMoving = true;
        speed = 1f;

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
        diveSpeed = 2f;

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

    void Split()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
		if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
		{
			transform.position = Vector2.MoveTowards(this.transform.position, player.position, bulletSpeed * Time.deltaTime);
		}
		else if (distanceFromPlayer <= shootingRange && fireTime < Time.time)
		{
			Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
			fireTime = Time.time + firingRate;
		}
    }

    IEnumerator Stages()
    {
        Dive();
        
        yield return new WaitForSeconds(5);

        for (int i = 0; i < 5; i++)
        {
            Move();
        }
    }
}
