using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeddyStates : MonoBehaviour
{

    CircleCollider2D col;
    public float sizeRad;
    bool IsLightOn;
    bool Holded;
    public float RadMultWithLight = 0.2f;
    public float RadMultWithHold = 0.5f;
    void Start()
    {
        //col = GetComponent<CircleCollider2D>();
        col = GetComponent<CircleCollider2D>();
        col.radius = sizeRad;

        IsLightOn = false;
        Holded = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && (IsLightOn == false))
        {
            col.radius = RadMultWithLight;
            Debug.Log("Luz prendida");
            IsLightOn = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && (IsLightOn == true))
        {
            col.radius = sizeRad;
            Debug.Log("Luz apagada");
            IsLightOn = false;
        }

        if (Input.GetKey(KeyCode.LeftShift) && IsLightOn == true && Holded == false)
        {
            col.radius = RadMultWithHold;
            Debug.Log("Luz elevada");
            Holded = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) && Holded == true)
        {
            col.radius = RadMultWithLight;
            Debug.Log("Luz normal");
            Holded = false;
        }
    }
}
