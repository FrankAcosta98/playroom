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
    public Collider2D area;
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
            position.Set(Random.Range(GetComponent<Collider>().bounds.min.x, GetComponent<Collider>().bounds.max.x), Random.Range(GetComponent<Collider>().bounds.min.y, GetComponent<Collider>().bounds.max.y));
        }
        usedValues.Add(position);
        return position;
    }
}
