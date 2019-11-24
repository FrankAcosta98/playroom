using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    //El código sirve para poder recoger la llave y hacer que desaparezca
    [Header("Components")]
    public Animator anim;

    private bool usable = false;

    void Start()
    {
        anim = GetComponent<Animator>();
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
        if (other.gameObject.name.Equals("Lucy") && MainChar.instace.RaycastCheckUpdate()) //Si Lucy se acerca se puede usar
        {
            usable = true;
            anim.SetBool("seeing", true);
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if (other.gameObject.name.Equals("Lucy") || !MainChar.instace.RaycastCheckUpdate()) //Si Lucy se aleja no se puede usar
        {
            usable = false;
            anim.SetBool("seeing", false);
        }
    }
}
