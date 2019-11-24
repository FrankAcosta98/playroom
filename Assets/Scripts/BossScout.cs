using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScout : MonoBehaviour
{

    [Header("Variables")]
    public float cooldown; //Tiempo para regresar a cooldown
    public float velocidad; //Velocidad de movimiento

    private float Aceleracion;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;


    void Start()
    {
        Aceleracion = 0f;
        CalcuateNewMovementVector();
    }

    void CalcuateNewMovementVector()
    {
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized; //Se elige un punto aleatorio dentro del rango
        movementPerSecond = movementDirection * velocidad; //Movimiento
    }


    void Update()
    {
        if (Time.time - Aceleracion > cooldown) //Se calcula si el tiempo es mayor al cooldown para moverse
        {
            Aceleracion = Time.time;
            CalcuateNewMovementVector();
        }
        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), transform.position.y + (movementPerSecond.y * Time.deltaTime));

    }
}