using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [Header("Components")]
    public Collider2D AoE; //Se usa como área de detección para llamar a otros monstruos

    [Header("Variables")]
    public float chill; 

    private bool hmm = false;
    private bool chilling = false; //Determina si está activo o no
    private float chillLevel;

    void Start()
    {

    }

    void Update()
    {
        if (hmm)
        {
            Alarm();

        }
        if (chillLevel <= 0) //Si llegamos a cero o menor...
        {
            hmm = false; //Volvemos a calmarnos
            gameObject.transform.tag = "Mono"; //Y volvemos al tag original para que no nos sigan los monstruos
        }

        if (chilling)
        {
            chillLevel -= Time.deltaTime; //Reducimos chillLevel conforme al tiempo si estamos chilleando
        }
    }

    private void OnTriggerEnter2D(Collider2D prey) //El metodo sirve para determinar si un detectable entro al área y empezar su función
    {
        if (prey.gameObject.transform.tag == "detectable" && prey.GetType() == typeof(CircleCollider2D))
        {
            hmm = true;
            chilling = false;
            //prey.gameObject.GetComponent<Transform>().tag = "detectable";
            chillLevel = chill;
            Debug.Log(prey.gameObject.transform.tag);
        }

        if(prey.gameObject.transform.tag == "Enemigo" && prey.GetType() == typeof(CircleCollider2D))
        {
            prey.gameObject.GetComponent<Boss1>().hmm = true;
        }
    }

    private void OnTriggerExit2D(Collider2D prey) //Si se aleja Lucy se calma
    {
        chilling = true;


    }

    private void Alarm()
    {
        gameObject.transform.tag = "Focus"; //Se convierte en un objeto al que los monstruos van
    }
}