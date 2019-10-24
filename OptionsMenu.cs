using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {


    [Header("Componentes")]
    //Aqui se pone el canvas de OptionsMenu
    public GameObject optionsMenu;
    //Aqui se pone el audio Mixer principal
    public AudioMixer audioMixer;
    //Aqui se pone el ResolutionDropdown
    public Dropdown resolutionDropdown;
    //Aqui se guarda una lista de las resoluciones
    Resolution[] resolutions;

    void Start()
    {
        //Se genera las resoluciones
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        //Se crea un List de strings
        List<string> options = new List<string>();

        //Se inisialisa la variable que va a guarda la posicion de la resolucion
        int currentResolutionIndex = 0;

        //Se generan y se guardan todas las resoluciones
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        //Se agregan las opciones al DropDown para mostrarlo en pantalla
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    //Pone la resolucion en el juego basado en la establecida por el dropDown
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
    }

    //Recibe valores de la VolumeSlider y los pasa al audio mixer
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("volume",volume);
    }

    //Checa si FullScreenBotton es true, si es true pone la pantalla completa
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    //Este proceso es para que al entrar seleccione automaticamente el boton play y puedas navegar sin usar mouse
    public void SetButton()
    {
        GameObject.Find("MenuButton").GetComponent<Button>().Select();
    }

    //Saca del programa
    public void Quit()
    {
        Debug.Log("Quit");
        //Aplication.Quit();
    }
}
