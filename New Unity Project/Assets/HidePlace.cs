using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : MonoBehaviour
{
    private bool hiding = false;
    public bool hided = false;

    // Use this for initialization
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // Mientras el jugador este escondido no se mueve hasta volver a oprimir el boton
        if (hiding == true && Input.GetKeyDown(KeyCode.E))
        {
            MainChar.instace.GetComponent<Collider2D>().enabled = false;
           // MainChar.instace.gameObject.SetActive(false);
            MainChar.instace.GetComponent<SpriteRenderer>().enabled = false;
            hided = true;
            
        }
        

        else if ((Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0) && hided == true)
        {
            MainChar.instace.GetComponent<Collider2D>().enabled = true;
           // MainChar.instace.gameObject.SetActive(true);
            MainChar.instace.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Al entrar en contacto y interactuan el other se vuelve player y hiding se activa 
        if (other.gameObject.name.Equals("Lucy"))
        {
            hiding = true;
            
        }
    }
}
