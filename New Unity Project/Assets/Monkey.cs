using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monkey : MonoBehaviour
{

    public Collider2D AoE;
    private bool hmm = false;
    private bool chilling = false;
    public float chill;
    private float chillLevel;

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
        if (prey.gameObject.transform.tag == "detectable")
        {
            hmm = true;
            //prey.gameObject.GetComponent<Transform>().tag = "detectable";
            chillLevel = chill;
        }
    }

    private void OnTriggerExit2D(Collider2D prey)
    {
        chilling = true;
    }

    private void Alarm()
    {
        gameObject.transform.tag = "focus";
        GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss1>().hmm = true;
    }
}
