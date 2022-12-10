using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime, Space.World);
    }
}
