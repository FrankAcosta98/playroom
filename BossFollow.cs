using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFollow : MonoBehaviour {

    [Header("Variables")]
    public float velocidad;

    private Transform target;

    void Start () {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

	void Update () {
        transform.position = Vector2.MoveTowards(transform.position, target.position, velocidad * Time.deltaTime);
		
	}

}
