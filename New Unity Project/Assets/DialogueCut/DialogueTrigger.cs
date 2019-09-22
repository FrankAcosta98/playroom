using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    //
    //private GameObject player;


    /*void Update()
    {
        player = GameObject.Find("DragonBoi");

    }*/

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        //JMovement moveScript = player.GetComponent<JMovement>();
        //moveScript.canMove = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (other.name == "BlueDragonBoi (Clone)")
            {
                other.GetComponent<blueJPMovement>().canMove = false;
            } else
            {
                other.GetComponent<JMovement>().canMove = false;
            } 
            TriggerDialogue();
            //GameObject.FindGameObjectWithTag("Player").GetComponent<JMovement>().canMove = true;
        }
    }
}
