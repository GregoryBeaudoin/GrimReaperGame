using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulProjectile : MonoBehaviour
{
    public GameObject projectile;
    public Animator soulAnim;
    public bool isHit = false; 
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(8, 9);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Collision found");
            if (collision.gameObject.TryGetComponent(out IDamageable hit))
            {
                Debug.Log("hit");
                hit.Damage();
                if (GameObject.Find("TestPlayer").GetComponent<PlayerMovement>().isExplosive == true)
                {
                    Debug.Log("explode"); 
                    soulAnim.SetBool("isDestroyed", true);
                    isHit = true; 
                }
            }
            
            Destroy(projectile, 0.75f); 
        }
    }
}
