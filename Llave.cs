using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour
{
    //El código sirve para poder recoger la llave y hacer que desaparezca
    private bool usable = false;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && usable == true) //Si se interactua pudiendo usar la llave
        {
            MainChar.instace.gameObject.GetComponent<MainChar>().hasKey = true; //Lucy ahora tiene la llave
            Destroy(gameObject); //La llave desaparece
        }
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.name.Equals("Lucy")  /* Agregar Raycast*/) //Si Lucy se acerca se puede usar
        {
            usable = true;

        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy")  /* Agregar Raycast*/) //Si Lucy se aleja no se puede usar
        {
            usable = false;

        }
    }
}