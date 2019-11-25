using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    public float movSpeed = 0.3f; //Tiempo que tarda en llegar a su destino
    private Rigidbody2D rb;
    private bool pickUpAllowed; //Determina si se puede levantar la caja
    public bool grabbed; //Determina si ya se levantó la caja
    public CircleCollider2D grab; //Areá para agarrarlo
    public float grabRad; //Areá del trigger para levantar la caja
    public float detectRad; //Areá del trigger para atraer monstruos
    public Collider2D hitbox; //El collider normal
    //public CircleCollider2D detectArea; 
    public float charge = 0.4f; //Tiempo para preparar la caja
    public float batery = 5f; //El tiempo que "suena" la caja
    public float charging; //Para determinar cuanto tiempo ya pasó
    public bool charged = false; //Determina si ya se cargó o no
    //private float throwD = 5f; //Distancia a la que se puede lanzar
    public bool throwed;
    public GameObject Lucy;

    public bool hasDirection; //metodo 3
    public float distanceToMove;
    [SerializeField]
    private Vector2 direction; //metodo 3
    private bool posReached;



    private void Start()
    {
        pickUpAllowed = false;
        grabbed = false;
        rb = this.GetComponent<Rigidbody2D>();
        grab.radius = grabRad;
        //detectArea.enabled = false;
        throwed = false;
        charged = false;
        hasDirection = false; //metodo 3
        posReached = false;
    }

    private void Update()
    {

        if (transform.tag == "detectable" || transform.tag == "Focus") //Si el tag de la caja es detectable...
            batery -= Time.deltaTime; //La batería comienza a reducirse con el tiempo
        if (batery <= 0) //Cuando la batería es igual o menor a 0..
            Destroy(gameObject); //La caja se destruye

        if (hasDirection)
        {
            transform.position = Vector2.MoveTowards(transform.position, direction, movSpeed * Time.deltaTime); //metodo3
        }
        /*
        if (Vector2.Distance(transform.position, direction) <= 0.01)
        {
            posReached = true;
        }
        */
    }

    private void FixedUpdate()
    {
        if (grabbed && throwed == false)
        {
            this.transform.position = Lucy.transform.position;
        }
        if (grabbed && charged == false)
        {
            charging += Time.deltaTime;
        }
        if (charging >= charge)
        {
            charged = true;
        }

        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E) && (grabbed == false) && throwed == false) //Si se permite levantar, se apreta E y no se ha levantado
        {
            PickedUp();
            MainChar.instace.hasBox = true; //Lucy consigue la caja

        }

        if (grabbed && charged && Input.GetKeyDown(KeyCode.E))
        {
            MainChar.instace.hasBox = false;  //Se quita la caja de Lucy
            Throw(); //Se lanza la caja
        }

    }

    private void Throw()
    {

        //detectArea.enabled = true;
        //grab.enabled = false;
        grab.radius = detectRad;
        grabbed = false;
        pickUpAllowed = false;
        this.tag = "detectable";
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Direction();
        hasDirection = true; //Metodo 3
        StartCoroutine(activeHitbox());
        throwed = true;
        //Vector2 temp = new Vector2(Lucy.GetComponent<Animator>().GetFloat("Hor"), Lucy.GetComponent<Animator>().GetFloat("Ver")); //Parte del método 1 de Roli
        //Setup(temp, BoxDirection()); //Parte del método 1 de Roli
        //rb.AddForce(Lucy.gameObject.GetComponent<MainChar>().lookAt * movSpeed); //Parte del metodo 2
        //this.GetComponent<Rigidbody2D>().AddForce(transform.forward * 100); //Revisar esta opcion
        //Metodo 3
        //hitbox.enabled = true;



        /*
        grabbed = false; //Ya no se está sosteniendo la caja
        rb.bodyType = RigidbodyType2D.Kinematic; //La caja vuelve a ser kinematica para poder moverse
        pickUpAllowed = false; //No se permite levantar (Porque ya no está en su área de levantar)
        hitbox.enabled = true; //Se vuelve a activar el collider que lo hace "físico"
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true; //Se reactiva el SpriteRenderer
        move = true; //Es Lucy, no la caja
        transform.gameObject.tag = "detectable"; //Pa los monstruos
        */
    }

    IEnumerator activeHitbox()
    {
        yield return new WaitForSecondsRealtime(1.0f);
        hitbox.enabled = true;
    }

    private void Direction() //Metodo 3
    {
        if ((Lucy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("f")))
        {

            direction = new Vector2(this.transform.position.x, (this.transform.position.y - distanceToMove));

        }
        else if (Lucy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("l"))
        {

            direction = new Vector2((this.transform.position.x - distanceToMove), this.transform.position.y);

        }
        else if (Lucy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("r"))
        {

            direction = new Vector2((this.transform.position.x + distanceToMove), this.transform.position.y);

        }
        else if (Lucy.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsTag("b"))
        {

            direction = new Vector2(this.transform.position.x, (this.transform.position.y + distanceToMove));

        }
    }


    //Método 1 de Roli
    /*
    private void Setup(Vector2 velocity, Vector3 direction)
    {
        rb.velocity = velocity.normalized * movSpeed;
        transform.rotation = Quaternion.Euler(direction);
    }

    private Vector3 BoxDirection()
    {
        float temp = Mathf.Atan2(Lucy.GetComponent<Animator>().GetFloat("Ver"), Lucy.GetComponent<Animator>().GetFloat("Hor")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp);
    }
    */ //Aqui finaliza el método 1 de Roli
    private void PickedUp()
    {
        grabbed = true; //Se establece que está levantanda
        hitbox.enabled = false; //Se desactiva el colider, ya que se usa el de Lucy cuando se levanta
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false; //Se desactiva el SpriteRenderer

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Lucy"))
        {
            pickUpAllowed = true;
            Lucy = collision.gameObject;
        }
        /*
                if (collision.name.Equals("Lucy") && throwed==true && posReached == true)
                {
                    hitbox.enabled = false;
                }
                */
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Lucy"))
        {
            pickUpAllowed = false;
            Lucy = null;
        }

        if (collision.name.Equals("Lucy") && throwed == true)
        {
            hitbox.enabled = true;
        }

    }

}