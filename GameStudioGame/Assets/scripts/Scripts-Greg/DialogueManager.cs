using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; 

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Button startButton; 

    public Animator animator;

    private Queue<string> sentences; 

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>(); 
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.title;

        Debug.Log("Starting conversation with " + dialogue.title); 

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); 
        }

        DisplayNextSentence(); 
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return; 
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        Debug.Log(sentence); 
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
        Destroy(startButton);
        startButton.GetComponent<Image>().enabled = false;
        //startButton.GetComponent<TextMeshPro>().enabled = false; 

        Debug.Log("End of conversation."); 
    }
}
