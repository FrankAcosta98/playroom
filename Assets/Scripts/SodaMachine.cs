using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SodaMachine : MonoBehaviour
{

    [Header("Components")]
    public Animator anim;
    public Sprite seeing;
    public Sprite notSeeing;

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
            anim.enabled = true;
            anim.SetBool("interact", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Al entrar en contacto y interactuan el other se vuelve player y se activa 
        if (other.gameObject.name.Equals("Lucy"))
        {
            anim.enabled = false;
            interact = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = seeing;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy"))
        {
            anim.enabled = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = notSeeing;
            anim.SetBool("interact", false);
        }
    }
}
