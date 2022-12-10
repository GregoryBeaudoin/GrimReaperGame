using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fade_out : MonoBehaviour
{
    public Text tex;
    Color col;
    void Start()
    {
        col=tex.color;
    }

    // Update is called once per frame
    void Update()
    {
        if(col.a>0)
        {
            col.a-=Time.deltaTime;
            tex.color=col;
        }
        
    }
}