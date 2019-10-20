using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint1 : MonoBehaviour
{

    private SavedFiles Save;
    private bool usable = false;
    void Start()
    {
        Save = GameObject.FindGameObjectWithTag("Save").GetComponent<SavedFiles>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && usable)
        {
            Save.lastCheckPoint = transform.position;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy") && other.gameObject.GetComponent<MainChar>().RaycastCheckUpdate())
        {
            usable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        usable = false;
    }
}