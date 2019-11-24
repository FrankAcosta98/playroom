using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour
{
    //El código sirve para verificar si Lucy tiene la llave y está en rango de poder interactuar con la caja de fusibles

    [Header("Components")]
    public Sprite seeing;
    public Sprite notSeeing;

    private bool block = true;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && block == false) //Si no se está bloquedo se puede interactuar con E
        {
            Debug.Log("Abierto");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy") && MainChar.instace.RaycastCheckUpdate())
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = seeing;
        }

        if (other.gameObject.name.Equals("Lucy") && MainChar.instace.gameObject.GetComponent<MainChar>().hasKey == true)  //Si se tiene la llave se desbloquea y es posible usarlo
        {
            block = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) //Si Lucy se aleja no se puede usa
    {
        if (other.gameObject.name.Equals("Lucy") || !MainChar.instace.RaycastCheckUpdate())
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = notSeeing;
            block = true;
        }
    }
}
