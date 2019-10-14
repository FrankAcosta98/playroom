using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


[RequireComponent(typeof(BoxCollider2D))]
public class ClownAttack : MonoBehaviour
{
    //For Attack Routine
    BoxCollider2D boxCol;
    public int secondsToAttack, secondsToStay;
    public float vel1, vel2;
    public GameObject target = null;
    private Rigidbody2D rb;
    private Vector2 tarPos;
    private Vector2 oriPos;
    public bool attacking, moveToTarget, firstTimePassed, secondTimePassed;
    CircleCollider2D kill;

    void Start()
    {
        //For Attack Routine
        rb = GetComponent<Rigidbody2D>();
        boxCol = GetComponent<BoxCollider2D>();
        oriPos = transform.position;
        attacking = false;
        moveToTarget = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attacking == true)
        {
            
            if (firstTimePassed == false)
            {
                StartCoroutine(FirstTime());
                moveToTarget = true;
                if (moveToTarget == true && firstTimePassed == true)
                {
                    tarPos = target.transform.position;
                    do
                    {
                        rb.MovePosition(rb.position + tarPos.normalized * vel1 * Time.deltaTime);

                    } while (rb.position != tarPos);
                    if (rb.position == tarPos)
                    {
                        moveToTarget = false;
                        StartCoroutine(SecondTime());
                        if (moveToTarget == false && secondTimePassed == true)
                        {

                            do
                            {
                                rb.MovePosition(rb.position + oriPos.normalized * vel1 * Time.deltaTime);

                            } while (rb.position != oriPos);
                            if (rb.position == oriPos)
                            {
                                attacking = false;
                                firstTimePassed = false;
                                secondTimePassed = false;
                            }
                        }
                    }
                }
            }
            

            

            

            
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Detectable") && attacking == false)
        {
            target = collision.gameObject;
            Debug.Log("YA TE VOY");
            attacking = true;
        }
    }


    private IEnumerator FirstTime()
    {
        yield return new WaitForSecondsRealtime(secondsToAttack);
        firstTimePassed = true;
    }
    private IEnumerator SecondTime()
    {
        yield return new WaitForSecondsRealtime(secondsToStay);
        secondTimePassed = true;
    }


    private void OnCollisionEnter2D(Collision2D colli)
    {
        if (colli.gameObject.transform.tag == "Detectable")
        {
            Debug.Log("Ya te cargo el payaso");
            //attacking = false;
            //Destroy(colli.gameObject);
            //Cuando se sepa como se llamara la escena de muerte agregar aquí
            //SceneManager.LoadScene("Nombre de la escena de muerte");
            

        } 
    }
}
