using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField]

    private GameObject flyEnemy;

    [SerializeField]

    private float flyEnemySwarm = 3.5f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(flyEnemySwarm, flyEnemy));
    }


    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);
        GameObject newEnemy = Instantiate(enemy, new Vector3(Random.Range(1, 9), Random.Range(1, 9), 0), Quaternion.identity);
        //StartCoroutine(spawnEnemy(interval, enemy));
    }
}
