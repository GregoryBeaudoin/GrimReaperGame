using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue : MonoBehaviour
{
    public string title;

    [TextArea(3, 35)]
    public string[] sentences; 

}
