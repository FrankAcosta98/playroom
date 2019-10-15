using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{
    [Header("Components")]
    public Collider2D AoE;

    [Header("Variables")]
    public float chill;

    private bool hmm = false;
    private bool chilling = false;
    private float chillLevel;
 
    void Start()
    {

    }

    void Update()
    {
        if (hmm)
        {
            Alarm();

        }
        if (chillLevel <= 0)
        {
            hmm = false;
            gameObject.transform.tag = "Mono";
        }
            
        if (chilling)
        {
            chillLevel -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D prey)
    {
        if (prey.gameObject.transform.tag == "detectable" && prey.GetType() == typeof(CircleCollider2D))
        {
            hmm = true;
            //prey.gameObject.GetComponent<Transform>().tag = "detectable";
            chillLevel = chill;
            Debug.Log(prey.gameObject.transform.tag);
        }
    }

    private void OnTriggerExit2D(Collider2D prey)
    {
        chilling = true;


    }

    private void Alarm()
    {
        gameObject.transform.tag = "Focus";
    }
}
