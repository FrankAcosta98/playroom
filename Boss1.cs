using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour {

    [Header("Components")]
    public Animator anim; //Animator
    public CircleCollider2D detect; //Detección de Lucy
    public Rigidbody2D rb; //Un clasico

    [Header("Variables")]
    public float speed; //Velocidad del enemigo
    public float cooldown; //Tiempo para cambiar
    public bool hmm = false; //Booleano que define si detecto al jugador
    public float chill; //Tiempo para regresar a chilling

    [Header("Puntos")]
    public Transform[] points; //Arreglo de puntos a los que se moverá

    private int Cpoint = 0; //Indice de puntos
    private float wait; //Tiempo de espera
    private bool chilling = false; //Esta descansando
    private float chillLevel; //Contador Chill
    private Vector2 dir; //Hacia donde apunta

    //Se inicialisan los valores y se agarran los componentes
    void Start()
    {
        wait = cooldown;
        transform.position = points[Cpoint].transform.position;
        rb = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        chillLevel = chill;
        dir = new Vector2(this.gameObject.transform.position.y,this.gameObject.transform.position.x).normalized;
    }

    void Update()
    {
        if (hmm)
        {
            Hunt(); //Se llama el método Hunt cuando se entra en estado de hmm
            ChangeDir(); //Se llama al animator
        }
        else //Si no se está en hmm se sigue moviendo
        {
            Move();
            ChangeDir();//Se llama al animatorx2
        }

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
        {
            dir = (GameObject.FindGameObjectWithTag("Focus").transform.position - this.transform.position).normalized; //Se crea la direccion en base al jugador
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Focus").transform.position, speed * Time.deltaTime);//Te diriges al objeto de focus
        } 
    }
}
