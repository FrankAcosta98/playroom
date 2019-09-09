
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    // Start is called before the first frame update
    private bool block = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && block == false)
        {
            Debug.Log("Abierto");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy") && other.gameObject.GetComponent<MainChar>().hasKey == true)
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

    // OntriggerExit Falso
}
