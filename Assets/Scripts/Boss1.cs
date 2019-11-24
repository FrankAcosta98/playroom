using System.Globalization;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [SerializeField]
    private Transform[] points; //Arreglo de puntos a los que se moverá
    public float speed; 
    public float cooldown; //Tiempo para cambiar
    private int Cpoint = 0; //Indice de puntos
    private float wait;
    private bool hmm = false;
    private bool chilling = false;
    public float chill; //Tiempo para regresar a chilling
    private float chillLevel;
    public CircleCollider2D detect; //Detección de Lucy
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
        if (hmm)
        {
            Hunt(); //Se llama el método Hunt cuando se entra en estado de hmm
        }
        else //Si no se está en hmm se sigue moviendo
            Move();

        if (chillLevel <= 0) //Cuando chillLevel se encuentra bajo 0...
        {
            hmm = false; //Se resetea los booleanos
            chilling = false;
            if (MainChar.instace.GetComponent<MainChar>().tag == "Focus")
                MainChar.instace.GetComponent<MainChar>().tag = "detectable"; //Y Lucy vuelve a ser detectable
        }
        if (chilling) //Si estás en chilling
        {
            chillLevel -= Time.deltaTime; //Se reduce
        }
    }
    private void Move() //Método que sirve para patrullar
    {
        transform.position = Vector2.MoveTowards(transform.position, points[Cpoint].position, speed * Time.deltaTime); //Aquí se indica que punto del arreglo se moverá

        if (Vector2.Distance(transform.position, points[Cpoint].position) < 0.2f) //Cuando te acercas
        {
            if (wait <= 0) //Cuando llegas a 0..
            {
                if (Cpoint + 1 == points.Length)//Si llegas al limite se resetea a 0
                {
                    Cpoint = 0;
                }
                else //Si no se suma 1 para pasar al siguiente
                {
                    Cpoint += 1;
                }
                wait = cooldown;
            }
            else //Si no has llegado a 0
            {
                wait -= Time.deltaTime; //Reduces el tiempo
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D prey) //Cuando te detectan
    {
        if (prey.gameObject.transform.tag == "detectable") 
        {
            hmm = true; //El boss entra en hmm
            prey.gameObject.tag = "Focus"; //Te vuelves un focus
            chillLevel = chill; //Se empieza el chill
            //
            Debug.Log(prey.gameObject.transform.tag);
        }
    }
    private void OnTriggerExit2D(Collider2D prey) //Cuando te alejas
    {
        chilling = true; //Te calmas

    }
    private void Hunt() //Método para cazar
    {
        if (GameObject.FindGameObjectWithTag("Focus") != null) //Cuando existe prey
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Focus").transform.position, speed * Time.deltaTime);//Te diriges al objeto de focus
    }
}