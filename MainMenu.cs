using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor.SceneManagement;

public class MainMenu : MonoBehaviour {

    
    [Header("component")]
    public GameObject mainMenu; //Agregas el Canvas de Nombre MainMenu

    //Cuando Empiece el programa selecciona el boton PlayButton
    private void Awake()
    {
        GameObject.Find("PlayButton").GetComponent<Button>().Select();
    }

    //Carga la siguiente escena
    public void Play()
    {
        Debug.Log("NewGame");
        //Carga la siguiente escena
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Saca del programa
    public void Quit()
    {
        Debug.Log("Quit");
        //Aplication.Quit();
    }

    //Este proceso es para que al entrar seleccione automaticamente el boton play y puedas navegar sin usar mouse;
    public void SetButton()
    {
        GameObject.Find("PlayButton").GetComponent<Button>().Select();
    }
}
