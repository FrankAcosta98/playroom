using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatern : MonoBehaviour {

    [Header("Components")]
    public Transform[] moveSpots;

    [Header("Variables")]
    public float velocidad;
    public float cooldown;
    
    private float wait;
    private int randomSpot;


    void Start()
    {
        wait = cooldown;
        randomSpot = Random.Range(0, moveSpots.Length);
    }


	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, velocidad * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if (wait <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                wait = cooldown;
            }
            else
            {
                wait -= Time.deltaTime;
            }
        }
	}

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy"))
        {
            Destroy(other.gameObject);
        }
    }
    
}
