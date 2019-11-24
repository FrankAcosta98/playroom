using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setup : MonoBehaviour
{
    public GameObject player;


    void Start()
    {
        //Cuando se inicie el código
        if (SceneManager.GetActiveScene().name == PlayerPrefs.GetString("Escena")) //Se determina si el nombre de la escena es el mismo que el guardado y en caso de que si..
        {
            player = GameObject.Find("Lucy"); //BUscamos al objeto de Lucy

            player.gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("ValorX"), PlayerPrefs.GetFloat("ValorY"), 0); //Le asignamos la posición guardada

            if (PlayerPrefs.GetString("LLave").Equals("True")) //Y se le da la llave si la tenía
            {
                player.GetComponent<MainChar>().hasKey = true;
            }

        }

   
    }
}
