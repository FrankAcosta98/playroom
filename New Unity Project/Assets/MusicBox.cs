using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private bool pickUpAllowed;
    private bool grabbed;
    public float distance=2f;
    public Collider2D grab;
    public Collider2D hitbox;
    public Collider2D AoE;
    public float throwforce;
    void Start(){
        grabbed=false;
    }

    void Update () {

        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E)&&(grabbed==false)){
            PickUp();
        }
        if(grabbed){
            this.transform.position=GameObject.Find("Lucy").transform.position;
        }
        if (grabbed && Input.GetKeyDown(KeyCode.E)){
            Throw();
        }
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
        Debug.Log("grabbed");
    }
    private void Throw(){
        grabbed=false;
        this.transform.position=GameObject.Find("Lucy").transform.position*throwforce;
    }
}
