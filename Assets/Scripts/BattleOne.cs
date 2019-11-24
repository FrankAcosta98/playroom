using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
public class BattleOne : MonoBehaviour
{
    public BoxCollider2D Collider;
    private bool bossCreated;
    public GameObject theBoss;
    
    public Light2D lt;
    private bool lightFinished;
    public float dim=0.5f;
    public float bri=4f;
    // Start is called before the first frame update
    void Start()
    {
        bossCreated = false;
        
        lt.intensity=dim;
        lightFinished = false;
        theBoss.gameObject.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (bossCreated == true && lightFinished==false)
        {
            StartCoroutine(Lightshow());
        }
    }

    IEnumerator Lightshow()
    {
        lightFinished = true;
        lt.intensity = bri;
        yield return new WaitForSecondsRealtime(0.3f);
        lt.intensity = dim;
        yield return new WaitForSecondsRealtime(0.5f);
        lt.intensity = bri;
        yield return new WaitForSecondsRealtime(0.3f);
        lt.intensity = dim;
        yield return new WaitForSecondsRealtime(0.6f);
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.name == "Lucy") && other.gameObject.GetComponent<MainChar>().hasKey == true && bossCreated == false)
        {
            theBoss.gameObject.SetActive(true);
            bossCreated = true;
            //Instantiate(theBoss, pos, Quaternion.identity);
        }
    }
}
