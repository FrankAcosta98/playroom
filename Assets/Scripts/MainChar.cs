using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainChar : MonoBehaviour
{
    //Este codigo sirve para que Lucy haga todo lo que tenga que hacer

    public static MainChar instace; //Para crear al personaje
    //private SavedFiles Save; //sirve para guardar su posición
    public Collider2D HitBox; //Collider que representa el "cuerpo" de Lucy
    public float velocidad; //velocidad original
    public Rigidbody2D rb;
    protected Vector2 mvmt; //Indica la dirección del movimiento
    protected Vector2 lookAt; //Indica a donde verá el sprite
    public Animator anim;
    public bool hasKey = false;
    public float dashVel; //Velocidad al correr
    public bool hasBox = false;
    private bool isMov = false; //Determina si se puede mover
    private bool isDown;
    private bool isUp;
    private float vel; //Velocidad dinámica
    private float dashT;
    public float dashDur; //Duración del dash
    private bool dash = true;
    private float dashChg; //Sirve para medir cuando ya haya pasado el cooldown
    public float dashC; //Cooldown para volver a dashear

    void Start()
    {
        isUp = false;
        //transform.position = Save.lastCheckPoint;
        instace = this;
        anim = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.gameObject.tag = "detectable"; //Importante que el tag sea detectable para que los monstruos sigan a Lucy
        vel = velocidad;
        dashT = dashDur + 1;
    }
    void Update()
    {
        //Determina si se está moviendo o no
        mvmt.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        //Se mide la carga del Dash
        dashChg += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && dash && dashChg >= dashC && hasBox == false)
        {
            if (transform.GetChild(0).gameObject.activeSelf == false)
            {
                velocidad = vel * dashVel; //La velocidad aumenta
                dashChg = 0.0f; //Y la carga se vuelve 0
            }
            else if (GetComponentInChildren<TeddyStates>().Holded == false)
            {
                velocidad = vel * dashVel; //La velocidad aumenta
                dashChg = 0.0f; //Y la carga se vuelve 0
            }
        }
    }
    void FixedUpdate()
    {
        if (mvmt.x != 0 || mvmt.y != 0)
            isMov = true;
        else
            isMov = false;
        //Se determina para donde se mueve y que Animador llamará
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") > 0)
        {
            isUp = true;
            anim.SetFloat("Hor", 0.0f);
        }
        else
            isUp = false;
        if (Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") < 0)
        {
            isDown = true;
            anim.SetFloat("Ver", 0.0f);
        }
        else
            isDown = false;
        if (isDown == false)
            anim.SetFloat("Ver", mvmt.y);
        if (isUp == false)
            anim.SetFloat("Hor", mvmt.x);
        anim.SetFloat("Spd", mvmt.sqrMagnitude);
        anim.SetBool("hasTed", true);
        anim.SetBool("hasBox", hasBox);
        anim.SetBool("isMov", isMov);
        //Si se apreta Space y se puede dashear; y si la carga de Dash es mayor al cooldown; y si no se tiene caja ni se mantiene cargado a Teddy...

        if (dashT > dashDur) //Si el tiempo con Dash se vuelve mayor a la duración..
        {
            dashT = 0; //El tiempo de dash se queda en 0
            dash = false;
            velocidad = vel; //Velocidad base
        }
        if (dashT < dashDur) //Si el tiempo con Dash es menor a la duración..
            dash = true;
        dashT += Time.fixedDeltaTime; //Se va aumentando el tiempo dasheando según el tiempo
        rb.MovePosition(rb.position + mvmt.normalized * velocidad * Time.fixedDeltaTime); //Movimiento
    }

    public bool RaycastCheckUpdate() //Este método sirve para revisar la dirección del Raycast y las animaciones
    {
        //El tag indica la dirección de la animación
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("f"))
            lookAt.Set(transform.position.x, transform.position.y - 3);
        else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("l"))
            lookAt.Set(transform.position.x - 3, transform.position.y);
        else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("r"))
            lookAt.Set(transform.position.x + 3, transform.position.y);
        else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("b"))
            lookAt.Set(transform.position.x, transform.position.y + 3);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookAt);
        if (hit.collider!=null) //Para verificar la colisión
        {
            if (hit.distance <= lookAt.magnitude)
                return true;
            else
                return false;
        }
        else
            return false;
    }

}