using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnTriggerStay2D(Collider2D prey)
    {
        if (prey.gameObject.transform.tag == "Focus" &&prey.gameObject.name=="Lucy")
        {
            Debug.Log("Ya te cargo el payaso");
            //Destroy(prey.gameObject);
        }
    }
}
