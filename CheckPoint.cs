using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    //El código sirve 


    //PlayerPrefs checkpoint;
    private bool SaveAllowed;
    GameObject player;
    private bool saved;

    void Start()
    {
        saved = false;
        SaveAllowed = false;
    }

    void Update()
    {
        //Si el jugador interactua
        if (SaveAllowed == true && Input.GetKeyDown(KeyCode.E))
        {
            if (player.gameObject.GetComponent<MainChar>().hasKey == true)
            {
                PlayerPrefs.SetFloat("ValorX", player.transform.position.x);
                PlayerPrefs.SetFloat("ValorY", player.transform.position.y);
                PlayerPrefs.SetString("Llave", "True");
                saved = true;
            }
            else if (player.gameObject.GetComponent<MainChar>().hasKey == false)
            {
            PlayerPrefs.SetFloat("ValorX", player.transform.position.x);
            PlayerPrefs.SetFloat("ValorY", player.transform.position.y);
                PlayerPrefs.SetString("Llave", "False");
            saved = true;
    }


        } 

        if (saved == true && Input.GetKeyDown(KeyCode.O))
        {
            player.transform.position = new Vector2(PlayerPrefs.GetFloat("ValorX"), PlayerPrefs.GetFloat("ValorY"));
            if (PlayerPrefs.GetString("Llave").Equals("True"))
            {
                player.gameObject.GetComponent<MainChar>().hasKey = true;
            } else
            {
                player.gameObject.GetComponent<MainChar>().hasKey = false;
            }
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