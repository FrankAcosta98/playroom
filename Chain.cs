using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chain : MonoBehaviour
{
public bool reset=false;
public GameObject og;
public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(reset)
            Vector2.MoveTowards(transform.position,og.transform.position,gameObject.GetComponentInParent<Clown>().headSpeed*Time.deltaTime);
    }
}
