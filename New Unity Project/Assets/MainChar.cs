using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainChar : MonoBehaviour
{
    public float velocidad;
    public Rigidbody2D rb;
    //public Animator anim;
    Vector2 mvmt;
    private Vector2 lookAt;
    public float dashVel;
    private float dashTime;
    public float dashDur;
    public float coolDown;
    private float vel;
    private float cdTime;

    public bool hasKey;
    //public float OffsetColider=2.0f;
    //public float Reach = 2.1f;
    void Start()
    {
        //anim=this.GetComponent<Animator>();
        rb=this.GetComponent<Rigidbody2D>();
        rb.constraints=RigidbodyConstraints2D.FreezeRotation;
    }
    void Update()
    {
        /*animator
        anim.SetFloat("Horizontal",mvmt.x);
        anim.SetFloat("Vertical",mvmt.y);
        */
        mvmt.Set(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
        
    }
    void FixedUpdate() {
        //RaycastCheckUpdate();
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
        rb.MovePosition(rb.position + mvmt.normalized * vel * Time.fixedDeltaTime);
    }
    /* ray-cast para saber si esta volteando si sirve pero falta el animator bien echo -Dios
    
        public RaycastHit2D CheckRaycast(Vector2 dir)
    {
        float dirOffset = OffsetColider * (dir.x > 0 ? 1 : -1);

        Vector2 org = new Vector2(transform.position.x + dirOffset, transform.position.y);

        return Physics2D.Raycast(org, dir, Reach);
    }
    private bool RaycastCheckUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector2 lookAt = new Vector2(anim.GetFloat("Horizontal"), anim.GetFloat("Vertical"));
            RaycastHit2D hit = CheckRaycast(lookAt);
            
            if (hit.collider)
            {
                Debug.Log("Hit-" + hit.collider.name);

                Debug.DrawRay(transform.position, hit.point, Color.red, 3f);
            }

            return true;
        }
        else
        {
            return false;
        }
    }*/
}

