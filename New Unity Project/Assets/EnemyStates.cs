using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStates : MonoBehaviour
{
    CircleCollider2D coli;
    public float rad;
    public GameObject target;
    public float moveSpeed = 15;
    public float rotationSpeed = 5;
    private Rigidbody2D rb;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coli = GetComponent<CircleCollider2D>();
        coli.radius = rad;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
           
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);
            
            transform.position += transform.forward * Time.deltaTime * moveSpeed;

            

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Lucy"))
        {
            target = collision.gameObject;
            Debug.Log("Persiguiendo algo");

        } else if (collision.gameObject.tag.Equals("MusicBox"))
        {
            target = collision.gameObject;
            Debug.Log("Persiguiendo algo");
        }
    }
    
    

    private void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
        Debug.Log("Ya no persigo a nadie");
    }
}
