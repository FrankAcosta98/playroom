using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class HidePlace : MonoBehaviour
{
    [Header("Components")]
    public Animator anim;
    public Sprite seeing;
    public Sprite notSeeing;

    private bool hiding = false;
    private bool hided = false;

    void Start()
    {
        anim = GetComponent<Animator>();

    }

    void Update()
    {
        // Mientras el jugador este escondido no se mueve hasta volver a oprimir el boton
        if (hiding == true && Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("hiding", true);
            MainChar.instace.GetComponent<BoxCollider2D>().enabled = false;
            MainChar.instace.GetComponentInChildren<CircleCollider2D>().enabled=false;
            MainChar.instace.GetComponentInChildren<Light2D>().enabled=false;
            MainChar.instace.GetComponent<SpriteRenderer>().enabled = false;
            hided = true;
            //desactivar luz
        }


        else if ((Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0) && hided)
        {
            anim.SetBool("hiding", false);
            MainChar.instace.GetComponent<BoxCollider2D>().enabled = true;
            MainChar.instace.GetComponentInChildren<CircleCollider2D>().enabled=true;
            MainChar.instace.GetComponentInChildren<Light2D>().enabled=true;
            MainChar.instace.GetComponent<SpriteRenderer>().enabled = true;
            //desactivar luz
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Al entrar en contacto y interactuan el other se vuelve player y hiding se activa
        if (other.gameObject.name.Equals("Lucy") && MainChar.instace.RaycastCheckUpdate())
        {
            if (!hided)
            {
                anim.enabled = false;
            }
            
            hiding = true;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = seeing;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy") || !MainChar.instace.RaycastCheckUpdate())
        {
            anim.enabled = true;
            hiding = false;
            hided = false;
            this.gameObject.GetComponent<SpriteRenderer>().sprite = notSeeing;
        }
    }
}
