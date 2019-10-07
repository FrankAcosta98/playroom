using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClownGrow : MonoBehaviour {

    [Header("Components")]
    public BoxCollider2D Range;
    public GameObject target = null;

    [Header("Vectors")]
    public Vector2 initialSize = new Vector2(0.1f,0.1f);
    public Vector2 maxSize = new Vector2(2f,2f);
    private Vector2 currentSize;

    [Header("Variables")]
    public float growingSpeed = 0.5f;
    private bool attacking;
    private bool maxRange;

    void Start()
    {
        //Inicialisa Valores
        Range = GetComponent<BoxCollider2D>();
        initialSize = Range.size;
        currentSize = initialSize;
        attacking = false;
        maxRange = false;
    }

    void FixedUpdate()
    {
        if (attacking == false)
        {
            if(maxRange == false)
            {
                GrowRange();
            }
            else
            {
                ReduceRange();
            }
        }
        else
        {
            ReduceRange();
        }
    }

    private void GrowRange()
    {
        if (currentSize.x < maxSize.x)
        {
            currentSize += Vector2.one * growingSpeed * Time.deltaTime;

            if (currentSize.x > maxSize.x)
            {
                currentSize = maxSize;
                maxRange = true;
            }
            Range.size = currentSize;
        }
    }

    private void ReduceRange()
    {
        if (currentSize.x > initialSize.x)
        {
            currentSize -= Vector2.one * growingSpeed * Time.deltaTime;

            if (currentSize.x < initialSize.x)
            {
                currentSize = initialSize;
                maxRange = false;
            }
            Range.size = currentSize;
        }
    }


    private void OnTriggerEnter2D(Collider2D prey)
    {
        if (prey.gameObject.tag.Equals("MusicBox") && attacking == false)
        {
            target = prey.gameObject;
            Debug.Log("CAJAAAAAA");
            attacking = true;
        }

        if (prey.gameObject.transform.tag == "detectable" && attacking == false)
        {
            target = prey.gameObject;
            Debug.Log("SULLYVAN DAME A LA NIÑA");
            attacking = true;
        }
    }
}
