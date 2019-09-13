using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerPos : MonoBehaviour {

    private SavedFiles Save;

	// Use this for initialization
	void Start () {
        Save = GameObject.FindGameObjectWithTag("Save").GetComponent<SavedFiles>();
        transform.position = Save.lastCheckPoint;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
