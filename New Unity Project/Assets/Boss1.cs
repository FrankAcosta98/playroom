using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [SerializeField]
    private Transform[] points;
    [SerializeField]
    public float speed;
    public float cooldown;
    private int Cpoint = 0;
    private float wait;
    private bool hmm = false;
    private bool chilling = false;
    public float chill;
    private float chillLevel;
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
            Hunt();
        else
            Move();
        if (chillLevel <= 0)
            hmm = false;
            //GameObject.FindGameObjectWithTag("Focus").tag = "detectable";
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
        if (prey.gameObject.transform.tag == "detectable" && prey.GetType() == typeof(CircleCollider2D))
        {
            hmm = true;
            prey.gameObject.tag = "Focus";
            chillLevel = chill;
        }

        if (prey.gameObject.transform.tag == "Focus" && this.GetType() == typeof(BoxCollider2D))
        {
            Debug.Log("Ya te cargo el payaso");
            //Destroy(prey.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D prey)
    {
        chilling = true;
    }
    private void Hunt()
    {
        transform.position = Vector2.MoveTowards(transform.position, GameObject.FindGameObjectWithTag("Focus").transform.position, speed * Time.deltaTime);
    }
}