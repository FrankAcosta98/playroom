
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

    void Start()
    {
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
    }
    void FixedUpdate()
    {
        anim.SetFloat("Hor",mvmt.x);
        anim.SetFloat("Ver",mvmt.y);
        anim.SetFloat("Spd",mvmt.sqrMagnitude);
        anim.SetBool("hasTed",hasTed);
        anim.SetBool("hasBox",hasBox);
        anim.SetBool("isMov",isMov);
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
