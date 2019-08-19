using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    public float velocidad=5f;
    public Rigidbody2D rb;
    //public Animator animator;
    Vector2 mvmt;
    public float dashVel;
    private float dashTime;
    public float dashDur;
    public float coolDown;
    private float vel;
    private float cdTime;
    void Update()
    {
        /* 
        anim.SetFloat("Horizontal",mvmt.x);
        anim.SetFloat("Vertical",mvmt.y);
        anim.SetFloat("vel",mvmt.sqrMagnitude);
        */
        mvmt.Set(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
    }
    void FixedUpdate() {
        if(Input.GetKeyDown(KeyCode.Space) && Time.time>cdTime){
            if(dashTime<=0){
                dashTime=dashDur;
                vel=velocidad;
            }else{
                dashTime-=Time.deltaTime;
                vel=dashVel;
                cdTime=coolDown+Time.time;
            }
        }else{
            vel=velocidad;
        }
        rb.MovePosition(rb.position + mvmt * vel * Time.fixedDeltaTime);
    }
}
