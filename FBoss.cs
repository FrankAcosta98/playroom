using System.Globalization;
using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FBoss : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    public float speed;
    public float cooldown;
    private int Cpoint = 0;
    private float wait;
    private bool hmm = false;
    private bool chilling = false;
    public float chill;
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
        if (hmm) {
            Hunt();
        }
        else
            Move();

        if (chillLevel <= 0) { 
            hmm = false;
            chilling = false;
            if(MainChar.instace.GetComponent<MainChar>().tag=="Focus")
                MainChar.instace.GetComponent<MainChar>().tag="detectable";
        }
        if (chilling)
        {
            chillLevel -= Time.deltaTime;
        }
    }
    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, points[Cpoint].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, points[Cpoint].position) < 0.2f)
        {
            if (wait <= 0)
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
            else
            {
                wait -= Time.deltaTime;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D prey)
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
    private void OnTriggerExit2D(Collider2D prey)
    {
        chilling = true;
        
    }
    private void Hunt()
    {
        if(GameObject.FindGameObjectWithTag("Focus")!=null)
            transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Focus").transform.position, speed * Time.deltaTime);
    }
}
