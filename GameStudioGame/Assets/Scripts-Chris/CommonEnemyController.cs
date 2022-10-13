using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEnemyController : Enemy, IDamageable
{
	public int Health {get; set; }
    public float speed;
    private Transform player;
    public float lineOfSight;
	
	public void Damage()
	{
		Health++;
		
		if (Health > 5){
			Debug.Log("Dead");
			Destroy(gameObject);
		}
	}

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
    }
}