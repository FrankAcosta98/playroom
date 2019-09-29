using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(BoxCollider2D))]
public class ClownAttack : MonoBehaviour
{
    //For Attack Routine
    BoxCollider2D boxCol;
    public int secondsToAttack, secondsToReach, secondsToStay, secondsToBack;
    private float t1, t2;
    public GameObject target = null;
    private Rigidbody2D rb;
    private Vector3 tarPos;
    private Vector3 oriPos;
    public bool attacking;
    CircleCollider2D kill;

    //For Collider Size

    public float finalSize = 1.5f;
    public float increaseSpeed = 1;
    //private BoxCollider2D colli;
    private Vector2 iniSize;
    public Vector2 currentSize;
    public Vector2 maxSize;
    // Start is called before the first frame update
    void Start()
    {
        //For Attack Routine
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        oriPos = transform.position;
        attacking = false;

        //For Collider Size
        //colli = GetComponent<BoxCollider2D>();
        iniSize = boxCol.size;
        currentSize = iniSize;

        maxSize = new Vector2(finalSize, finalSize);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //For Attack Routine
        t1 += Time.deltaTime / secondsToReach;
        t2 += Time.deltaTime / secondsToBack;


        //For Collider Size
        if (attacking == false)
        {
            GrowCollider();

        } else if (attacking == true)
        {
            ReduceCollider();
        }

    }

    private void ReduceCollider()
    {

        do
        {

            currentSize -= Vector2.one * increaseSpeed * Time.deltaTime;
            
            boxCol.size = currentSize;
        } while (currentSize.x > iniSize.x);


    }
    private void GrowCollider()
    {

        while (currentSize.x < maxSize.x)
        {
            currentSize += Vector2.one * increaseSpeed * Time.deltaTime;
            
            boxCol.size = currentSize;
        }


    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("MusicBox") && attacking == false)
        {
            target = collision.gameObject;
            Debug.Log("CAJAAAAAA");
            attacking = true;
            Attack();


        }
        else if (collision.gameObject.tag.Equals("Lucy") && attacking == false)
        {
            target = collision.gameObject;
            Debug.Log("SULLYVAN DAME A LA NIÑA");
            attacking = true;
            Attack();

        }

    }

    private void Attack()
    {
        //tarPos = target.transform.position;
        StartCoroutine(Prepare());
    }

    private IEnumerator Prepare()
    {
        yield return new WaitForSecondsRealtime(secondsToAttack);
        tarPos = target.transform.position;
        transform.position = Vector3.Lerp(transform.position, tarPos, t1);
        yield return new WaitForSecondsRealtime(secondsToStay);
        transform.position = Vector3.Lerp(transform.position, oriPos, t2);
        //Attack();
        attacking = false;
        target = null;
        Debug.Log("Ahorita vemos que pedo");
    }


    private void OnCollisionEnter2D(Collision2D colli)
    {
        if (colli.gameObject.transform.tag == "Lucy")
        {
            Debug.Log("Ya te cargo el payaso");
            //Destroy(colli.gameObject);
            //Cuando se sepa como se llamara la escena de muerte agregar aquí
            //SceneManager.LoadScene("Nombre de la escena de muerte");
            

        } else if (colli.gameObject.transform.tag== "Music Box")
        {
            Debug.Log("Caja rota");
            //Destroy(colli.gameObject);
        }
    }
    
        

}
