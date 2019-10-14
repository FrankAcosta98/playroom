using System.Net.NetworkInformation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject spawn; //El objeto que se spawneará
    public float spawnRate; //Cada cuanto tiempo se spawneará el objeto
    public float nextSpawn; //Sirve en cuestión del tiempo para determinar si se vuelve a hacer spawn del objeto
    List<Vector2> usedValues = new List<Vector2>(); //La lista guarda los valores de posición que ya se usaron para no repetirlos
    public Collider2D area; //Esto define el área donde se podrá spawnear el objeto
    Vector2 position; //Coordenadas donde se hará spawn


    private void Start()
    {

    }

    void FixedUpdate()
    {
        if (Time.time > nextSpawn) //Si el valor de tiempo se hace mayor que el valor de next spawn...
        {
            nextSpawn = Time.time + spawnRate; //Se determina que el siguiente momento de Spawn sea la suma del tiempo en ese momento mas el valor de spawnRate
            Instantiate(spawn, RandomPosition(), Quaternion.identity); //Se hace una Instancia del objeto seleccionado, en un punto random (para saber más de esto checar RandomPosition(), y se usa un Quaternion para respetar la sintaxis del codigo, osea, no se debe mover eso)
        }
    }

    private Vector2 RandomPosition() //Se usa este método para determinar una posición random dentro del aréa que se usa
    {
        //Mientras listacontenga el valor de position...
        while (usedValues.Contains(position))
        {
            //Se define el valor de position usando Random
            //Se definen los valores máximos y minimos usando el collider, se define tanto en el eje x como en el eje y
            //Se necesitan los máximos y mínimos definidos para poder usarlos como limitantes al momento de determinar un punto en el espacio del collider
            position.Set(Random.Range(GetComponent<Collider>().bounds.min.x, GetComponent<Collider>().bounds.max.x), Random.Range(GetComponent<Collider>().bounds.min.y, GetComponent<Collider>().bounds.max.y));
        }
        usedValues.Add(position); //Se agrega el valor de position a la lista de UsedValaues
        return position; //Se devuelve un vector2 para poder usarlo al momento de Instanciar el objeto
    }
}