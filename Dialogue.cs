using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public Text dialogueText;
    public bool PlayerInRange; //Determina si ya se puede interactuar
    public string[] dialogues; //Arreglo donde se guardarán los Dialogos
    public int dialogueIndex; //Indice de dialogo
    public bool DialogueEnded; //Indica si ya se terminaron los dialogos

    // Start is called before the first frame update
    void Start()
    {
        dialogueIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.E) && PlayerInRange==true && DialogueEnded ==true)
      {
        dialogueBox.SetActive(false);
            DialogueEnded = false;
      } else if (Input.GetKeyDown(KeyCode.E) && PlayerInRange && DialogueEnded==false)
      {
            dialogueBox.SetActive(true); 
            if (dialogueIndex + 1 == dialogues.Length)//Si llegas al limite se resetea a 0
            {
                dialogueText.text = dialogues[dialogueIndex];
                DialogueEnded = true;
                dialogueIndex = 0;
            }
            else //Si no se suma 1 para pasar al siguiente
            {
                dialogueText.text = dialogues[dialogueIndex];
                dialogueIndex += 1;
            }
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Lucy"))
        {
            PlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name.Equals("Lucy"))
        {
            PlayerInRange = false;
            dialogueBox.SetActive(false);
        }
    }
}
