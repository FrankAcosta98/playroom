using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockDoor : MonoBehaviour
{
    public bool IsLucy = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MainChar.instace.gameObject.GetComponent<MainChar>().hasKey == true && IsLucy == true)
        {
            SceneManager.LoadScene("Lvl2");
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals("Lucy") /*&& other.gameObject */ /* Agregar Raycast*/)
        {
            IsLucy = true;
        }
    }
}
