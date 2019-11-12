using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleOne : MonoBehaviour
{
    public BoxCollider2D Collider;
    private bool bossCreated;
    public GameObject theBoss;
    private Vector3 pos;
    public GameObject posObj;
    public Light light1;
    private bool lightFinished;
    public GameObject bunny;
    // Start is called before the first frame update
    void Start()
    {
        bossCreated = false;
        pos = posObj.transform.position;
        light1.GetComponent<Light>().enabled = false;
        lightFinished = false;
        bunny.gameObject.SetActive(false);
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
        light1.GetComponent<Light>().enabled = true;
        yield return new WaitForSecondsRealtime(0.3f);
        light1.GetComponent<Light>().enabled = false;
        yield return new WaitForSecondsRealtime(0.5f);
        light1.GetComponent<Light>().enabled = true;
        yield return new WaitForSecondsRealtime(0.3f);
        light1.GetComponent<Light>().enabled = false;
        yield return new WaitForSecondsRealtime(0.6f);
        

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.name == "Lucy") && other.gameObject.GetComponent<MainChar>().hasKey == true && bossCreated == false)
        {
            bunny.gameObject.SetActive(true);
            bossCreated = true;
            //Instantiate(theBoss, pos, Quaternion.identity);
        }
    }
}
