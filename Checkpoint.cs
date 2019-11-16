using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    private bool SaveAllowed; //Determina si es posible guardar los datos de la partida
    GameObject player; //Lucy
    Scene scene; //Escena actual, se usa su nombre
    public Collider2D box; //Collider de colision
    public Collider2D area; //Collider de trigger
    


    void Start()
    {
        scene = SceneManager.GetActiveScene();
        SaveAllowed = false;
    }

    void Update()
    {
        //Si el jugador interactua
        if (SaveAllowed == true && Input.GetKeyDown(KeyCode.E)) {
            //Se guarda el valor de la posicion x y y de Lucy en ese momento
            PlayerPrefs.SetString("Guardado", "True");
            PlayerPrefs.SetFloat("ValorX", player.transform.position.x);
            PlayerPrefs.SetFloat("ValorY", player.transform.position.y);
            //Se determina si Lucy tiene la llave
            if (player.gameObject.GetComponent<MainChar>().hasKey == true)
            {
                PlayerPrefs.SetString("Llave", "True");
            }
            //Se guarda el nombre de la escena para usarlo más tarde al momento de recargar
            PlayerPrefs.SetString("Escena", SceneManager.GetActiveScene().name);
            //Comprobación de guardado
            Debug.Log("Ya lo guarde we");
            Debug.Log(PlayerPrefs.GetFloat("ValorX").ToString());
            Debug.Log(PlayerPrefs.GetFloat("ValorY").ToString());

        } 

    }

    private void OnTriggerEnter2D(Collider2D collision) //Cuando se entre en una collision...
    {
        if (collision.gameObject.name.Equals("Lucy") && MainChar.instace.RaycastCheckUpdate()) //Si es Lucy y el Raycast apunta a la caja de guardado...
        {
            SaveAllowed = true; //Se permite levantarlo
            player = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision) //Cuando Lucy sale de la colision...
    {
        if (collision.gameObject.name.Equals("Lucy"))
        {
            SaveAllowed = false; //No se permite levantar
        }
    }
    

}