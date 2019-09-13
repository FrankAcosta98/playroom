using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

    private SavedFiles Save;

    void Start()
    {
        Save = GameObject.FindGameObjectWithTag("Save").GetComponent<SavedFiles>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.name.Equals("Lucy"))
        {
            Save.lastCheckPoint = transform.position;
        }
    }

    // Use this for initialization
    
	
	// Update is called once per frame
	void Update () {
		
	}
}
