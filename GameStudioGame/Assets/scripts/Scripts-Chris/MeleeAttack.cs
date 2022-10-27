using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
	[Header("Attack Properties")]
	[SerializeField]
	private Transform hitboxPoint;
	[SerializeField]
	private float hitboxRange;
	[SerializeField]
	private LayerMask hitboxMask;
	
	private void OnDrawGizmos()
	{
			if (hitboxPoint is null)
				return;
			
			Gizmos.color = Color.red;
			Gizmos.DrawWireSphere(hitboxPoint.position, hitboxRange);
	}
	
	private void MainAttack()
	{
		Debug.Log("swing");
		Collider2D[] objs = Physics2D.OverlapCircleAll(hitboxPoint.position, hitboxRange, hitboxMask);

		foreach (var obj in objs)
		{
			Debug.Log("swing2");
			if (obj.TryGetComponent(out IDamageable hit))
			{
				Debug.Log("hit");
				hit.Damage();
			}
		}
	}
}
