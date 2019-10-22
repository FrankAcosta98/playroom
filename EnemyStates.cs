using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    CircleCollider2D coli;
    public float rad; //Valor del radio de detección
    public GameObject target;
    public float moveSpeed = 15; //Velocidad a la que se mueve
    public float rotationSpeed = 5; //Velocidad a la que rota
    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coli = GetComponent<CircleCollider2D>();
        coli.radius = rad;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null) //Si target existe...
        {
            //El monstruo rotara hacia este y se moverá a su posición
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);

            transform.position += transform.forward * Time.deltaTime * moveSpeed;



        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //El metodo sirve para determinar si es Lucy o una caja
    {
        if (collision.gameObject.tag.Equals("Focus") || collision.gameObject.tag.Equals("detectable"))
        {
            if (collision.gameObject.name.Equals("Lucy") || collision.gameObject.name.Equals("MusicBox"))
            {
                target = collision.gameObject;
                Debug.Log("Persiguiendo Lucy/Caja");

            } else
            {
                target = collision.gameObject;
                Debug.Log("Persiguiendo algo más");
            }
        }
        

    }



    private void OnTriggerExit2D(Collider2D collision) //Si te alejas ya no hay target
    {
        target = null;
        Debug.Log("Ya no persigo a nadie");
    }
}