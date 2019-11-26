using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown : MonoBehaviour
{

    [Header("Components")]
    public BoxCollider2D Range; //Determina el rango de detección
    public GameObject head;
    public Animator anim;
    //private GameObject[] Chain=new GameObject[7];
    [Header("Vectors")]
    public Vector2 initialSize = new Vector2(5.9f, 4f); //Tamaño inicial
    public Vector2 maxSize = new Vector2(9.23f, 7.41f); //Tamaño máximo
    private Vector2 currentSize; //Tamnaño actual

    [Header("Variables")]
    public float growingSpeed = 0.5f; //Velocidad a la que se crece
    public float secondsToAttack = 2f; //Tiempo para tomar acción
    public float headSpeed = 2.5f; //Velocidad de movimiento de la cabeza
    private float waitToAttack = 0f;
    private bool attacking; //Se ataca
    private bool attacked; //Se atacó
    private bool maxRange; //Determina si se llego al rango maximo
    public float atakRange = 5f;
    private bool stop = false;

    void Start()
    {
        this.GetComponentInChildren<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation; ;
        //Inicializa Valores
        Range = GetComponent<BoxCollider2D>();
        initialSize = Range.size;
        currentSize = initialSize;
        attacking = false;
        maxRange = false;
        attacked = false;
        anim.SetFloat("spd", growingSpeed);
    }

    void Update()
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
            anim.SetBool("Active", false);
            Attack(); //Se activa el método de ataque
        }

    }

    private void Attack()
    {
        if (secondsToAttack > waitToAttack) //Si los segundos para atacar son mayores al tiempo para esperar..
        {
            waitToAttack += Time.deltaTime; //aumentamos el waitToAttack
        }
        else //Cuando waitToAttack sea mayor...
        {
            //Vector2.Distance(head.transform.position, transform.position) <= atakRange
            if (GameObject.FindGameObjectWithTag("Focus") == null){
                waitToAttack = 0f;
                attacking = false;
                attacked = true;
            }
            else {
                head.transform.position = Vector2.MoveTowards(head.transform.position, GameObject.FindGameObjectWithTag("Focus").transform.position, headSpeed * Time.deltaTime); //Nos movemos hacia el target
                if (Vector2.Distance(GameObject.FindGameObjectWithTag("Focus").transform.position, head.transform.position) <= 0.001)//Cuando ya estamos cerca
                {
                    //Declaramos que ya atacamos y ya no estamos atacando
                    waitToAttack = 0f;
                    attacking = false;
                    attacked = true;
                }
            }
        }
    }

    private void Retreat()
    {
        if (secondsToAttack > waitToAttack) //Si los secondsToAttack son mayores al waitToAttack
        {
            waitToAttack += Time.deltaTime; //Aumentamos waitToAttack

        }
        else //En caso contrario
        {
            anim.SetBool("Active", false);
            //Volvemos a la posición original
            head.transform.position = Vector2.MoveTowards(head.transform.position, gameObject.transform.position, headSpeed * Time.deltaTime);
            vamonos(true);
            if (Vector2.Distance(this.gameObject.transform.position, head.transform.position) < 0.001)
            {
                if (MainChar.instace.GetComponent<MainChar>().tag == "Focus")
                    MainChar.instace.GetComponent<MainChar>().tag = "detectable";
                waitToAttack = 0f;
                attacking = false;
                attacked = false;
                vamonos(false);
            }
        }

    }
    #region Range

    private void GrowRange() //Metodo para crecer el Collider
    {
        anim.SetBool("Active", true);
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
        anim.SetBool("Active", true);
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
        if (prey.gameObject.transform.tag == "detectable" && attacking == false) //Si eres Lucy
        {
            anim.SetBool("Active", false);
            prey.gameObject.transform.tag = "Focus";
            attacking = true;
        }
    }
    #endregion
    private void vamonos(bool si)
    {
        if (si)
        {
            for (int i = 2; i < 9; i++)
                transform.GetChild(i).transform.position = Vector2.MoveTowards(transform.GetChild(i).transform.position, transform.GetChild(1).transform.position, headSpeed * Time.deltaTime);
        }
    }
}
