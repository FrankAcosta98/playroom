using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private bool pickUpAllowed;
    public bool grabbed;
    public float distance=2f;
    public Collider2D grab;
    public Collider2D hitbox;
    public Collider2D AoE;
    public float throwforce;
    public float charge;
    public float batery;
    private float charging;
    private bool charged=false;
    void Start(){
        grabbed=false;
    }

    void Update () {

        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E)&&(grabbed==false)){
            PickUp();
        }
        else if (grabbed && Input.GetKeyDown(KeyCode.E)&&charged){
            Throw();
        }
        if(grabbed){
            this.transform.position=GameObject.Find("Lucy").transform.position;
        }
        if (Input.GetKeyDown(KeyCode.Q)&&grabbed){
            charging+=Time.deltaTime;
            Debug.Log(charging);
        }
        if (charging>=charge){
            charged=true;
        }
        if (transform.tag=="detectable")
            batery-=Time.deltaTime;
        if (batery<=0)
            Destroy(this);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name.Equals("Lucy")){
            pickUpAllowed = true;
        }        
    }
    private void OnTriggerExit2D(Collider2D collision){
        if (collision.gameObject.name.Equals("Lucy")){
            pickUpAllowed = false;
        }
    }
    private void PickUp(){
        grabbed=true;
        hitbox.enabled=false;
        this.gameObject.GetComponent<SpriteRenderer>().enabled=false;
    }
    private void Throw(){
        grabbed=false;
        pickUpAllowed=false;
        hitbox.enabled=true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled=false;
        this.transform.position=GameObject.Find("Lucy").transform.position*throwforce;
        transform.gameObject.tag = "detectable";
    }
}
