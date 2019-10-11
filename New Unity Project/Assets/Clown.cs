using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clown : MonoBehaviour {

    [Header("Components")]
    public BoxCollider2D Range;
    public GameObject target = null;
    public GameObject head;
    public float headSpeed = 1f;
    private Rigidbody2D rb;

    [Header("Vectors")]
    public Vector2 initialSize = new Vector2(0.1f, 0.1f);
    public Vector2 maxSize = new Vector2(2f, 2f);
    private Vector2 currentSize;

    [Header("Variables")]
    public float growingSpeed = 0.5f;
    public float secondsToAttack = 2f;
    private float waitToAttack = 0f;
    private bool attacking;
    private bool attacked;
    private bool maxRange;

    void Start()
    {
        //Inicialisa Valores
        head = Instantiate(head, this.gameObject.transform.position, Quaternion.identity);
        rb = GetComponent<Rigidbody2D>();
        Range = GetComponent<BoxCollider2D>();
        initialSize = Range.size;
        currentSize = initialSize;
        attacking = false;
        maxRange = false;
        attacked = false;
    }

    void FixedUpdate()
    {
        if (attacking == false)
        {
            if (maxRange == false)
            {
                GrowRange();
            }
            else
            {
                ReduceRange();
            }

            if(attacked == true)
            {
                Retreat();
            }
        }
        else if(attacking == true)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (secondsToAttack > waitToAttack)
        {
            waitToAttack += Time.deltaTime;
            Debug.Log(waitToAttack);
        }
        else
        {
            head.transform.LookAt(target.transform.position);
            head.transform.position += head.gameObject.transform.forward * headSpeed * Time.deltaTime;
            if(Vector2.Distance(target.transform.position,head.transform.position) < 0.1)
            {
                waitToAttack = 0f;
                attacking = false;
                attacked = true;
            }
        }
    }

    private void Retreat()
    {
        if (secondsToAttack > waitToAttack)
        {
            waitToAttack += Time.deltaTime;
            Debug.Log(waitToAttack);
        }
        else
        {
            head.transform.LookAt(this.gameObject.transform.position);
            head.transform.position += head.gameObject.transform.forward * headSpeed * Time.deltaTime;
            if (Vector2.Distance(this.gameObject.transform.position, head.transform.position) < 0.1)
            {
                waitToAttack = 0f;
                attacking = false;
                attacked = false;
            }
        }
        
    }

    #region Range

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

    #endregion

    #region Detector

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
            Debug.Log("Vivimos en una sociedad");
            attacking = true;
        }
    }

    #endregion


    #region Ecorutine

    //private IEnumerator Attack()
    //{
    //    float lag = 0f;
    //    if(secondsToAttack < lag)
    //    {
    //    }

    //    yield return wait;
    //    //attacking = false; 
    //}

    //private IEnumerator SecondTime()
    //{
    //    yield return new WaitForSecondsRealtime(secondsToStay);
    //    secondTimePassed = true;
    //}

    #endregion


}
