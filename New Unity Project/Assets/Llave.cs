using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name.Equals("Lucy") && Input.GetKeyDown(KeyCode.E) /* Agregar Raycast*/)
        {
            other.gameObject.GetComponent<MainChar>().hasKey = true;

        }
    }
}
