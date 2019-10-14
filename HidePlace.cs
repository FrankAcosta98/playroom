using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePlace : MonoBehaviour
{
    private bool hiding = false;
    private bool hided = false;
    
    private void Start() {
    }
    void Update()
    {
        if (hiding == true && Input.GetKeyDown(KeyCode.E))
        {
            MainChar.instace.GetComponent<BoxCollider2D>().enabled = false;
            MainChar.instace.GetComponentInChildren<CircleCollider2D>().enabled=false;
            MainChar.instace.GetComponent<SpriteRenderer>().enabled = false;
            MainChar.instace.enabled=false;
            hided = true;
            //desactivar luz
        }
        else if (Input.GetKeyDown(KeyCode.E) && hided == true)
        {
            MainChar.instace.GetComponent<BoxCollider2D>().enabled = true;
            MainChar.instace.GetComponentInChildren<CircleCollider2D>().enabled=false;
            MainChar.instace.GetComponent<SpriteRenderer>().enabled = true;
            MainChar.instace.enabled=true;
            //desactivar luz
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy")){
            if(other.gameObject.GetComponent<MainChar>().RaycastCheckUpdate()){
                hiding = true;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        hiding=false;
    }
}

