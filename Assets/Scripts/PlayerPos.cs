using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour
{

    private SavedFiles Save; //Usamos la clase SavedFiles
    
    void Start()
    {
        Save = GameObject.FindGameObjectWithTag("Save").GetComponent<SavedFiles>(); //El objeto con el tag correspondiente se guardará como el atributo Save...
        transform.position = Save.lastCheckPoint; //Y se guardará su ultima checkpoint
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //Cuando se presione "P" se carga la ultima posición
        }
    }
}