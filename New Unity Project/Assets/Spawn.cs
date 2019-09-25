using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject spawn;
    public float spawnRate;
    public float nextSpawn;
    List<Vector2> usedValues = new List<Vector2>();
    public Collider2D collider;
    Vector2 position;


    private void Start()
    {

    }

    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            Instantiate(spawn, RandomPosition(), Quaternion.identity);
        }
    }

    private Vector2 RandomPosition()
    {
        
        while (usedValues.Contains(position)){
            position.Set(Random.Range(collider.bounds.min.x, collider.bounds.max.x), Random.Range(collider.bounds.min.y, collider.bounds.max.y));
        }
        usedValues.Add(position);
        return position;
    }
}
