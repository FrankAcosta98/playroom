using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatern : MonoBehaviour
{

    [Header("Components")]
    public Transform[] moveSpots; //Arreglo para guardar los puntos a los que se moverá

    [Header("Variables")]
    public float velocidad;
    public float cooldown;

    private float wait;
    private int randomSpot;


    void Start()
    {
        wait = cooldown;
        randomSpot = Random.Range(0, moveSpots.Length); //Se define un valor entre 0 y el maximo
    }


    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, velocidad * Time.deltaTime); //Nos movemos al punto determinado por el random

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f) //cuando te acercas lo suficiente
        {
            if (wait <= 0) //Si el wait llega a 0...
            {
                randomSpot = Random.Range(0, moveSpots.Length); //Redefinimos el punto random
                wait = cooldown; //Empezamos de nuevo el cooldown
            }
            else
            {
                wait -= Time.deltaTime; //Reducimos el wait
            }
        }
    }


    void OnTriggerStay2D(Collider2D other) //Si Lucy entra
    {
        if (other.gameObject.name.Equals("Lucy"))
        {
            Destroy(other.gameObject); //La mata
        }
    }

}
