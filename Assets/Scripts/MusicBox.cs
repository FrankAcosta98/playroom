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
    public float detectRad; //Areá del trigger para atraer monstruos
    public Collider2D hitbox; //El collider normal
    public CircleCollider2D aoe;
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
        //detectArea.enabled = false;
        throwed = false;
        charged = false;
        hasDirection = false; //metodo 3
        posReached = false;
        GetComponent<CircleCollider2D>().= false;
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
        grabbed = false;
        pickUpAllowed = false;
        this.tag = "detectable";
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        Direction();
        hasDirection = true; //Metodo 3
        StartCoroutine(activeHitbox());
        throwed = true;

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