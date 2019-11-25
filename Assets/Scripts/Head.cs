using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [HideInInspector]//no se toca
    public bool IsLucy=false;
    [HideInInspector]
    public bool IsBox=false;
    
    [Header("Components")]
    public CircleCollider2D trigger; //trigger
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsLucy)
            anim.SetBool("IsLucy",IsLucy);
        else if(IsBox)
            anim.SetBool("IsBox",IsBox);
        if(IsLucy==false&&IsBox==false){
            anim.SetBool("IsBox",IsBox);
            anim.SetBool("IsLucy",IsLucy);
        }
    }
    void OnTriggerEnter2D(Collider2D prey)
    {
        if (prey.gameObject.transform.tag == "Focus" && prey.gameObject.name!="Lucy")
        {
            IsBox = true;
            Destroy(prey.gameObject);
        }
        
        else if (prey.gameObject.name == "Lucy") //Si se mantiene sobre Lucy en estado de Focus..
        {
            IsLucy=true;
            //cambio de escena a game ova
        }
        
    }
    void OnTriggerExit2D(Collider2D other)
    {
        IsBox=IsLucy=false;
    }
}
