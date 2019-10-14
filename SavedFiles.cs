using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedFiles : MonoBehaviour
{

    private static SavedFiles instance; //Sirve para guardar la información del jugador
    public Vector2 lastCheckPoint; //Es la posición donde se guardó el juego por ultima vez

    void Awake()
    {
        if (instance == null) //Si no existe una instancia...
        {
            instance = this; //Instancia se vuelve "this", es decir, el objeto al que está asignado el código
            DontDestroyOnLoad(instance); //No se destruirá el objeto al cargar el juego o una escena
        }
        else //Si existe una instancia
        {
            Destroy(gameObject); //Se destruye el objeto
        }

    }

    void Start()
    {

    }

    void Update()
    {

    }
}