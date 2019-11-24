using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    //El codigo se utiliza para determinar si el Boss mata a Lucy

    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D prey)
    {
        if (prey.gameObject.transform.tag == "Focus" && prey.gameObject.name == "Lucy") //Si se mantiene sobre Lucy en estado de Focus..
        {
            Debug.Log("Ya te cargo el payaso");
            //Destroy(prey.gameObject); //Se podrá destruir el objeto de Lucy
            //Recordar agregar animador
        }
    }
}