
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{

    private bool block = true;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && block == false)
        {
            Debug.Log("Abierto");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy") && MainChar.instace.gameObject.GetComponent<MainChar>().hasKey == true)
        {
            block = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy")  /* Agregar Raycast*/)
        {
            block = true;
        }
    }
}
