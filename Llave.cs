using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour
{
    private bool usable = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && usable == true)
        {
            MainChar.instace.gameObject.GetComponent<MainChar>().hasKey = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy")  /* Agregar Raycast*/)
        {
            usable = true;
           
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy")  /* Agregar Raycast*/)
        {
            usable = false;

        }
    }
}
