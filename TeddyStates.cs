using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class TeddyStates : MonoBehaviour
{

    CircleCollider2D col; //El Collider que representa la luz y el área de detección por los monstruos
    Light2D lt;
    public float sizeRad = 2f; //Tamaño del collider
    bool IsLightOn = false; //Determina si la luz está prendida o apagada
    public bool Holded = false; //Determina si Teddy está en estado normal o levantado
    public float RadMultWithLight = 2.8f; //Radio de collider cuando la luz está prendida
    public float VelocidadLenta = 1.98f; //Velocidad a la que Lucy se mueve cuando tiene a Teddy levantado
    public float RadMultWithHold = 6.44f; //Radio de collider cuando Teddy está levantado
    private float tmpv; //Velocidad original de Lucy, se guarda para que cuando cambie pueda regresar a su estado original

    void Start()
    {
        //Se inician los componentes necesarios
        col = GetComponent<CircleCollider2D>();
        lt=GetComponent<Light2D>();
        col.radius = sizeRad;
        lt.pointLightOuterRadius=0.68f;
        tmpv = this.GetComponentInParent<MainChar>().velocidad;
        transform.gameObject.tag = "detectable"; //Lucy se establece como detectable
    }

    void Update()
    {
        
        if (this.GetComponentInParent<MainChar>().hasBox == false) //Si Lucy no tiene una caja...
        {
            
            if (Input.GetKeyDown(KeyCode.F) && (IsLightOn == false)) //Si se apreta "F" y la luz está apagada...
            {
                col.radius = RadMultWithLight; //Se aumenta el radio al número definido
                lt.pointLightOuterRadius=1.36f;
                IsLightOn = true; //Se establece que la luz está prendida
            }
            else if (Input.GetKeyDown(KeyCode.F) && (IsLightOn == true)) //Si se apreta "F" y la luz está prendida...
            {
                col.radius = sizeRad; //El radio vuelve al tamaño original
                lt.pointLightOuterRadius=0.68f;
                IsLightOn = false; //Se establece que la luz está apagada
            }

            if (Input.GetKey(KeyCode.LeftShift) && IsLightOn == true && Holded == false) //Si se mantiene apretado LeftShift, la luz está prendida y no se está levantando el Teddy
            {
                col.radius = RadMultWithHold; //El radio del collider crece al número definido
                lt.pointLightOuterRadius=2.72f;
                Holded = true; //Se establece que Teddy está levantado
                this.GetComponentInParent<MainChar>().velocidad = VelocidadLenta; //La velocidad de Lucy se reduce a lo establecido
                this.GetComponentInParent<Animator>().SetBool("High", true); //El Animator se activa para la animación de levantar a Teddy
            }
            else if (Input.GetKeyUp(KeyCode.LeftShift) && Holded == true) //Si se deja de apretar LeftShift y se está levantando a Teddy...
            {
                col.radius = RadMultWithLight; //El radio regresa al número con la luz prendida
                lt.pointLightOuterRadius=1.36f;
                Holded = false; //Se establece que no se está levantando a Teddy
                this.GetComponentInParent<Animator>().SetBool("High", false); //El Animator ya no realiza la animación de Teddy levantado
                this.GetComponentInParent<MainChar>().velocidad = tmpv; //La velocidad de Lucy regresa a la normal
            }
        }
        else //En otro caso...
        {
            col.radius = sizeRad; //El radio se mantiene al definido
            lt.pointLightOuterRadius=0.68f;
            IsLightOn = false; //La luz se mantiene apagada mientras no se aprete "F"
        }
    }
}
