using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuar : MonoBehaviour
{
    public GameObject currentInterObj = null;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)){
            

            //other.GetComponent<Interactable>().Activate(true)
            Debug.Log("Roli Gay");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Interactuable" && UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
        {
            currentInterObj = other.gameObject;

        }
    }
}