using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown : MonoBehaviour
{

    [Header("Components")]
    public BoxCollider2D Range; //Determina el rango de detección
    public GameObject target = null;
    public GameObject head;
    public float headSpeed = 1f; //Velocidad de movimiento de la cabeza
    private Rigidbody2D rb;

    [Header("Vectors")]
    public Vector2 initialSize = new Vector2(0.1f, 0.1f); //Tamaño inicial
    public Vector2 maxSize = new Vector2(2f, 2f); //Tamaño máximo
    private Vector2 currentSize; //Tamnaño actual

    [Header("Variables")]
    public float growingSpeed = 0.5f; //Velocidad a la que se crece
    public float secondsToAttack = 2f; //Tiempo para tomar acción
    private float waitToAttack = 0f;
    private bool attacking; //Se ataca
    private bool attacked; //Se atacó
    private bool maxRange; //Determina si se llego al rango maximo

    void Start()
    {
        //Inicializa Valores
        head = Instantiate(head, this.gameObject.transform.position, Quaternion.identity);
        rb = GetComponent<Rigidbody2D>();
        Range = GetComponent<BoxCollider2D>();
        initialSize = Range.size;
        currentSize = initialSize;
        attacking = false;
        maxRange = false;
        attacked = false;
    }

    void FixedUpdate()
    {
        if (attacking == false) //Si no se está atacando...
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

            if (attacked == true)
            {
                Retreat(); //Se activa la retirada
            }
        }
        else if (attacking == true)
        {
            Attack(); //Se activa el método de ataque
        }
    }

    private void Attack()
    {
        if (secondsToAttack > waitToAttack) //Si los segundos para atacar son mayores al tiempo para esperar..
        {
            waitToAttack += Time.deltaTime; //aumentamos el waitToAttack
            Debug.Log(waitToAttack);
        }
        else //Cuando waitToAttack sea mayor...
        {
            head.transform.LookAt(target.transform.position); //Vemos al target
            head.transform.position += head.gameObject.transform.forward * headSpeed * Time.deltaTime; //Nos movemos hacia el target
            if (Vector2.Distance(target.transform.position, head.transform.position) < 0.1) //Cuando ya estamos cerca
            {
                //Declaramos que ya atacamos y ya no estamos atacando
                waitToAttack = 0f;
                attacking = false;
                attacked = true;
            }
        }
    }

    private void Retreat()
    {
        if (secondsToAttack > waitToAttack) //Si los secondsToAttack son mayores al waitToAttack
        {
            waitToAttack += Time.deltaTime; //Aumentamos waitToAttack
            Debug.Log(waitToAttack);
        }
        else //En caso contrario
        {
            //Volvemos a la posición original
            head.transform.LookAt(this.gameObject.transform.position);
            head.transform.position += head.gameObject.transform.forward * headSpeed * Time.deltaTime;
            if (Vector2.Distance(this.gameObject.transform.position, head.transform.position) < 0.1)
            {
                waitToAttack = 0f;
                attacking = false;
                attacked = false;
            }
        }

    }

    #region Range

    private void GrowRange() //Metodo para crecer el Collider
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

    #endregion

    #region Detector

    private void OnTriggerEnter2D(Collider2D prey) //Cuando entras al collider..
    {
        if (prey.gameObject.tag.Equals("MusicBox") && attacking == false) //Si eres la caja
        {
            target = prey.gameObject;
            Debug.Log("CAJAAAAAA");
            attacking = true;
        }

        if (prey.gameObject.transform.tag == "detectable" && attacking == false) //Si eres Lucy
        {
            target = prey.gameObject;
            Debug.Log("Vivimos en una sociedad");
            attacking = true;
        }
    }

    #endregion
}