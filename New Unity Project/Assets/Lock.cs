using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    // Start is called before the first frame update
    private bool block;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(block == false)
        {
            Debug.Log("Abierto");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Lucy") && other.gameObject.GetComponent<MainChar>().hasKey == true /*Raycast */)
        {
            block = false;
        }

    }
}
