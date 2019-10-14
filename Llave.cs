using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour
{
    private bool usable = false;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && usable == true)
        {
            MainChar.instace.GetComponent<MainChar>().hasKey = true;
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy")&&other.gameObject.GetComponent<MainChar>().RaycastCheckUpdate())
        {
            usable = true;
        }
    }

}
