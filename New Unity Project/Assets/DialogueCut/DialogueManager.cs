using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    private Queue<string> sentences;

    public GameObject player;

    void Start()
    {
        sentences = new Queue<string>();
    }


    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            FindObjectOfType<AudioManager>().Play("BoxSound");
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            FindObjectOfType<AudioManager>().Play("BoxSound");
            EndDialogue();
            //if (other.name == "BlueDragonBoi (Clone)")

            player = GameObject.FindGameObjectWithTag("Player");
            if (player.name == "BlueDragonBoi (Clone)")
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<blueJPMovement>().canMove = true;
            }
            else {
                GameObject.FindGameObjectWithTag("Player").GetComponent<JMovement>().canMove = true;
            }
            

            return;
        }

        FindObjectOfType<AudioManager>().Play("VoiceSound");
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);
    }
}
