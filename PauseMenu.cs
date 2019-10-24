using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {

    [Header("component")]
    //Aqui va el Canvas de Pause
    public GameObject pauseMenu;

    [Header("variables")]
    //La condicion que revisa el estado del juego
    public static bool gameIsPause = false;

    void Update()
    {
        //Cuando oprimas Esc, el pausa se activa o se apaga dependiendo de si ya estaba o no estaba
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPause)
            {
                Resume();
            }
            else
            {
                Stop();
            }
        }
    }

    //Hace que pausa se desactive y regresa el tiempo normal
    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPause = false;
    }

    //Activa pausa y regresa el tiempo a 0
    public void Stop()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPause = true;
        //Selecciona el boton de ResumeButton cuando prende
        GameObject.Find("ResumeButton").GetComponent<Button>().Select();
    }

    //Saca del programa
    public void Quit()
    {
        Debug.Log("Quit");
        //Aplication.Quit();
    }
}
