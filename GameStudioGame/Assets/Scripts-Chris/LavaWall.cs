using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaWall : MonoBehaviour
{
   private void OnTriggerEnter2D(Collider2D collision)
   {
	   if (GameObject.Find ("TestPlayer").GetComponent<PlayerMovement> ().isDashing == false)
	   {
		GameObject.Find ("TestPlayer").GetComponent<CharCont> ().playerHealth -= 8;
	   }
   }
}
