using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private bool pickUpAllowed; //Determina si se puede levantar ´la caja
    public bool grabbed; //Determina si ya se levantó la caja
    public Rigidbody2D rb; //El clásico
    public Collider2D grab; //Areá para agarrarlo
    public Collider2D hitbox; //El collider normal
    public Collider2D AoE; //Área de deteción
    public float charge = 0.4f; //Tiempo para preparar la caja
    public float batery = 5f; //El tiempo que "suena" la caja
    private float charging; //Para determinar cuanto tiempo ya pasó
    private bool charged = false; //Determina si ya se cargó o no
    private float throwD = 5f; //Distancia a la que se puede lanzar
    private bool move = false; //Para que Lucy no se mueva
    private Vector2 dirAbs; //Dirección sin determinar
    public float movT = 0.3f; //Tiempo que tarda en llegar a su destino
    private float tmpT = 0f; //No lo toquen
    private Vector2 dir; //Hacia donde apunta

    void Start()
    {
        grabbed = false;
        AoE.enabled = false;
        pickUpAllowed = false;
        rb.bodyType = RigidbodyType2D.Static;
        dirAbs.Set(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
    }

    void Update()
    {
        if (rb.bodyType == RigidbodyType2D.Kinematic) //Si es kinematico cancela la rotación del objeto
        {
            transform.rotation.Set(0, 0, 0, 0);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E) && (grabbed == false)) //Si se permite levantar, se apreta E y no se ha levantado
        {
            PickUp();
            MainChar.instace.hasBox = true; //Lucy consigue la caja
        }
        else if (grabbed && Input.GetKeyDown(KeyCode.E) && charged) //En cambio si ya se esta agrrando la caja, se apreta E y esta cargado
        {
            Throw(); //Se lanza la caja
            MainChar.instace.hasBox = false; //Se quita la caja de Lucy
            AoE.enabled = true; //Se activa el area de detección
        }

        if (Input.GetKeyDown(KeyCode.Q) && grabbed) //Si ya se levantó la caja y se apreta Q
        {
            charging += Time.deltaTime; //Charging comienza a aumentar su valor
        }
        if (charging >= charge) //Si charging es mayor a charge
        {
            charged = true; //Significa que la caja está cargada
            Debug.Log("cargado");
        }
        if (transform.tag == "detectable") //Si el tag de la caja es detectable...
            batery -= Time.deltaTime; //La batería comienza a reducirse con el tiempo
        if (batery <= 0) //Cuando la batería es igual o menor a 0..
            Destroy(gameObject); //La caja se destruye
        if (grabbed) //Si ya recojiste la caja se empieza el Animator
        {
            if (MainChar.instace.GetComponent<MainChar>().anim.GetCurrentAnimatorStateInfo(0).IsTag("f"))
                dir.Set(0, -throwD);
            else if (MainChar.instace.GetComponent<MainChar>().anim.GetCurrentAnimatorStateInfo(0).IsTag("l"))
                dir.Set(-throwD, 0);
            else if (MainChar.instace.GetComponent<MainChar>().anim.GetCurrentAnimatorStateInfo(0).IsTag("r"))
                dir.Set(throwD, 0);
            else if (MainChar.instace.GetComponent<MainChar>().anim.GetCurrentAnimatorStateInfo(0).IsTag("b"))
                dir.Set(0, throwD);
        }
    }
    private void FixedUpdate()
    {
        if (move) //Si el movimiento está activo...
        {
            if (tmpT < movT) //Y si el valor de tmpT es menor al del movimiento total de la caja
            {
                rb.MovePosition(rb.position + dir * Time.fixedDeltaTime); //La caja se mvovera en la dirección de dir
                tmpT += Time.deltaTime; 
            }
        }
        if (tmpT >= movT) //Si el valor de tmpT es mayor o igual que el de movT...
        {
            tmpT = 0.0f; //tmpT se queda en ceros
            move = false; //El movimiento se desactiva
        }
        if (grabbed) //Si se recoje la caja...
        {
            this.transform.position = GameObject.Find("Lucy").transform.position; //Se sigue la posición de Lucy
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //Cuando se entre en una collision...
    {
        if (collision.gameObject.name.Equals("Lucy") && MainChar.instace.RaycastCheckUpdate()) //Si es Lucy y el Raycast apunta a la caja...
        {
            pickUpAllowed = true; //Se permite levantarlo
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //Cuando un trigger sale ce la colision...
    {
        if (collision.gameObject.name.Equals("Lucy")) //Si era Lucy..
        {
            pickUpAllowed = false; //No se permite levantar
        }
    }
    private void PickUp() //Método para levantar la MusicBox
    {
        grabbed = true; //Se establece que está levantanda
        hitbox.enabled = false; //Se desactiva el colider, ya que se usa el de Lucy cuando se levanta
        this.gameObject.GetComponent<SpriteRenderer>().enabled = false; //Se desactiva el SpriteRenderer
    }
    private void Throw() //Método cuando se lanza
    {
        grabbed = false; //Ya no se está sosteniendo la caja
        rb.bodyType = RigidbodyType2D.Kinematic; //La caja vuelve a ser kinematica para poder moverse
        pickUpAllowed = false; //No se permite levantar (Porque ya no está en su área de levantar)
        hitbox.enabled = true; //Se vuelve a activar el collider que lo hace "físico"
        this.gameObject.GetComponent<SpriteRenderer>().enabled = true; //Se reactiva el SpriteRenderer
        move = true; //Es Lucy, no la caja
        transform.gameObject.tag = "detectable"; //Pa los monstruos
    }
}