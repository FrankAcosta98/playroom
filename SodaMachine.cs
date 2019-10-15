using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaMachine : MonoBehaviour
{
    //El código sirve unicamente para interactuar con la máquina de soda y que realice su animación

    [Header("Components")]
    public Animator anim;

    private bool interact;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Mientras el jugador este escondido no se mueve hasta volver a oprimir el boton
        if (interact == true && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("interact", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Al entrar en contacto y interactuan el other se vuelve player y se activa 
        if (other.gameObject.name.Equals("Lucy"))
        {
            interact = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision) //Cuando Lucy se aleja se desactiva la interacción
    {
        anim.SetBool("interact", false);
    }

}
