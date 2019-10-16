using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;

    private bool hiding = false;
    private bool hided = false;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // Mientras el jugador este escondido no se mueve hasta volver a oprimir el boton
        if (hiding == true && Input.GetKeyDown(KeyCode.E)) //Interactuar con la caja/Casillero
        {
            anim.SetBool("hiding", true);
            //En estas líneas se desactivan los componentes para hacer a Lucy invisible e indetectable
            MainChar.instace.GetComponent<BoxCollider2D>().enabled = false;
            MainChar.instace.GetComponent<CircleCollider2D>().enabled = false;
            MainChar.instace.GetComponent<SpriteRenderer>().enabled = false;
            hided = true;
            //desactivar luz
        }


        else if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && hided == true)
        {
            anim.SetBool("hiding", false);
            MainChar.instace.GetComponent<BoxCollider2D>().enabled = true;
            MainChar.instace.GetComponent<CircleCollider2D>().enabled = true;
            MainChar.instace.GetComponent<SpriteRenderer>().enabled = true;
            //desactivar luz
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Al entrar en contacto y interactuan el other se vuelve player y hiding se activa, haciendo posible esconderse
        if (other.gameObject.name.Equals("Lucy"))
        {
            hiding = true;

        }
    }

    void OnTriggerExit2D(Collider2D other) //Si te alejas se desactiva la posibilidad de interactuar y se resetean las condiciones
    {
        if (other.gameObject.name.Equals("Lucy")  /* Agregar Raycast*/)
        {
            hiding = false;
            hided = false;
        }
    }
}