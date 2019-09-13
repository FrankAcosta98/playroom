using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavedFiles : MonoBehaviour {

    private static SavedFiles instance;
    public Vector2 lastCheckPoint;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    void Start () {
		
	}

	void Update () {
		
	}
}
