using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class DialougeSystem : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueGUI;
    public Transform dialogueBoxGUI;
    public KeyCode DialogueInput = KeyCode.F;
    public AudioClip audioClip;
    public AudioSource audioSource;
    private GameObject interlocutor;
    public float letterDelay = .1f;
    public float letterMultiplier = .5f;
    public string Names;
    public string[] dialougeLines;

    private bool letterIsMultiplied = false;
    private bool dialogueActive = false;
    private bool dialogueEnded = false;
    private bool outOfRange = true;

    Sentences sentences = new Sentences();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        dialogueText.text = "";
    }

    void Update()
    {
        
    }

    public void EnterRangeOfNPC(GameObject NPC)
    {
        outOfRange = false;
        dialogueGUI.SetActive(true);
        interlocutor = NPC;
        if(dialogueActive == true)
        {
            dialogueGUI.SetActive(false);
        }
    }
    public void NPCName()
    {
        outOfRange = false;
        dialogueBoxGUI.gameObject.SetActive(true);
        nameText.text = Names;
        if(Input.GetKeyDown(DialogueInput))
        {
            if (!dialogueActive)
            {
                dialogueActive = true;
                StartCoroutine(StartDialogue());
            }
        }
        StartDialogue();
    }

    private IEnumerator StartDialogue()
    {
        if(outOfRange == false)
        {
            int dialogueLength = dialougeLines.Length;
            int currentDialogueIndex = 0;

            while(currentDialogueIndex < dialogueLength || !letterIsMultiplied)
            {
                if (!letterIsMultiplied)
                {
                    letterIsMultiplied = true;
                    StartCoroutine(DisplayString(dialougeLines[currentDialogueIndex++]));

                    if(currentDialogueIndex >= dialogueLength)
                    {
                        dialogueEnded = true;
                    }
                }
                yield return 0;
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogueInput) && dialogueEnded == false)
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            dialogueActive = false;
            DropDialogue();
        }
    }

    private IEnumerator DisplayString(string stringToDisplay)
    {
        if (outOfRange == false)
        {
            int stringLength = stringToDisplay.Length;
            int currentCharacterIndex = 0;

            dialogueText.text = "";

            while (currentCharacterIndex < stringLength)
            {
                dialogueText.text += stringToDisplay[currentCharacterIndex];
                currentCharacterIndex++;

                if (currentCharacterIndex < stringLength)
                {
                    if (Input.GetKey(DialogueInput))
                    {
                        yield return new WaitForSeconds(letterDelay * letterMultiplier);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5f);
                    }
                    else
                    {
                        yield return new WaitForSeconds(letterDelay);

                        if (audioClip) audioSource.PlayOneShot(audioClip, 0.5F);
                    }
                }
                else
                {
                    dialogueEnded = false;
                    break;
                }
            }
            while (true)
            {
                if (Input.GetKeyDown(DialogueInput))
                {
                    break;
                }
                yield return 0;
            }
            dialogueEnded = false;
            letterIsMultiplied = false;
            dialogueText.text = "";
        }
    }

    public void DropDialogue()
    {
        dialogueGUI.SetActive(false);
        dialogueBoxGUI.gameObject.SetActive(false);
        if(interlocutor.tag == "Cat")
        {
            GameObject female = GameObject.FindGameObjectWithTag("QuestLady");
            Vector3 pos = female.transform.position;
            pos.x += 3;
            pos.y -= 1;
            interlocutor.transform.position = pos;
            female.GetComponent<NPC>().Sentences = sentences.HappyLady();
            interlocutor.GetComponent<NPC>().Sentences = sentences.HappyCat();
        }
    }

    public void OutOfRange()
    {
        outOfRange = true;
        if (outOfRange == true)
        {
            letterIsMultiplied = false;
            dialogueActive = false;
            StopAllCoroutines();
            dialogueGUI.SetActive(false);
            dialogueBoxGUI.gameObject.SetActive(false);
        }
    }
}

    
