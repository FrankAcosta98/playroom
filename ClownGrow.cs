using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownGrow : MonoBehaviour
{

    [Header("Components")]
    public BoxCollider2D Range; //Determina el rango de detección
    public GameObject target = null;

    [Header("Vectors")]
    public Vector2 initialSize = new Vector2(0.1f, 0.1f); //Tamaño inicial
    public Vector2 maxSize = new Vector2(2f, 2f); //Tamaño máximo
    private Vector2 currentSize; //Tamaño actual

    [Header("Variables")]
    public float growingSpeed = 0.5f; //Velocidad a la que se crece
    private bool attacking; //Se ataca
    private bool maxRange; //Determina si se llego al rango maximo

    void Start()
    {
        //Inicialisa Valores
        Range = GetComponent<BoxCollider2D>();
        initialSize = Range.size;
        currentSize = initialSize;
        attacking = false;
        maxRange = false;
    }

    void FixedUpdate()
    {
        if (attacking == false)//Si no se está atacando...
        {
            if (maxRange == false)
            {
                //Hacemos que el rango crezca mientras no hayamos llegado al maximo
                GrowRange();
            }
            else
            {
                //Si estamos en el maximo lo reducimos
                ReduceRange();
            }
        }
        else //Si se esta atacando..
        {
            ReduceRange(); //Reducimos rango
        }
    }

    private void GrowRange() //Método para aumentar el tamaño del collider
    {
        if (currentSize.x < maxSize.x) //Verificamos si el tamaño actual es menor que el maximo
        {
            currentSize += Vector2.one * growingSpeed * Time.deltaTime; //Lo hacemos crecer conforme al tiempo y velocidad

            if (currentSize.x > maxSize.x) //Si nos pasamos..
            {
                currentSize = maxSize; //Establecemos el tamaño actual como el máximo
                maxRange = true;
            }
            Range.size = currentSize;
        }
    }

    private void ReduceRange() //Método para reducir el  tamaño del collider
    {
        if (currentSize.x > initialSize.x) //Si nuestro tamaño es mayor que el inicial...
        {
            currentSize -= Vector2.one * growingSpeed * Time.deltaTime; //Lo reducimos

            if (currentSize.x < initialSize.x) //Si nos pasamos...
            {
                currentSize = initialSize; //Establecemos el tamaño como el original
                maxRange = false;
            }
            Range.size = currentSize;
        }
    }


    private void OnTriggerEnter2D(Collider2D prey) //Cuando entras al collider..
    {
        if (prey.gameObject.name.Equals("MusicBox") && attacking == false) //Si eres la caja
        {
            target = prey.gameObject;
            Debug.Log("CAJAAAAAA");
            attacking = true;
        }

        if (prey.gameObject.transform.tag == "detectable" && attacking == false) //Si eres Lucy
        {
            target = prey.gameObject;
            Debug.Log("SULLYVAN DAME A LA NIÑA");
            attacking = true;
        }
    }
}