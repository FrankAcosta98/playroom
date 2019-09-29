
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
    public bool hasKey = false;
    private bool hasBox=false;
    private bool isMov=false;
    private bool isDown;
    private bool isUp; 
    private float vel;
    private float dashT;
    public float dashDur;
    private bool dash=true;
    private float dashChg;
    public float dashC;

    void Start()
    {
        isUp = false;
        Save = GameObject.FindGameObjectWithTag("Save").GetComponent<SavedFiles>();
        transform.position = Save.lastCheckPoint;
        instace = this;
        anim=this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        transform.gameObject.tag = "detectable";
        vel=velocidad;
        dashT=dashDur+1;
    }
    void Update()
    {
        mvmt.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (mvmt.x!=0||mvmt.y!=0)
            isMov=true;
        else
            isMov=false;
        /*
        if (Input.GetKeyDown(KeyCode.P))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            */
        if(Input.GetKeyDown(KeyCode.E))
            RaycastCheckUpdate();
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
        dashChg+=Time.deltaTime;
    }
    void FixedUpdate()
    {
        if(isDown==false)
            anim.SetFloat("Ver",mvmt.y);
        if(isUp == false)
            anim.SetFloat("Hor",mvmt.x);
        anim.SetFloat("Spd",mvmt.sqrMagnitude);
        anim.SetBool("hasTed",true);
        anim.SetBool("hasBox",hasBox);
        anim.SetBool("isMov",isMov);
        if (Input.GetKeyDown(KeyCode.Space)&&dash&&dashChg>=dashC){
            vel=velocidad*dashVel;
            dashChg=0.0f;}
        if(dashT>dashDur){
            dashT=0;
            dash=false;
            vel=velocidad;}
        if(dashT<dashDur)
            dash=true;
        dashT+=Time.fixedDeltaTime;
        rb.MovePosition(rb.position + mvmt.normalized * vel * Time.fixedDeltaTime);
    }

    public bool RaycastCheckUpdate()
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
            if(hit.distance<=lookAt.magnitude)
                return true;
            else
                return false;
        }else
            return false;
    }
    
}
