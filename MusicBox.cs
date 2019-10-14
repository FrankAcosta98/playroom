using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicBox : MonoBehaviour
{
    private bool pickUpAllowed;
    public bool grabbed;
    public Rigidbody2D rb;
    public Collider2D grab;
    public Collider2D hitbox;
    public Collider2D AoE;
    public float charge=0.4f;
    public float batery=5f;
    private float charging;
    private bool charged=false;
    private float throwD=5f;
    private bool move=false;
    private Vector2 dirAbs;
    public float movT=0.3f;
    private float tmpT=0f;
    Vector2 dir;
    void Start(){
        grabbed=false;
        AoE.enabled=false;
        pickUpAllowed=false;
        rb.bodyType=RigidbodyType2D.Static;
        dirAbs.Set(Mathf.Abs(dir.x),Mathf.Abs(dir.y));
    }

    void Update () {
        if(rb.bodyType==RigidbodyType2D.Kinematic){
            transform.rotation.Set(0,0,0,0);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E)&&(grabbed==false)){
            PickUp();
            MainChar.instace.hasBox=true;
        }
        else if (grabbed && Input.GetKeyDown(KeyCode.E)&&charged){
            Throw();
            MainChar.instace.hasBox=false;
            AoE.enabled=true;
        }
        
        if (Input.GetKeyDown(KeyCode.Q)&&grabbed){
            charging+=Time.deltaTime;
        }
        if (charging>=charge){
            charged=true;
            Debug.Log("cargado");
        }
        if (transform.tag=="detectable")
            batery-=Time.deltaTime;
        if (batery<=0)
            Destroy(gameObject);
        if(grabbed){
            if (MainChar.instace.GetComponent<MainChar>().anim.GetCurrentAnimatorStateInfo(0).IsTag("f"))
                dir.Set(0,-throwD);
            else if (MainChar.instace.GetComponent<MainChar>().anim.GetCurrentAnimatorStateInfo(0).IsTag("l"))
                dir.Set(-throwD,0);
            else if (MainChar.instace.GetComponent<MainChar>().anim.GetCurrentAnimatorStateInfo(0).IsTag("r"))
                dir.Set(throwD,0);
            else if (MainChar.instace.GetComponent<MainChar>().anim.GetCurrentAnimatorStateInfo(0).IsTag("b"))
                dir.Set(0,throwD);
                }
    }
    private void FixedUpdate() {
        if(move){
            if(tmpT<movT){
            rb.MovePosition(rb.position+dir*Time.fixedDeltaTime);
            tmpT+=Time.deltaTime;
            }
        }
        if(tmpT>=movT){
            tmpT=0.0f;
            move=false;
        }
        if(grabbed){
            this.transform.position=GameObject.Find("Lucy").transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.gameObject.name.Equals("Lucy")&&MainChar.instace.RaycastCheckUpdate()){
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
        rb.bodyType=RigidbodyType2D.Kinematic;
        pickUpAllowed=false;
        hitbox.enabled=true;
        this.gameObject.GetComponent<SpriteRenderer>().enabled=true;
        move=true;
        transform.gameObject.tag = "detectable";
    }
}
