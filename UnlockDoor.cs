using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockDoor : MonoBehaviour
{   
    private bool usable=false;
    void Update()
    {
        if (MainChar.instace.GetComponent<MainChar>().hasKey&&usable)
        {
            //aqui se cambia la escena
            //SceneManager.LoadScene("Lvl2");
            Debug.Log("siguiente escena");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy")&&other.gameObject.GetComponent<MainChar>().RaycastCheckUpdate())
        {
            usable=true;
        }
    }
}
