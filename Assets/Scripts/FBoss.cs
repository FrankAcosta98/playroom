using System.Globalization;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBoss : MonoBehaviour
{
    [SerializeField]
    private Transform[] points; //Arreglo de puntos a los que se moverá el Boss
    public float speed;
    public float cooldown; //Tiempo para volver a tomar acción
    private int Cpoint = 0; //Indice para controlar los puntos
    private float wait; //Tiempo establecido para tomar acción
    private bool hmm = false; //Si está cazando
    private bool chilling = false; //Si está calmado
    public float chill; //Determina el tiempo para volver al estado normal
    private float chillLevel;
    public CircleCollider2D detect;
    public Rigidbody2D rb;

    void Start()
    {
        wait = cooldown;
        transform.position = points[Cpoint].transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        chillLevel = chill;
    }
    void Update()
    {
        if (hmm) //COndición para ejecutar el método de ataque
        {
            Hunt();
        }
        else //Patrullar
            Move();

        if (chillLevel <= 0) //Si chillLevel llega a 0 se regresa a patrullar
        {
            hmm = false;
            chilling = false;
            if (MainChar.instace.GetComponent<MainChar>().tag == "Focus") //Lucy o la caja dejan de ser un objeto a perseguir y vuelven a la condición de detectable
                MainChar.instace.GetComponent<MainChar>().tag = "detectable";
        }
        if (chilling) //Condición para volver a la normalidad
        {
            chillLevel -= Time.deltaTime;
        }
    }
    private void Move() //Método de patrulla
    {
        transform.position = Vector2.MoveTowards(transform.position, points[Cpoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[Cpoint].position) < 0.2f) //Si la distancia entre el boss y el punto se hace menos de 0.2...
        {
            if (wait <= 0) //Se agrega está condición para ir aumentando el valor de Cpoint y pasar al siguiente
            {
                if (Cpoint + 1 == points.Length)
                {
                    Cpoint = 0;
                }
                else
                {
                    Cpoint += 1;
                }
                wait = cooldown;
            }
            else //Si wait fuera mayor a 0 hacemos que se reduzca hasta llegar
            {
                wait -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D prey) //Este método sirve para convertir a Lucy/Caja en un objeto Focus (Osea que van hacia el)
    {
        if (prey.gameObject.transform.tag == "detectable")
        {
            hmm = true;
            prey.gameObject.tag = "Focus";
            chillLevel = chill;
            //
            Debug.Log(prey.gameObject.transform.tag);
        }
    }
    private void OnTriggerExit2D(Collider2D prey) //Si prey se aleja volvemos a estado de chilling
    {
        chilling = true;

    }
    private void Hunt() //Estado de ataque
    {
        if (GameObject.FindGameObjectWithTag("Focus") != null) //Verifica que existe Focus y va hacia el
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Focus").transform.position, speed * Time.deltaTime);
    }
}