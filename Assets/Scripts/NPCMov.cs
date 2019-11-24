using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMov : MonoBehaviour
{

    [Header("Components")]
    public Animator anim; //Animator
    public CircleCollider2D detect; //Detección de Lucy
    public Rigidbody2D rb; //Un clasico

    [Header("Variables")]
    public float speed; //Velocidad del enemigo
    public float cooldown; //Tiempo para cambiar

    [Header("Puntos")]
    public Transform[] points; //Arreglo de puntos a los que se moverá

    private int Cpoint = 0; //Indice de puntos
    private float wait; //Tiempo de espera
    private Vector2 dir; //Hacia donde apunta

    //Se inicialisan los valores y se agarran los componentes
    void Start()
    {
        wait = cooldown;
        transform.position = points[Cpoint].transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        dir = new Vector2(this.gameObject.transform.position.y, this.gameObject.transform.position.x).normalized;
    }

    void Update()
    {
        Move();
    }

    //Le manda la direccion al animator como una condicion de movimiento
    private void ChangeDir()
    {
        anim.SetFloat("x", dir.x);
        anim.SetFloat("y", dir.y);
    }

    private void Move() //Método que sirve para patrullar
    {
        this.transform.position = Vector2.MoveTowards(transform.position, points[Cpoint].position, speed * Time.deltaTime); //Aquí se indica que punto del arreglo se moverá
        dir = (points[Cpoint].transform.position - this.transform.position).normalized; //Se genera a direccion basado en el punto actual
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
}