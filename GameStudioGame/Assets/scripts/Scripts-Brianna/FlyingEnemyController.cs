using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyController : Enemy, IDamageable
{
	public int Health {get; set; }
    public float speed;
    public float lineOfSight;
    public float shootingRange;
    public float firingRate = 1f;
    private float fireTime;

	public void Damage()
	{
		Health++;
		
		if (Health > 3){
			Debug.Log("Dead");
			Destroy(gameObject);
		}
	}

    private Transform player;
    public GameObject bullet;
    public GameObject bulletParent;
    public Animator animator; 

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {

        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight && distanceFromPlayer > shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            animator.SetBool("isAttacking", true);
        }
        else if (distanceFromPlayer <= shootingRange && fireTime < Time.time)
        {
            Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            fireTime = Time.time + firingRate;
            animator.SetBool("isAttacking", true);
        }
        else
        {
            animator.SetBool("isAttacking", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
