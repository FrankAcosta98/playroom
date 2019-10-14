using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockDoor : MonoBehaviour
{
    private bool usable = false;
    public string nivel; //"nivel" determina a cual nivel cambiará la escena

    void Update()
    {
        //Si Lucy tiene la llave y es la puerta es usable...
        if (MainChar.instace.GetComponent<MainChar>().hasKey && usable)
        {
            //La escena cambia a la asignada en el componente
            SceneManager.LoadScene(nivel);
            Debug.Log("siguiente escena");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Determina si Lucy esta colisionando con la puerta y si está viendo hacia la puerta
        if (other.gameObject.name.Equals("Lucy") && other.gameObject.GetComponent<MainChar>().RaycastCheckUpdate())
        {
            //Se determina que la puerta es usable
            usable = true;
        }
    }
}
