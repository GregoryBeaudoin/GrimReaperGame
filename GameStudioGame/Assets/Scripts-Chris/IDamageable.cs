using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
	public int Health {get; set;}
	public void Damage()
	{
		Health--;
		Debug.Log(Health);
		if (Health < 1)
		{
			Debug.Log(Health);
			//_anim.SetTrigger(_deathAnimHash);
			//Destroy(gameObject);
		}
		//else
			//_anim.SetTrigger(_hitAnimHash);
	}

}
