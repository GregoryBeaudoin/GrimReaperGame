using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionSwitch : MonoBehaviour
{
    public Animator companionAnim; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("TestPlayer").GetComponent<PlayerMovement>().isFaster == true)
        {
            Debug.Log("Soul Change");
            companionAnim.SetBool("isFast", true);
        }
        if (GameObject.Find("TestPlayer").GetComponent<PlayerMovement>().isDasher == true)
        {
            companionAnim.SetBool("isDash", true);
        }
        if (GameObject.Find("TestPlayer").GetComponent<PlayerMovement>().isExplosive == true)
        {
            companionAnim.SetBool("isBoom", true);
        }
        if (GameObject.Find("TestPlayer").GetComponent<PlayerMovement>().isStronger == true)
        {
            companionAnim.SetBool("isStrong", true);
        }
    }

    
}
