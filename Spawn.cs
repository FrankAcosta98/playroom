using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject key;
    public GameObject box;
    public float spawnCapacity;
    List<Vector2> usedValues = new List<Vector2>();
    public Collider2D collider;
    Vector2 position;


    private void Start()
    {
        
    }

    void Update()
    {
        if (usedValues.Count < spawnCapacity)
        {
            Instantiate(box, RandomPosition(), Quaternion.identity);
        }
        else if(usedValues.Count == spawnCapacity)
        {
            Instantiate(key, RandomPosition(), Quaternion.identity);
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
