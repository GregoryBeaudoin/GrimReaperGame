using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

	public GameObject door;

	private void OnTriggerEnter2D(Collider2D collision)
    {
		door.SetActive(true);
    }
}
