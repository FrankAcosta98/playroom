using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [HideInInspector]//no se toca
    public bool IsLucy=false;
    [HideInInspector]
    public bool ISBox=false;

    [Header("Components")]
    public CircleCollider2D trigger; //trigger
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim.SetFloat("spd",this.GetComponentInParent<Clown>().headSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        if(IsLucy)
            anim.SetBool("IsLucy",IsLucy);
        else if(ISBox)
            anim.SetBool("ISBox",ISBox);
    }
    void OnTriggerEnter2D(Collider prey)
    {
        if (prey.gameObject.name == "Lucy") //Si se mantiene sobre Lucy en estado de Focus..
        {
            Debug.Log("Ya te cargo el payaso");
            IsLucy=true;
            //Destroy(prey.gameObject); //Se podrá destruir el objeto de Lucy
            //Recordar agregar animador
        }
        else if (prey.gameObject.transform.tag == "detectable"){
            ISBox=true;
            Destroy(prey.gameObject);
            }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        IsBox=IsLucy=false;
    }
}
