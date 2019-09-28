
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainChar : MonoBehaviour
{
    public static MainChar instace;
    private SavedFiles Save;
    public Collider2D HitBox;
    public float velocidad;
    public Rigidbody2D rb;
    Vector2 mvmt;
    Vector2 lookAt;
    public Animator anim;
    public float dashVel;
    private float dashTime;
    public float dashDur;
    public float coolDown;
    private float vel;
    private float cdTime;
    public bool hasKey = false;
    private bool hasBox=false;
    private bool hasTed=false;
    private bool isMov=false;
    private bool isDown;
    private bool isUp; 

    void Start()
    {
        isUp = false;
        Save = GameObject.FindGameObjectWithTag("Save").GetComponent<SavedFiles>();
        transform.position = Save.lastCheckPoint;
        instace = this;
        //anim=this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.gameObject.tag = "detectable";
    }
    void Update()
    {
        
        mvmt.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (mvmt.x!=0||mvmt.y!=0)
            isMov=true;
        else
            isMov=false;
        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if(this.GetComponentInChildren<Transform>().tag=="TedEnable")
            hasTed=true;
        if(GameObject.Find("Box").GetComponent<MusicBox>().grabbed==true)
            hasBox=true;
        if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") > 0){
            isUp = true;
            anim.SetFloat("Hor",0.0f);
        } else 
            isUp = false;
        if(Input.GetAxisRaw("Horizontal") != 0 && Input.GetAxisRaw("Vertical") < 0){
            isDown = true;
            anim.SetFloat("Ver",0.0f);
        } else 
            isDown = false;
    }
    void FixedUpdate()
    {

        if(isDown==false)
        anim.SetFloat("Ver",mvmt.y);
        if(isUp == false)
            anim.SetFloat("Hor",mvmt.x);
        
        anim.SetFloat("Spd",mvmt.sqrMagnitude);
        anim.SetBool("hasTed",hasTed);
        anim.SetBool("hasBox",hasBox);
        anim.SetBool("isMov",isMov);
        if (Input.GetKeyDown(KeyCode.E))
            RaycastCheckUpdate();
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > cdTime)
        {
            if (dashTime <= 0)
            {
                dashTime = dashDur;
                vel = velocidad;
            }
            else
            {
                dashTime -= Time.deltaTime;
                vel = dashVel;
                cdTime = coolDown + Time.time;
            }
        }
        else
        {
            vel = velocidad;
        }
        rb.MovePosition(rb.position + mvmt.normalized * vel * Time.fixedDeltaTime);
    }

    private void RaycastCheckUpdate()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsTag("f"))
            lookAt.Set(0,-3);
        else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("l"))
            lookAt.Set(-3,0);
        else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("r"))
            lookAt.Set(3,0);
        else if (anim.GetCurrentAnimatorStateInfo(0).IsTag("b"))
            lookAt.Set(0,3);
        RaycastHit2D hit = Physics2D.Raycast(transform.position,lookAt);
        if (hit.collider)
        {
            Debug.Log("Hit-" + hit.collider.name);
        }
    }
    
}
