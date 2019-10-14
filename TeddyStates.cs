using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyStates : MonoBehaviour
{

    CircleCollider2D col;
    public float sizeRad=1;
    bool IsLightOn=false;
    public bool Holded=false;
    public float RadMultWithLight = 2f;
    public float VelocidadLenta=1.98f;
    public float RadMultWithHold = 4f;
    private float tmpv;
    void Start()
    {
        col = GetComponent<CircleCollider2D>();
        col.radius = sizeRad;
        tmpv=this.GetComponentInParent<MainChar>().velocidad;
        transform.position=GetComponentInParent<MainChar>().transform.position;
        transform.gameObject.tag = "detectable";
    }

    void Update()
    {
        if(this.GetComponentInParent<MainChar>().hasBox==false){
            if (Input.GetKeyDown(KeyCode.F) && (IsLightOn == false))
            {
                col.radius = RadMultWithLight;
                Debug.Log("Luz prendida");
                IsLightOn = true;
            }
            else if (Input.GetKeyDown(KeyCode.F) && (IsLightOn == true))
            {
                col.radius = sizeRad;
                Debug.Log("Luz apagada");
                IsLightOn = false;
            }

            if (Input.GetKey(KeyCode.LeftShift) && IsLightOn == true && Holded == false)
            {
                col.radius = RadMultWithHold;
                Debug.Log("Luz elevada");
                Holded = true;
                this.GetComponentInParent<MainChar>().velocidad=VelocidadLenta;
                this.GetComponentInParent<Animator>().SetBool("High",true);
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) && Holded == true)
            {
                col.radius = RadMultWithLight;
                Debug.Log("Luz normal");
                Holded = false;
                this.GetComponentInParent<Animator>().SetBool("High",false);
                this.GetComponentInParent<MainChar>().velocidad=tmpv;
            }
        }else{
            col.radius = sizeRad;
            Debug.Log("Luz apagada");
            IsLightOn = false;
        }
    }
}
