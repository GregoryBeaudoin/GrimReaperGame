using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
	public GameObject soul;
    // Start is called before the first frame update
    void Start()
    {
        soul.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
		if(GameObject.Find ("BossCommonEnemy").GetComponent<CommonEnemyController> ().Health == 0)
			soul.SetActive(true);
    }
}
